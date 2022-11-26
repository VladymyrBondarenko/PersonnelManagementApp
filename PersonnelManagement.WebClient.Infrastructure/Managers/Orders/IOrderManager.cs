using PersonnelManagement.Contracts.v1.Requests;
using PersonnelManagement.Contracts.v1.Requests.Orders;
using PersonnelManagement.Contracts.v1.Requests.Originals;
using PersonnelManagement.Contracts.v1.Requests.Queries;
using PersonnelManagement.Contracts.v1.Responses;
using PersonnelManagement.Contracts.v1.Responses.Orders;
using PersonnelManagement.Contracts.v1.Responses.Originals;
using Refit;

namespace PersonnelManagement.WebClient.Infrastructure.Managers.Orders
{
    public interface IOrderManager
    {
        Task<Response<AcceptOrderSuccessResponse>> AcceptOrder(Guid orderId);
        Task<Response<GetOrderResponse>> CreateAsync(CreateOrderRequest createRequest);
        Task<IApiResponse> DeleteAsync(Guid id);
        Task<PagedResponse<GetOrderResponse>> GetAllAsync(PaginationQueryRequest queryRequest = null, GetAllOrdersQuery query = null);
        Task<Response<AcceptOrderSuccessResponse>> RollbackOrder(Guid orderId);
        Task<Response<GetOrderResponse>> UpdateAsync(Guid orderId, UpdateOrderRequest updateRequest);
    }
}