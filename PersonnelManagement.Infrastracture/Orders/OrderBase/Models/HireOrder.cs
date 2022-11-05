using PersonnelManagement.Application.Employees;
using PersonnelManagement.Application.FileOperations.Originals;
using PersonnelManagement.Application.Orders.Interfaces;
using PersonnelManagement.Domain.Models.Filters;
using PersonnelManagement.Domain.Models.Originals;
using PersonnelManagement.Domain.Orders;

namespace PersonnelManagement.Infrastracture.Orders.OrderBase.Models
{
    internal class HireOrder : IOrderBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEmployeeService _employeeService;
        private readonly IOriginalService _originalService;

        public Order Order { get; }

        public HireOrder(Order order, IOrderRepository orderRepository, IEmployeeService employeeService,
            IOriginalService originalService)
        {
            _orderRepository = orderRepository;
            _employeeService = employeeService;
            _originalService = originalService;
            Order = order;
        }

        public async Task<bool> AcceptOrderAsync()
        {
            var employee = await _employeeService.CreateAsync(this);

            if(employee != null)
            {
                Order.OrderState = OrderState.Accepted;
                Order.EmployeeId = employee.Id;

                await _orderRepository.UpdateAsync(Order);

                var originals = await _originalService.GetOriginalsAsync(
                    filter: new GetAllOriginalsFilter { EntityKey = Order.Id });

                // move files from order to employee when accepting hire order
                foreach (var original in originals)
                {
                    var bytes = await _originalService.GetOriginalBytesAsync(original.Id);

                    await _originalService.AddOriginalAsync(
                        new OriginalCreateParams
                        {
                            FileName = original.FileName,
                            Bytes = bytes,
                            EntityId = employee.Id,
                            OriginalEntity = OriginalEntity.Employees
                        });
                }

                return true;
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
