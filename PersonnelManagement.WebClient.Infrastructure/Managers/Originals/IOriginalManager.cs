using PersonnelManagement.Contracts.v1.Requests;
using PersonnelManagement.Contracts.v1.Requests.Originals;
using PersonnelManagement.Contracts.v1.Requests.Queries;
using PersonnelManagement.Contracts.v1.Responses;
using PersonnelManagement.Contracts.v1.Responses.Originals;
using Refit;

namespace PersonnelManagement.WebClient.Infrastructure.Managers.Originals
{
    public interface IOriginalManager
    {
        Task<Response<GetOriginalResponse>> CreateAsync(int originalEntity, int originalType, Guid entityId, StreamPart file);
        Task<IApiResponse> DeleteAsync(Guid originalId);
        Task<PagedResponse<GetOriginalResponse>> GetAllAsync(PaginationQueryRequest queryRequest = null, GetAllOriginalsQuery query = null);
        Task<Response<GetOriginalResponse>> GetAsync(Guid originalId);
        string GetFileDownloadEndpoint(Guid originalId, string token);
        Task<Response<GetOriginalResponse>> UpdateAsync(Guid originalId, UpdateOriginalRequest updateRequest);
    }
}