﻿using PersonnelManagement.Contracts.v1.Requests;
using PersonnelManagement.Contracts.v1.Requests.Originals;
using PersonnelManagement.Contracts.v1.Requests.Queries;
using PersonnelManagement.Contracts.v1.Responses;
using PersonnelManagement.Contracts.v1.Responses.Originals;
using Refit;

namespace PersonnelManagement.WebClient.Infrastructure.Managers.Originals
{
    public interface IOriginalManager
    {
        Task<ApiResponse<Response<GetOriginalResponse>>> CreateAsync(int originalEntity, Guid entityId, StreamPart file);
        Task<IApiResponse> DeleteAsync(Guid originalId);
        Task<ApiResponse<PagedResponse<GetOriginalResponse>>> GetAllAsync(PaginationQueryRequest queryRequest = null, GetAllOriginalsQuery query = null);
        Task<ApiResponse<Response<GetOriginalResponse>>> GetAsync(Guid originalId);
        string GetFileDownloadEndpoint(Guid originalId);
        Task<ApiResponse<Response<GetOriginalResponse>>> UpdateAsync(Guid originalId, UpdateOriginalRequest updateRequest);
    }
}