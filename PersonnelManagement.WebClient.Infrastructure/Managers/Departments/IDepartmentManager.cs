using PersonnelManagement.Contracts.v1.Requests;
using PersonnelManagement.Contracts.v1.Requests.Queries;
using PersonnelManagement.Contracts.v1.Responses;
using PersonnelManagement.Contracts.v1.Responses.Departments;
using Refit;

namespace PersonnelManagement.WebClient.Infrastructure.Managers.Departments
{
    public interface IDepartmentManager
    {
        Task<ApiResponse<Response<GetDepartmentResponse>>> CreateAsync(CreateDepartmentRequest createRequest);
        Task<IApiResponse> DeleteAsync(Guid id);
        Task<ApiResponse<PagedResponse<GetDepartmentResponse>>> GetAllAsync(PaginationQueryRequest queryRequest = null, GetAllDepartmentsQuery query = null);
        Task<ApiResponse<Response<GetDepartmentResponse>>> UpdateAsync(Guid departmentId, UpdateDepartmentRequest updateRequest);
    }
}