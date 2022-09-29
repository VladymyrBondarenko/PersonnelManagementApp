using Microsoft.EntityFrameworkCore;
using PersonnelManagement.Application.DbContexts;
using PersonnelManagement.Application.FileOperations;
using PersonnelManagement.Application.FileOperations.Originals;
using PersonnelManagement.Domain.Exceptions;
using PersonnelManagement.Domain.Models.Originals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Infrastracture.FileOperations.Originals
{
    public class OriginalService : IOriginalService
    {
        private readonly IFtpService _ftpService;
        private readonly FtpStructureSettings _entityOriginalSettings;
        private readonly IApplicationDbContext _dbContext;

        public OriginalService(IFtpService ftpService, FtpStructureSettings entityOriginalSettings,
            IApplicationDbContext dbContext)
        {
            _ftpService = ftpService;
            _entityOriginalSettings = entityOriginalSettings;
            _dbContext = dbContext;
        }

        public async Task<List<Original>> GetOriginalsAsync()
        {
            return await _dbContext.Originals.ToListAsync();
        }

        public async Task<Original> GetOriginalAsync(Guid id)
        {
            return await _dbContext.Originals.FindAsync(id);
        }

        public async Task<Original> AddOriginalAsync(string sourceFilePath, OriginalType originalType,
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
                            break;
                        case OriginalType.Employees:
                            original.EmployeeId = bindedEntityKey;
                            break;
                    }
                }

                var created = await _dbContext.Originals.AddAsync(original);

                await _dbContext.SaveChangesAsync();

                return created.Entity;
            }
            else
            {
                throw new OriginalNotSavedException("Original was not saved.");
            }
        }

        public async Task<bool> DeleteOriginalAsync(Original original)
        {
            var exists = await _dbContext.Originals.FindAsync(original?.Id) != null;

            if (exists)
            {
                await _ftpService.DeleteFileFromFtpAsync(original.OriginalPath);

                _dbContext.Originals.Remove(original);
                return await _dbContext.SaveChangesAsync() > 0;
            }

            return false;
        }
    }
}
