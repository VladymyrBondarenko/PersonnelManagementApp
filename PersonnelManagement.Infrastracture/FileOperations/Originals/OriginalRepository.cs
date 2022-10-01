using Microsoft.EntityFrameworkCore;
using PersonnelManagement.Application.DbContexts;
using PersonnelManagement.Application.FileOperations.Originals;
using PersonnelManagement.Domain.Models.Originals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Infrastracture.FileOperations.Originals
{
    public class OriginalRepository : IOriginalRepository
    {
        private readonly IApplicationDbContext _dbContext;

        public OriginalRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Original>> GetAllAsync()
        {
            return await _dbContext.Originals.ToListAsync();
        }

        public async Task<Original> GetAsync(Guid Id)
        {
            return await _dbContext.Originals.FindAsync(Id);
        }

        public async Task<Original> CreateAsync(Original original)
        {
            await _dbContext.Originals.AddAsync(original);
            var added = await _dbContext.SaveChangesAsync() > 0;

            if (added)
            {
                var newOrig = await GetAsync(original.Id);
                return newOrig;
            }

            return null;
        }

        public async Task<bool> DeleteAsync(Original original)
        {
            var exists = _dbContext.Originals.Any(x => x.Id == original.Id);

            if (!exists)
            {
                return false;
            }

            _dbContext.Originals.Remove(original);

            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
