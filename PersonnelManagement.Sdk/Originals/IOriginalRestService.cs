using PersonnelManagement.Contracts.v1.Requests;
using PersonnelManagement.Contracts.v1.Requests.Originals;
using PersonnelManagement.Contracts.v1.Requests.Queries;
using PersonnelManagement.Contracts.v1.Responses;
using PersonnelManagement.Contracts.v1.Responses.Originals;
using PersonnelManagement.Contracts.v1.Routes;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Sdk.Originals
{
    public interface IOriginalRestService
    {
        [Get($"/{ApiRoutes.Originals.GetAll}")]
        [Headers("Authorization: Bearer")]
        Task<ApiResponse<PagedResponse<GetOriginalResponse>>> GetAllAsync(PaginationQueryRequest queryRequest = null, GetAllOriginalsQuery query = null);

        [Get($"/{ApiRoutes.Originals.Get}")]
        [Headers("Authorization: Bearer")]
        Task<ApiResponse<Response<GetOriginalResponse>>> GetAsync(Guid originalId);

        [Put($"/{ApiRoutes.Originals.Update}")]
        [Headers("Authorization: Bearer")]
        Task<ApiResponse<Response<GetOriginalResponse>>> UpdateAsync(Guid originalId, UpdateOriginalRequest updateRequest);

        [Multipart]
        [Post($"/{ApiRoutes.Originals.Create}")]
        [Headers("Authorization: Bearer")]
        Task<ApiResponse<Response<GetOriginalResponse>>> CreateAsync(int originalEntity, Guid entityId, [AliasAs("file")] StreamPart file);

        [Delete($"/{ApiRoutes.Originals.Delete}")]
        [Headers("Authorization: Bearer")]
        Task<IApiResponse> DeleteAsync(Guid originalId);
    }
}
