using Microsoft.EntityFrameworkCore;
using PersonnelManagement.Application.DbContexts;
using PersonnelManagement.Application.FileOperations;
using PersonnelManagement.Application.FileOperations.Originals;
using PersonnelManagement.Domain.Exceptions;
using PersonnelManagement.Domain.Models.Originals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Infrastracture.FileOperations.Originals
{
    public class OriginalService : IOriginalService
    {
        private readonly IFtpService _ftpService;
        private readonly FtpStructureSettings _entityOriginalSettings;
        private readonly IOriginalRepository _originalRepo;

        public OriginalService(IFtpService ftpService, FtpStructureSettings entityOriginalSettings,
            IOriginalRepository originalRepo)
        {
            _ftpService = ftpService;
            _entityOriginalSettings = entityOriginalSettings;
            _originalRepo = originalRepo;
        }

        public async Task<List<Original>> GetOriginalsAsync()
        {
            return await _originalRepo.GetAllAsync();
        }

        public async Task<Original> GetOriginalAsync(Guid id)
        {
            return await _originalRepo.GetAsync(id);
        }

        public async Task<Original> AddOriginalAsync(OriginalCreateParams createParams, OriginalType originalType)
        {
            var bindedEntutyKey = originalType switch
            {
                OriginalType.Orders => createParams.OrderId,
                OriginalType.Employees => createParams.EmployeeId,
                _ => throw new NotImplementedException("Specified original type could not be handled")
            };

            Original original = null;

            if (createParams.Bytes != null && !string.IsNullOrWhiteSpace(createParams.FileName))
            {
                original = await addOriginalAsync(
                    createParams.FileName, createParams.Bytes, originalType, bindedEntutyKey);
            }
            else if (!string.IsNullOrWhiteSpace(createParams.SourceFilePath))
            {
                original = await addOriginalAsync(
                    createParams.SourceFilePath, originalType, bindedEntutyKey);
            }

            return original;
        }

        private async Task<Original> addOriginalAsync(string fileName, byte[] bytes, OriginalType originalType,
            Guid bindedEntityKey = default)
        {
            string executableLocation = Path.GetDirectoryName(
                Assembly.GetExecutingAssembly().Location);
            var filePath = Path.Combine(executableLocation, fileName);

            await File.WriteAllBytesAsync(filePath, bytes);

            if (!File.Exists(filePath))
            {
                return null;
            }

            var original = await addOriginalAsync(filePath, originalType, bindedEntityKey);

            File.Delete(filePath);

            return original;
        }

        private async Task<Original> addOriginalAsync(string sourceFilePath, OriginalType originalType,
            Guid bindedEntityKey = default)
        {
            var remotePath = originalType switch
            {
                OriginalType.Orders => _entityOriginalSettings.OrdersDirectoryPath,
                OriginalType.Employees => _entityOriginalSettings.EmpoloyeesDirectoryPath,
                _ => throw new NotImplementedException("Specified original type could not be handled")
            };

            var fileName = Path.GetFileName(sourceFilePath);
            var resultFilePath = Path.Combine(remotePath, fileName);
            var saved = await _ftpService.SaveFileToFtpAsync(sourceFilePath, resultFilePath);

            if (saved)
            {
                var original = new Original
                {
                    Id = Guid.NewGuid(),
                    OriginalPath = resultFilePath,
                    OriginalTitle = Path.GetFileNameWithoutExtension(sourceFilePath),
                    OriginalFileExtension = Path.GetExtension(resultFilePath)
                };
                if(bindedEntityKey != default)
                {
                    switch (originalType)
                    {
                        case OriginalType.Orders:
                            original.OrderId = bindedEntityKey;
                            original.EmployeeId = null;
                            break;
                        case OriginalType.Employees:
                            original.EmployeeId = bindedEntityKey;
                            original.OrderId = null;
                            break;
                    }
                }

                var createdOrig = await _originalRepo.CreateAsync(original);
                return createdOrig;
            }
            else
            {
                throw new OriginalNotSavedException("Original was not saved.");
            }
        }

        public async Task<bool> DeleteOriginalAsync(Original original)
        {
            var exists = await _originalRepo.GetAsync(original.Id) != null;

            if (exists)
            {
                await _ftpService.DeleteFileFromFtpAsync(original.OriginalPath);

                return await _originalRepo.DeleteAsync(original);
            }

            return false;
        }
    }
}
