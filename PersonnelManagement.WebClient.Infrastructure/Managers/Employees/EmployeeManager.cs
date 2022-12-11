using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using PersonnelManagement.Contracts.v1.Requests;
using PersonnelManagement.Contracts.v1.Requests.Employees;
using PersonnelManagement.Contracts.v1.Requests.Queries;
using PersonnelManagement.Contracts.v1.Responses;
using PersonnelManagement.Contracts.v1.Responses.Employees;
using PersonnelManagement.Sdk.Employees;
using PersonnelManagement.WebClient.Infrastructure.Managers.Identity;
using PersonnelManagement.WebClient.Options;
using Refit;
using System.Net;

namespace PersonnelManagement.WebClient.Infrastructure.Managers.Employees
{
    public class EmployeeManager : BaseManager, IEmployeeManager
    {
        private IEmployeeRestService _employeeService;

        public EmployeeManager(IHttpClientFactory httpClientFactory,
            ManagersApiOptions apiOptions, ILocalStorageService localStorage,
            IIdentityManager identityManager, AuthenticationStateProvider authenticationStateProvider)
            : base(identityManager, localStorage, authenticationStateProvider)
        {
            var httpClient = httpClientFactory.CreateClient(apiOptions.ClientName);
            _employeeService = RestService.For<IEmployeeRestService>(httpClient);
        }

        public async Task<PagedResponse<GetEmployeeResponse>> GetAllAsync(PaginationQueryRequest queryRequest = null, GetAllEmployeesQuery query = null)
        {
            try
            {
                var response = await _employeeService.GetAllAsync(queryRequest, query);

                if (response?.Error?.StatusCode == HttpStatusCode.Unauthorized)
                {
                    if (await TryRefreshTokenOrLogout())
                    {
                        response = await _employeeService.GetAllAsync(queryRequest, query);
                    }
                }

                return response?.Content;
            }
            catch (HttpRequestException)
            {
                return new PagedResponse<GetEmployeeResponse>();
            }
            catch (ApiException)
            {
                return new PagedResponse<GetEmployeeResponse>();
            }
        }

        public async Task<Response<GetEmployeeResponse>> CreateAsync(CreateEmployeeRequest createRequest)
        {
            try
            {
                var response = await _employeeService.CreateAsync(createRequest);

                if (response?.Error?.StatusCode == HttpStatusCode.Unauthorized)
                {
                    if (await TryRefreshTokenOrLogout())
                    {
                        response = await _employeeService.CreateAsync(createRequest);
                    }
                }

                return response?.Content;
            }
            catch (HttpRequestException)
            {
                return new Response<GetEmployeeResponse>(new GetEmployeeResponse());
            }
            catch (ApiException)
            {
                return new Response<GetEmployeeResponse>(new GetEmployeeResponse());
            }
        }

        public async Task<Response<GetEmployeeResponse>> UpdateAsync(Guid orderDescriptionId, UpdateEmployeeRequest updateRequest)
        {
            try
            {
                var response = await _employeeService.UpdateAsync(orderDescriptionId, updateRequest);

                if (response?.Error?.StatusCode == HttpStatusCode.Unauthorized)
                {
                    if (await TryRefreshTokenOrLogout())
                    {
                        response = await _employeeService.UpdateAsync(orderDescriptionId, updateRequest);
                    }
                }

                return response?.Content;
            }
            catch (HttpRequestException)
            {
                return new Response<GetEmployeeResponse>(new GetEmployeeResponse());
            }
            catch (ApiException)
            {
                return new Response<GetEmployeeResponse>(new GetEmployeeResponse());
            }
        }

        public async Task<IApiResponse> DeleteAsync(Guid id)
        {
            return await _employeeService.DeleteAsync(id);
        }
    }
}
