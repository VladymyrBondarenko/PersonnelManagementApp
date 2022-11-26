using PersonnelManagement.Contracts.v1.Requests;
using PersonnelManagement.Contracts.v1.Requests.Positions;
using PersonnelManagement.Contracts.v1.Requests.Queries;
using PersonnelManagement.Contracts.v1.Responses;
using PersonnelManagement.Contracts.v1.Responses.Positions;
using PersonnelManagement.Contracts.v1.Routes;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Sdk.Positions
{
    public interface IPositionRestService
    {
        [Get($"/{ApiRoutes.Positions.GetAll}")]
        [Headers("Authorization: Bearer")]
        Task<ApiResponse<PagedResponse<GetPositionResponse>>> GetAllAsync(PaginationQueryRequest queryRequest = null, GetAllPositionsQuery query = null);

        [Post($"/{ApiRoutes.Positions.Create}")]
        [Headers("Authorization: Bearer")]
        Task<ApiResponse<Response<GetPositionResponse>>> CreateAsync(CreatePositionRequest createRequest);

        [Put($"/{ApiRoutes.Positions.Update}")]
        [Headers("Authorization: Bearer")]
        Task<ApiResponse<Response<GetPositionResponse>>> UpdateAsync(Guid positionId, UpdatePositionRequest updateRequest);

        [Delete($"/{ApiRoutes.Positions.Delete}")]
        [Headers("Authorization: Bearer")]
        Task<IApiResponse> DeleteAsync(Guid positionId);
    }
}
