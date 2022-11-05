using Microsoft.EntityFrameworkCore;
using PersonnelManagement.Application.DbContexts;
using PersonnelManagement.Application.Orders.Interfaces;
using PersonnelManagement.Domain.Models;
using PersonnelManagement.Domain.Models.Filters;
using PersonnelManagement.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Infrastracture.Orders.OrderBase
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IApplicationDbContext _dbContext;

        public OrderRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> GetOrdersAmountAsync(GetAllOrdersFilter filter = null)
        {
            var queryable = _dbContext.Orders.AsQueryable();

            if (filter == null)
            {
                return await queryable.CountAsync();
            }

            if (filter.OrderDescriptionId != default)
            {
                queryable = queryable.Where(x => x.OrderDescriptionId == filter.OrderDescriptionId);
            }

            return await queryable.CountAsync();
        }

        public async Task<Order> GetOrderAsync(Guid id)
        {
            return await _dbContext.Orders
                .Include(x => x.Employee)
                .Include(x => x.Position)
                .Include(x => x.Department)
                .Include(x => x.OrderDescription)
                .Include(x => x.Originals)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Order>> GetAllAsync(PaginationQuery paginationFilter = null, GetAllOrdersFilter filter = null)
        {
            var queryable = _dbContext.Orders
                .Include(x => x.Employee)
                .Include(x => x.Position)
                .Include(x => x.Department)
                .Include(x => x.OrderDescription)
                .Include(x => x.Originals).AsQueryable();

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

        public async Task<List<Order>> GetAllAsync()
        {
            return await _dbContext.Orders
                .Include(x => x.Employee)
                .Include(x => x.Position)
                .Include(x => x.Department)
                .Include(x => x.OrderDescription)
                .Include(x => x.Originals)
                .ToListAsync();
        }

        public async Task<Order> CreateAsync(Order order)
        {
            await _dbContext.Orders.AddAsync(order);
            var added = await _dbContext.SaveChangesAsync() > 0;

            if (added)
            {
                var newOrder = await GetOrderAsync(order.Id);
                return newOrder;
            }

            return null;
        }

        public async Task<bool> UpdateAsync(Order order)
        {
            var exists = _dbContext.Orders.Any(x => x.Id == order.Id);

            if (!exists)
            {
                return false;
            }

            _dbContext.Orders.Update(order);

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var order = await _dbContext.Orders.FindAsync(id);

            if (order == null)
            {
                return false;
            }

            _dbContext.Orders.Remove(order);

            try
            {
                return await _dbContext.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }

        private static IQueryable<Order> addFiltersOnQuery(GetAllOrdersFilter filter, IQueryable<Order> queryable)
        {
            if(filter == null)
            {
                return queryable;
            }

            if (!string.IsNullOrWhiteSpace(filter.SearchText))
            {
                var text = filter.SearchText;
                queryable = queryable.Where(x =>
                    x.FirstName.Contains(text) ||
                    x.LastName.Contains(text) ||
                    x.Position.PositionTitle.Contains(text) ||
                    x.Department.DepartmentTitle.Contains(text));
            }

            if (filter.OrderDescriptionId != default)
            {
                queryable = queryable.Where(x => x.OrderDescriptionId == filter.OrderDescriptionId);
            }

            return queryable;
        }
    }
}
