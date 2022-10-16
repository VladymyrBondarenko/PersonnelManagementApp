using PersonnelManagement.Contracts.v1.Requests;
using PersonnelManagement.Contracts.v1.Requests.Queries;
using PersonnelManagement.Contracts.v1.Responses;
using PersonnelManagement.Contracts.v1.Responses.Departments;
using PersonnelManagement.Sdk.Departments;
using PersonnelManagement.WebClient.Options;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.WebClient.Infrastructure.Managers.Departments
{
    public class DepartmentManager : IDepartmentManager
    {
        private IDepartmentRestService _departmentService;

        public DepartmentManager(IHttpClientFactory httpClientFactory,
            ManagersApiOptions apiOptions)
        {
            var httpClient = httpClientFactory.CreateClient(apiOptions.ClientName);
            _departmentService = RestService.For<IDepartmentRestService>(httpClient);
        }

        public async Task<ApiResponse<PagedResponse<GetDepartmentResponse>>> GetAllAsync(PaginationQueryRequest queryRequest = null, GetAllDepartmentsQuery query = null)
        {
            var response = await _departmentService.GetAllAsync(queryRequest, query);
            return response;
        }

        public async Task<ApiResponse<Response<GetDepartmentResponse>>> CreateAsync(CreateDepartmentRequest createRequest)
        {
            var response = await _departmentService.CreateAsync(createRequest);
            return response;
        }

        public async Task<ApiResponse<Response<GetDepartmentResponse>>> UpdateAsync(Guid departmentId, UpdateDepartmentRequest updateRequest)
        {
            var response = await _departmentService.UpdateAsync(departmentId, updateRequest);
            return response;
        }

        public async Task<IApiResponse> DeleteAsync(Guid id)
        {
            return await _departmentService.DeleteAsync(id);
        }
    }
}
