﻿using PersonnelManagement.Contracts.v1.Requests;
using PersonnelManagement.Contracts.v1.Requests.Positions;
using PersonnelManagement.Contracts.v1.Requests.Queries;
using PersonnelManagement.Contracts.v1.Responses;
using PersonnelManagement.Contracts.v1.Responses.Positions;
using Refit;

namespace PersonnelManagement.WebClient.Infrastructure.Managers.Positions
{
    public interface IPositionManager
    {
        Task<ApiResponse<Response<GetPositionResponse>>> CreateAsync(CreatePositionRequest createRequest);
        Task<IApiResponse> DeleteAsync(Guid id);
        Task<ApiResponse<PagedResponse<GetPositionResponse>>> GetAllAsync(PaginationQueryRequest queryRequest = null, GetAllPositionsQuery query = null);
        Task<ApiResponse<Response<GetPositionResponse>>> UpdateAsync(Guid positionId, UpdatePositionRequest updateRequest);
    }
}