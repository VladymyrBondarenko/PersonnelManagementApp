using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonnelManagement.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using PersonnelManagement.Application.DbContexts;
using PersonnelManagement.Application.Orders.Interfaces;
using PersonnelManagement.Domain.Models;
using PersonnelManagement.Domain.Models.Filters;
using PersonnelManagement.Domain.Departments;

namespace PersonnelManagement.Infrastracture.Orders.OrderBase
{
    public class OrderDescriptionService : IOrderDescriptionService
    {
        private readonly IApplicationDbContext _dbContext;

        public OrderDescriptionService(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OrderDescription> GetAsync(Guid id)
        {
            return await _dbContext.OrdersDescription
                .Include(x => x.Orders)
                    .ThenInclude(x => x.Department)
                .Include(x => x.Orders)
                    .ThenInclude(x => x.Position)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> GetOrderDescriptionsAmount()
        {
            return await _dbContext.OrdersDescription.CountAsync();
        }

        public async Task<List<OrderDescription>> GetAllAsync(PaginationQuery paginationFilter = null, GetAllOrderDescriptionsFilter filter = null)
        {
            var queryable = _dbContext.OrdersDescription
                .Include(x => x.Orders)
                .AsQueryable();

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

        public async Task<OrderDescription> CreateAsync(OrderDescription orderDesc)
        {
            await _dbContext.OrdersDescription.AddAsync(orderDesc);
            var added = await _dbContext.SaveChangesAsync() > 0;

            if (added)
            {
                var newPosition = await GetAsync(orderDesc.Id);
                return newPosition;
            }

            return null;
        }

        public async Task<bool> UpdateAsync(OrderDescription orderDesc)
        {
            var exists = _dbContext.OrdersDescription.Any(x => x.Id == orderDesc.Id);

            if (!exists)
            {
                return false;
            }

            _dbContext.OrdersDescription.Update(orderDesc);

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var department = await _dbContext.OrdersDescription.FindAsync(id);

            if (department == null)
            {
                return false;
            }

            _dbContext.OrdersDescription.Remove(department);

            try
            {
                return await _dbContext.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }

        private IQueryable<OrderDescription> addFiltersOnQuery(GetAllOrderDescriptionsFilter filter, IQueryable<OrderDescription> queryable)
        {
            if (!string.IsNullOrWhiteSpace(filter.SearchText))
            {
                queryable = queryable.Where(x => x.OrderDescriptionTitle.Contains(filter.SearchText));
            }

            return queryable;
        }
    }
}
