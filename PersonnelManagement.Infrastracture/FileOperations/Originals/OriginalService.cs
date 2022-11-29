using Microsoft.EntityFrameworkCore;
using PersonnelManagement.Application.DbContexts;
using PersonnelManagement.Application.FileOperations;
using PersonnelManagement.Application.FileOperations.Originals;
using PersonnelManagement.Domain.Exceptions;
using PersonnelManagement.Domain.Models;
using PersonnelManagement.Domain.Models.Filters;
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

        public async Task<List<Original>> GetOriginalsAsync(PaginationQuery paginationFilter = null, GetAllOriginalsFilter filter = null)
        {
            return await _originalRepo.GetAllAsync(paginationFilter, filter);
        }

        public async Task<Original> GetOriginalAsync(Guid id)
        {
            return await _originalRepo.GetAsync(id);
        }

        public async Task<byte[]> GetOriginalBytesAsync(Guid id)
        {
            var original = await GetOriginalAsync(id);

            if(original != null)
            {
                var bytes = await _ftpService.ReadAllBytesAsync(original.OriginalPath);
                return bytes;
            }

            return new byte[0];
        }

        public async Task<int> GetOriginalsAmountAsync(GetAllOriginalsFilter filter = null)
        {
            return await _originalRepo.GetOriginalsAmountAsync(filter);
        }

        public async Task<Original> AddOriginalAsync(OriginalCreateParams createParams)
        {
            Original original = null;

            if (createParams.Bytes != null && !string.IsNullOrWhiteSpace(createParams.FileName))
            {
                original = await addOriginalAsync(
                    createParams.FileName, createParams.Bytes, createParams.OriginalEntity, createParams.EntityId);
            }
            else if (!string.IsNullOrWhiteSpace(createParams.SourceFilePath))
            {
                original = await addOriginalAsync(
                    createParams.SourceFilePath, createParams.OriginalEntity, createParams.EntityId);
            }

            return original;
        }

        public async Task<bool> UpdateOriginal(Original original)
        {
            return await _originalRepo.UpdateOriginal(original);
        }

        public async Task<bool> DeleteOriginalAsync(Guid originalId)
        {
            var original = await _originalRepo.GetAsync(originalId);

            if (original != null)
            {
                await _ftpService.DeleteFileFromFtpAsync(original.OriginalPath);

                return await _originalRepo.DeleteAsync(original);
            }

            return false;
        }

        private async Task<Original> addOriginalAsync(string fileName, byte[] bytes, OriginalEntity originalEntity,
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

            var original = await addOriginalAsync(filePath, originalEntity, bindedEntityKey);

            File.Delete(filePath);

            return original;
        }

        private async Task<Original> addOriginalAsync(string sourceFilePath, OriginalEntity originalEntity,
            Guid bindedEntityKey = default)
        {
            var remotePath = getDirectoryPath(originalEntity);
            var ext = Path.GetExtension(sourceFilePath);
            var fileName = Path.GetRandomFileName() + ext;
            var resultFilePath = Path.Combine(remotePath, fileName);
            var saved = await _ftpService.SaveFileToFtpAsync(sourceFilePath, resultFilePath);

            if (saved)
            {
                var original = new Original
                {
                    Id = Guid.NewGuid(),
                    OriginalPath = resultFilePath,
                    OriginalTitle = Path.GetFileNameWithoutExtension(sourceFilePath),
                    FileName = Path.GetFileName(sourceFilePath),
                    OriginalFileExtension = ext
                };
                if(bindedEntityKey != default)
                {
                    switch (originalEntity)
                    {
                        case OriginalEntity.Orders:
                            original.OrderId = bindedEntityKey;
                            original.EmployeeId = null;
                            break;
                        case OriginalEntity.Employees:
                            original.EmployeeId = bindedEntityKey;
                            original.OrderId = null;
                            break;
                    }
                }

                var createdOrig = await _originalRepo.CreateAsync(original);
                return createdOrig;
            }

            return null;
        }

        private string getDirectoryPath(OriginalEntity originalEntity)
        {
            return originalEntity switch
            {
                OriginalEntity.Orders => _entityOriginalSettings.OrdersDirectoryPath,
                OriginalEntity.Employees => _entityOriginalSettings.EmpoloyeesDirectoryPath,
                _ => throw new NotImplementedException("Specified original type could not be handled")
            };
        }
    }
}
