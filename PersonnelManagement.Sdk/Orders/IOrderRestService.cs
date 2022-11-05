using PersonnelManagement.Contracts.v1.Requests;
using PersonnelManagement.Contracts.v1.Requests.Orders;
using PersonnelManagement.Contracts.v1.Requests.Originals;
using PersonnelManagement.Contracts.v1.Requests.Queries;
using PersonnelManagement.Contracts.v1.Responses;
using PersonnelManagement.Contracts.v1.Responses.Orders;
using PersonnelManagement.Contracts.v1.Responses.Originals;
using PersonnelManagement.Contracts.v1.Routes;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Sdk.Orders
{
    public interface IOrderRestService
    {
        [Get($"/{ApiRoutes.Orders.GetAll}")]
        Task<ApiResponse<PagedResponse<GetOrderResponse>>> GetAllAsync(PaginationQueryRequest queryRequest = null, GetAllOrdersQuery query = null);

        [Post($"/{ApiRoutes.Orders.Create}")]
        Task<ApiResponse<Response<GetOrderResponse>>> CreateAsync(CreateOrderRequest createRequest);

        [Put($"/{ApiRoutes.Orders.Update}")]
        Task<ApiResponse<Response<GetOrderResponse>>> UpdateAsync(Guid orderId, UpdateOrderRequest updateRequest);

        [Delete($"/{ApiRoutes.Orders.Delete}")]
        Task<IApiResponse> DeleteAsync(Guid orderId);

        [Post($"/{ApiRoutes.Orders.AcceptOrder}")]
        Task<ApiResponse<Response<AcceptOrderSuccessResponse>>> AcceptOrder(Guid orderId);

        [Post($"/{ApiRoutes.Orders.RollbackOrder}")]
        Task<ApiResponse<Response<AcceptOrderSuccessResponse>>> RollbackOrder(Guid orderId);
    }
}
