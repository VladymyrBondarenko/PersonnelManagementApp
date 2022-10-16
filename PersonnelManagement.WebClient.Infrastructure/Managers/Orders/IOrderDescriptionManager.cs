using PersonnelManagement.Contracts.v1.Requests;
using PersonnelManagement.Contracts.v1.Requests.Orders;
using PersonnelManagement.Contracts.v1.Requests.Queries;
using PersonnelManagement.Contracts.v1.Responses;
using PersonnelManagement.Contracts.v1.Responses.OrdersDescription;
using Refit;

namespace PersonnelManagement.WebClient.Infrastructure.Managers.Orders
{
    public interface IOrderDescriptionManager
    {
        Task<ApiResponse<Response<GetOrderDescriptionResponse>>> CreateAsync(CreateOrderDescriptionRequest createRequest);
        Task<IApiResponse> DeleteAsync(Guid id);
        Task<ApiResponse<PagedResponse<GetOrderDescriptionResponse>>> GetAllAsync(PaginationQueryRequest queryRequest = null, GetAllOrderDescriptionsQuery query = null);
        Task<ApiResponse<Response<GetOrderDescriptionResponse>>> GetAsync(Guid id);
        Task<ApiResponse<Response<GetOrderDescriptionResponse>>> UpdateAsync(Guid orderDescriptionId, UpdateOrderDescriptionRequest updateRequest);
    }
}