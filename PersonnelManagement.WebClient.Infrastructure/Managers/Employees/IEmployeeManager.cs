﻿using PersonnelManagement.Contracts.v1.Requests;
using PersonnelManagement.Contracts.v1.Requests.Employees;
using PersonnelManagement.Contracts.v1.Requests.Queries;
using PersonnelManagement.Contracts.v1.Responses;
using PersonnelManagement.Contracts.v1.Responses.Employees;
using Refit;

namespace PersonnelManagement.WebClient.Infrastructure.Managers.Employees
{
    public interface IEmployeeManager
    {
        Task<ApiResponse<Response<GetEmployeeResponse>>> CreateAsync(CreateEmployeeRequest createRequest);
        Task<IApiResponse> DeleteAsync(Guid id);
        Task<ApiResponse<PagedResponse<GetEmployeeResponse>>> GetAllAsync(PaginationQueryRequest queryRequest = null, GetAllEmployeesQuery query = null);
        Task<ApiResponse<Response<GetEmployeeResponse>>> UpdateAsync(Guid orderDescriptionId, UpdateEmployeeRequest updateRequest);
    }
}