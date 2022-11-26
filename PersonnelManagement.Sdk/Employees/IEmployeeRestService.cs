using PersonnelManagement.Contracts.v1.Requests;
using PersonnelManagement.Contracts.v1.Requests.Employees;
using PersonnelManagement.Contracts.v1.Requests.Queries;
using PersonnelManagement.Contracts.v1.Responses;
using PersonnelManagement.Contracts.v1.Responses.Employees;
using PersonnelManagement.Contracts.v1.Routes;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Sdk.Employees
{
    public interface IEmployeeRestService
    {
        [Get($"/{ApiRoutes.Employees.GetAll}")]
        [Headers("Authorization: Bearer")]
        Task<ApiResponse<PagedResponse<GetEmployeeResponse>>> GetAllAsync(PaginationQueryRequest queryRequest = null, GetAllEmployeesQuery query = null);

        [Post($"/{ApiRoutes.Employees.Create}")]
        [Headers("Authorization: Bearer")]
        Task<ApiResponse<Response<GetEmployeeResponse>>> CreateAsync(CreateEmployeeRequest createRequest);

        [Put($"/{ApiRoutes.Employees.Update}")]
        [Headers("Authorization: Bearer")]
        Task<ApiResponse<Response<GetEmployeeResponse>>> UpdateAsync(Guid employeeId, UpdateEmployeeRequest updateRequest);

        [Delete($"/{ApiRoutes.Employees.Delete}")]
        [Headers("Authorization: Bearer")]
        Task<IApiResponse> DeleteAsync(Guid employeeId);
    }
}
