using PersonnelManagement.Contracts.v1.Requests;
using PersonnelManagement.Contracts.v1.Requests.Orders;
using PersonnelManagement.Contracts.v1.Requests.Queries;
using PersonnelManagement.Contracts.v1.Responses;
using PersonnelManagement.Contracts.v1.Responses.OrdersDescription;
using PersonnelManagement.Contracts.v1.Routes;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Sdk.Orders
{
    public interface IOrderDescriptionRestService
    {
        [Get($"/{ApiRoutes.OrderDescriptions.Get}")]
        [Headers("Authorization: Bearer")]
        Task<ApiResponse<Response<GetOrderDescriptionResponse>>> Get(Guid orderDescriptionId);

        [Get($"/{ApiRoutes.OrderDescriptions.GetAll}")]
        [Headers("Authorization: Bearer")]
        Task<ApiResponse<PagedResponse<GetOrderDescriptionResponse>>> GetAllAsync(PaginationQueryRequest queryRequest = null, GetAllOrderDescriptionsQuery query = null);

        [Post($"/{ApiRoutes.OrderDescriptions.Create}")]
        [Headers("Authorization: Bearer")]
        Task<ApiResponse<Response<GetOrderDescriptionResponse>>> CreateAsync(CreateOrderDescriptionRequest createRequest);

        [Put($"/{ApiRoutes.OrderDescriptions.Update}")]
        [Headers("Authorization: Bearer")]
        Task<ApiResponse<Response<GetOrderDescriptionResponse>>> UpdateAsync(Guid orderDescriptionId, UpdateOrderDescriptionRequest updateRequest);

        [Delete($"/{ApiRoutes.OrderDescriptions.Delete}")]
        [Headers("Authorization: Bearer")]
        Task<IApiResponse> DeleteAsync(Guid orderDescriptionId);
    }
}
