using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using PersonnelManagement.Contracts.v1.Requests;
using PersonnelManagement.Contracts.v1.Requests.Queries;
using PersonnelManagement.Contracts.v1.Responses;
using PersonnelManagement.Contracts.v1.Responses.Departments;
using PersonnelManagement.Sdk.Departments;
using PersonnelManagement.WebClient.Infrastructure.Managers.Identity;
using PersonnelManagement.WebClient.Options;
using Refit;
using System.Net;

namespace PersonnelManagement.WebClient.Infrastructure.Managers.Departments
{
    public class DepartmentManager : BaseManager, IDepartmentManager
    {
        private IDepartmentRestService _departmentService;

        public DepartmentManager(IHttpClientFactory httpClientFactory,
            ManagersApiOptions apiOptions, ILocalStorageService localStorage,
            IIdentityManager identityManager, AuthenticationStateProvider authenticationStateProvider)
            : base(identityManager, localStorage, authenticationStateProvider)
        {
            var httpClient = httpClientFactory.CreateClient(apiOptions.ClientName);
            _departmentService = RestService.For<IDepartmentRestService>(httpClient);
        }

        public async Task<PagedResponse<GetDepartmentResponse>> GetAllAsync(PaginationQueryRequest queryRequest = null, GetAllDepartmentsQuery query = null)
        {
            try
            {
                var response = await _departmentService.GetAllAsync(queryRequest, query);

                if (response?.Error?.StatusCode == HttpStatusCode.Unauthorized)
                {
                    if (await TryRefreshTokenOrLogout())
                    {
                        response = await _departmentService.GetAllAsync(queryRequest, query);
                    }
                }

                return response?.Content;
            }
            catch (HttpRequestException)
            {
                return new PagedResponse<GetDepartmentResponse>();
            }
            catch (ApiException)
            {
                return new PagedResponse<GetDepartmentResponse>();
            }
        }

        public async Task<Response<GetDepartmentResponse>> CreateAsync(CreateDepartmentRequest createRequest)
        {
            try
            {
                var response = await _departmentService.CreateAsync(createRequest);

                if (response?.Error?.StatusCode == HttpStatusCode.Unauthorized)
                {
                    if (await TryRefreshTokenOrLogout())
                    {
                        response = await _departmentService.CreateAsync(createRequest);
                    }
                }

                return response?.Content;
            }
            catch (HttpRequestException)
            {
                return new Response<GetDepartmentResponse>(new GetDepartmentResponse());
            }
            catch (ApiException)
            {
                return new Response<GetDepartmentResponse>(new GetDepartmentResponse());
            }
        }

        public async Task<Response<GetDepartmentResponse>> UpdateAsync(Guid departmentId, UpdateDepartmentRequest updateRequest)
        {
            try
            {
                var response = await _departmentService.UpdateAsync(departmentId, updateRequest);

                if (response?.Error?.StatusCode == HttpStatusCode.Unauthorized)
                {
                    if (await TryRefreshTokenOrLogout())
                    {
                        response = await _departmentService.UpdateAsync(departmentId, updateRequest);
                    }
                }

                return response?.Content;
            }
            catch (HttpRequestException)
            {
                return new Response<GetDepartmentResponse>(new GetDepartmentResponse());
            }
            catch (ApiException)
            {
                return new Response<GetDepartmentResponse>(new GetDepartmentResponse());
            }
        }

        public async Task<IApiResponse> DeleteAsync(Guid id)
        {
            return await _departmentService.DeleteAsync(id);
        }
    }
}
