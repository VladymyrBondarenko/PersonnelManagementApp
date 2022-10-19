using PersonnelManagement.Application.Employees;
using PersonnelManagement.Application.Orders.Interfaces;
using PersonnelManagement.Domain.Orders;

namespace PersonnelManagement.Infrastracture.Orders.OrderBase.Models
{
    internal class HireOrder : IOrderBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEmployeeService _employeeService;

        public Order Order { get; }

        public HireOrder(Order order, IOrderRepository orderRepository, IEmployeeService employeeService)
        {
            _orderRepository = orderRepository;
            _employeeService = employeeService;
            Order = order;
        }

        public async Task<bool> AcceptOrderAsync()
        {
            var employee = await _employeeService.CreateAsync(this);

            if(employee != null)
            {
                Order.OrderState = OrderState.Accepted;
                Order.EmployeeId = employee.Id;
                return await _orderRepository.UpdateAsync(Order);
            }

            // TODO: creating appointment

            return false;
        }

        public async Task<bool> RollbackOrderAsync(bool toProject = false)
        {
            if(Order?.Employee != null)
            {
                var employeeId = Order.EmployeeId ?? default;

                Order.OrderState = toProject ? OrderState.Project : OrderState.Canceled;
                Order.EmployeeId = null;

                if(await _orderRepository.UpdateAsync(Order) && employeeId != default)
                {
                    return await _employeeService.DeleteAsync(employeeId);
                }
            }

            return false;
        }
    }
}
