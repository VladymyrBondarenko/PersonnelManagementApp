using Microsoft.EntityFrameworkCore;
using PersonnelManagement.Application.DbContexts;
using PersonnelManagement.Application.FileOperations.Originals;
using PersonnelManagement.Domain.Models;
using PersonnelManagement.Domain.Models.Filters;
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

        public async Task<List<Original>> GetAllAsync(PaginationQuery paginationFilter = null, GetAllOriginalsFilter filter = null)
        {
            var queryable = _dbContext.Originals.OrderByDescending(x => x.CreatedDate).AsQueryable();

            if (filter != null)
            {
                queryable = addFiltersOnQuery(filter, queryable);
            }

            if (paginationFilter == null || paginationFilter.PageSize < 1 || paginationFilter.PageNumber < 1)
            {
                return await queryable.ToListAsync();
            }

            var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;
            return await queryable
                .Skip(skip).Take(paginationFilter.PageSize).ToListAsync();
        }

        public async Task<int> GetOriginalsAmountAsync(GetAllOriginalsFilter filter = null)
        {
            var queryable = _dbContext.Originals.AsQueryable();

            if(filter == null)
            {
                return await queryable.CountAsync();
            }

            if(filter.EntityKey != default)
            {
                queryable = queryable.Where(x =>
                    x.EmployeeId == filter.EntityKey ||
                    x.OrderId == filter.EntityKey);
            }

            return await queryable.CountAsync();
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

        public async Task<bool> UpdateOriginal(Original original)
        {
            var exists = _dbContext.Originals.Any(x => x.Id == original.Id);

            if (!exists)
            {
                return false;
            }

            _dbContext.Originals.Update(original);

            return await _dbContext.SaveChangesAsync() > 0;
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

        private static IQueryable<Original> addFiltersOnQuery(GetAllOriginalsFilter filter, IQueryable<Original> queryable)
        {
            if (filter == null)
            {
                return queryable;
            }

            if (filter.EntityKey != default)
            {
                queryable = queryable.Where(x =>
                    x.EmployeeId == filter.EntityKey ||
                    x.OrderId == filter.EntityKey);
            }

            if (!string.IsNullOrWhiteSpace(filter.SearchText))
            {
                queryable = queryable.Where(x =>
                    x.OriginalTitle.Contains(filter.SearchText) ||
                    x.OriginalFileExtension.Contains(filter.SearchText));
            }

            return queryable;
        }
    }
}
