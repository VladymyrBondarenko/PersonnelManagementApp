using PersonnelManagement.Contracts.v1.Requests;
using PersonnelManagement.Contracts.v1.Requests.Orders;
using PersonnelManagement.Contracts.v1.Requests.Queries;
using PersonnelManagement.Contracts.v1.Responses;
using PersonnelManagement.Contracts.v1.Responses.Orders;
using Refit;

namespace PersonnelManagement.WebClient.Infrastructure.Managers.Orders
{
    public interface IOrderManager
    {
        Task<ApiResponse<Response<AcceptOrderSuccessResponse>>> AcceptOrder(Guid orderId);
        Task<ApiResponse<Response<GetOrderResponse>>> CreateAsync(CreateOrderRequest createRequest);
        Task<IApiResponse> DeleteAsync(Guid id);
        Task<ApiResponse<PagedResponse<GetOrderResponse>>> GetAllAsync(PaginationQueryRequest queryRequest = null, GetAllOrdersQuery query = null);
        Task<ApiResponse<Response<AcceptOrderSuccessResponse>>> RollbackOrder(Guid orderId);
        Task<ApiResponse<Response<GetOrderResponse>>> UpdateAsync(Guid orderDescriptionId, UpdateOrderRequest updateRequest);
    }
}