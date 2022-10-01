using PersonnelManagement.Application.FileOperations.Originals;
using PersonnelManagement.Application.Orders.Interfaces;
using PersonnelManagement.Domain.Models.Originals;
using PersonnelManagement.Domain.Orders;

namespace PersonnelManagement.Infrastracture.Orders.OrderBase
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderFactory _orderFactory;
        private readonly IOriginalService _originalService;

        public OrderService(IOrderRepository orderRepository, IOrderFactory orderFactory,
            IOriginalService originalService)
        {
            _orderRepository = orderRepository;
            _orderFactory = orderFactory;
            _originalService = originalService;
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

        public async Task<Original> AddOriginalAsync(OriginalCreateParams createParams)
        {
            var order = await _orderRepository.GetOrderAsync(createParams.OrderId);

            if(order == null)
            {
                return null;
            }

            var original = await _originalService.AddOriginalAsync(createParams, OriginalType.Orders);
            return original;
        }

        public async Task<bool> DeleteOriginalAsync(OriginalDeleteParams deleteParams)
        {
            var original = await _originalService.GetOriginalAsync(deleteParams.OriginalId);

            if(original == null || original.OrderId != deleteParams.OrderId)
            {
                return false;
            }

            var origDeleted = await _originalService.DeleteOriginalAsync(original);
            return origDeleted;
        }
    }
}
