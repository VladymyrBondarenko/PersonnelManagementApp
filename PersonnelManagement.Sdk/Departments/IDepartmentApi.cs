using PersonnelManagement.Contracts.v1.Requests;
using PersonnelManagement.Contracts.v1.Requests.Queries;
using PersonnelManagement.Contracts.v1.Responses;
using PersonnelManagement.Contracts.v1.Responses.Departments;
using PersonnelManagement.Contracts.v1.Routes;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Sdk.Departments
{
    public interface IDepartmentApi
    {
        [Get($"/{ApiRoutes.Departments.GetAll}")]
        Task<ApiResponse<PagedResponse<GetDepartmentResponse>>> GetAllAsync(PaginationQueryRequest queryRequest = null, GetAllDepartmentsQuery query = null);

        [Post($"/{ApiRoutes.Departments.Create}")]
        Task<ApiResponse<Response<GetDepartmentResponse>>> CreateAsync(CreateDepartmentRequest createRequest);

        [Put($"/{ApiRoutes.Departments.Update}")]
        Task<ApiResponse<Response<GetDepartmentResponse>>> UpdateAsync(Guid departmentId, UpdateDepartmentRequest updateRequest);

        [Delete($"/{ApiRoutes.Departments.Delete}")]
        Task<IApiResponse> DeleteAsync(Guid departmentId);
    }
}
