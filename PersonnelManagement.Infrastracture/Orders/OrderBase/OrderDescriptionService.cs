using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonnelManagement.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using PersonnelManagement.Application.DbContexts;
using PersonnelManagement.Application.Orders.Interfaces;

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
            return await _dbContext.OrdersDescription.FindAsync(id);
        }

        public async Task<List<OrderDescription>> GetAllAsync()
        {
            return await _dbContext.OrdersDescription.ToListAsync();
        }

        public async Task<OrderDescription> CreateAsync(OrderDescription order)
        {
            var addedOrder = await _dbContext.OrdersDescription.AddAsync(order);
            await _dbContext.SaveChangesAsync();

            return addedOrder.Entity;
        }
    }
}
