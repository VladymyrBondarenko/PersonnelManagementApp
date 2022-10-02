using PersonnelManagement.Application.Employees;
using PersonnelManagement.Application.Orders.Interfaces;
using PersonnelManagement.Domain.Orders;
using PersonnelManagement.Infrastracture.Orders.OrderBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Infrastracture.Orders.OrderBase
{
    public class OrderFactory : IOrderFactory
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEmployeeService _employeeService;

        public OrderFactory(IOrderRepository orderRepository, IEmployeeService employeeService)
        {
            _orderRepository = orderRepository;
            _employeeService = employeeService;
        }

        public IOrderBase GetOrder(Order order)
        {
            var orderType = order.OrderDescription?.OrderType;

            switch (orderType)
            {
                case OrderType.HireOrder:
                    return (IOrderBase)Activator.CreateInstance(
                        typeof(HireOrder), order, _orderRepository, _employeeService);
                case OrderType.FireOrder:
                    return (IOrderBase)Activator.CreateInstance(
                        typeof(FireOrder), order, _orderRepository, _employeeService);
                default:
                    return (IOrderBase)Activator.CreateInstance(
                        typeof(OtherOrder), order, _orderRepository, _employeeService);
            }
        }
    }
}
