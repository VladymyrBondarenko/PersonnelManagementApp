using PersonnelManagement.Contracts.v1.Requests;
using PersonnelManagement.Contracts.v1.Requests.Queries;
using PersonnelManagement.Contracts.v1.Responses;
using PersonnelManagement.Contracts.v1.Responses.Departments;
using Refit;

namespace PersonnelManagement.WebClient.Infrastructure.Managers.Departments
{
    public interface IDepartmentManager
    {
        Task<Response<GetDepartmentResponse>> CreateAsync(CreateDepartmentRequest createRequest);
        Task<IApiResponse> DeleteAsync(Guid id);
        Task<PagedResponse<GetDepartmentResponse>> GetAllAsync(PaginationQueryRequest queryRequest = null, GetAllDepartmentsQuery query = null);
        Task<Response<GetDepartmentResponse>> UpdateAsync(Guid departmentId, UpdateDepartmentRequest updateRequest);
    }
}