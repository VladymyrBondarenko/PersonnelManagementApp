using PersonnelManagement.Application.Employees;
using PersonnelManagement.Application.Orders.Interfaces;
using PersonnelManagement.Domain.Employees;
using PersonnelManagement.Domain.Orders;

namespace PersonnelManagement.Infrastracture.Orders.OrderBase.Models
{
    internal class FireOrder : IOrderBase
    {
        public Order Order { get; }

        private readonly IOrderRepository _orderRepository;
        private readonly IEmployeeService _employeeService;

        public FireOrder(Order order, IOrderRepository orderRepository, IEmployeeService employeeService)
        {
            Order = order;
            _orderRepository = orderRepository;
            _employeeService = employeeService;
        }

        public async Task<bool> AcceptOrderAsync()
        {
            if (Order?.Employee != null)
            {
                Order.Employee.EmployeeState = EmployeeState.Fired;
                if(await _employeeService.UpdateAsync(Order.Employee))
                {
                    Order.OrderState = OrderState.Accepted;
                    return await _orderRepository.UpdateAsync(Order);
                }
            }

            // TODO: deleting appointment

            return false;
        }

        public async Task<bool> RollbackOrderAsync(bool toProject = false)
        {
            if (Order?.Employee != null)
            {
                Order.Employee.EmployeeState = EmployeeState.Hired;
                if (await _employeeService.UpdateAsync(Order.Employee))
                {
                    Order.OrderState = toProject ? OrderState.Project : OrderState.Canceled;
                    return await _orderRepository.UpdateAsync(Order);
                }
            }

            // TODO: creating appointment

            return false;
        }
    }
}