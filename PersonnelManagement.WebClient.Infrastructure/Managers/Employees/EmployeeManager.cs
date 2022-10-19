using PersonnelManagement.Contracts.v1.Requests;
using PersonnelManagement.Contracts.v1.Requests.Employees;
using PersonnelManagement.Contracts.v1.Requests.Queries;
using PersonnelManagement.Contracts.v1.Responses;
using PersonnelManagement.Contracts.v1.Responses.Employees;
using PersonnelManagement.Sdk.Employees;
using PersonnelManagement.WebClient.Options;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.WebClient.Infrastructure.Managers.Employees
{
    public class EmployeeManager : IEmployeeManager
    {
        private IEmployeeRestService _employeeService;

        public EmployeeManager(IHttpClientFactory httpClientFactory,
            ManagersApiOptions apiOptions)
        {
            var httpClient = httpClientFactory.CreateClient(apiOptions.ClientName);
            _employeeService = RestService.For<IEmployeeRestService>(httpClient);
        }

        public async Task<ApiResponse<PagedResponse<GetEmployeeResponse>>> GetAllAsync(PaginationQueryRequest queryRequest = null, GetAllEmployeesQuery query = null)
        {
            var response = await _employeeService.GetAllAsync(queryRequest, query);
            return response;
        }

        public async Task<ApiResponse<Response<GetEmployeeResponse>>> CreateAsync(CreateEmployeeRequest createRequest)
        {
            var response = await _employeeService.CreateAsync(createRequest);
            return response;
        }

        public async Task<ApiResponse<Response<GetEmployeeResponse>>> UpdateAsync(Guid orderDescriptionId, UpdateEmployeeRequest updateRequest)
        {
            var response = await _employeeService.UpdateAsync(orderDescriptionId, updateRequest);
            return response;
        }

        public async Task<IApiResponse> DeleteAsync(Guid id)
        {
            return await _employeeService.DeleteAsync(id);
        }
    }
}
