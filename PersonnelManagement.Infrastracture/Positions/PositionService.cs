using Microsoft.EntityFrameworkCore;
using PersonnelManagement.Application.DbContexts;
using PersonnelManagement.Application.Positions;
using PersonnelManagement.Domain.Models;
using PersonnelManagement.Domain.Models.Filters;
using PersonnelManagement.Domain.Positions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Infrastracture.Positions
{
    public class PositionService : IPositionService
    {
        private readonly IApplicationDbContext _dbContext;

        public PositionService(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> GetPositionsAmountAsync()
        {
            return await _dbContext.Positions.CountAsync();
        }

        public async Task<List<Position>> GetAllAsync(PaginationQuery paginationFilter = null, GetAllPositionsFilter filter = null)
        {
            var queryable = _dbContext.Positions.OrderByDescending(x => x.CreatedDate).AsQueryable();

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

        public async Task<List<Position>> GetAllAsync()
        {
            return await _dbContext.Positions.OrderByDescending(x => x.CreatedDate).ToListAsync();
        }

        public async Task<Position> GetAsync(Guid Id)
        {
            return await _dbContext.Positions.FindAsync(Id);
        }

        public async Task<bool> UpdateAsync(Position position)
        {
            var exists = _dbContext.Positions.Any(x => x.Id == position.Id);

            if (!exists)
            {
                return false;
            }

            _dbContext.Positions.Update(position);

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var position = await _dbContext.Positions.FindAsync(id);

            if (position == null)
            {
                return false;
            }

            _dbContext.Positions.Remove(position);

            try
            {
                return await _dbContext.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Position> CreateAsync(Position position)
        {
            await _dbContext.Positions.AddAsync(position);
            var added = await _dbContext.SaveChangesAsync() > 0;

            if (added)
            {
                var newPosition = await GetAsync(position.Id);
                return newPosition;
            }

            return null;
        }

        private static IQueryable<Position> addFiltersOnQuery(GetAllPositionsFilter filter, IQueryable<Position> queryable)
        {
            if (!string.IsNullOrWhiteSpace(filter.SearchText))
            {
                var text = filter.SearchText;
                queryable = queryable.Where(x =>
                    x.PositionTitle.Contains(text) ||
                    x.PositionDescription.Contains(text));
            }

            return queryable;
        }
    }
}
