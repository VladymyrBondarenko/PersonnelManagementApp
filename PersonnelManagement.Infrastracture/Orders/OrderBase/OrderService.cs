using Microsoft.EntityFrameworkCore;
using PersonnelManagement.Application.Orders.Interfaces;
using PersonnelManagement.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Infrastracture.Orders.OrderBase
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderFactory _orderFactory;

        public OrderService(IOrderRepository orderRepository, IOrderFactory orderFactory)
        {
            _orderRepository = orderRepository;
            _orderFactory = orderFactory;
        }

        public async Task<IOrderBase> GetOrderAsync(Guid id)
        {
            var orderModel = await _orderRepository.GetOrderAsync(id);
            return _orderFactory.GetOrder(orderModel);
        }

        public async Task<List<IOrderBase>> GetAllAsync()
        {
            var orderModels = await _orderRepository.GetAllAsync();
            var orders = new List<IOrderBase>();

            foreach (var orderModel in orderModels)
            {
                orders.Add(_orderFactory.GetOrder(orderModel));
            }

            return orders;
        }

        public async Task<IOrderBase> CreateAsync(Order order)
        {
            order.OrderState = OrderState.Project;
            var addedOrder = await _orderRepository.CreateAsync(order);
            return _orderFactory.GetOrder(addedOrder);
        }
    }
}
