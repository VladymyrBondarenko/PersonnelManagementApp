using Microsoft.EntityFrameworkCore;
using PersonnelManagement.Application.DbContexts;
using PersonnelManagement.Application.Orders.Interfaces;
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

        public async Task<Order> GetOrderAsync(Guid id)
        {
            return await _dbContext.Orders
                .Include(x => x.Employee)
                .Include(x => x.Position)
                .Include(x => x.Department)
                .Include(x => x.OrderDescription)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _dbContext.Orders
                .Include(x => x.Employee)
                .Include(x => x.Position)
                .Include(x => x.Department)
                .Include(x => x.OrderDescription)
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
    }
}
