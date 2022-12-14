using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using PersonnelManagement.Contracts.v1.Requests;
using PersonnelManagement.Contracts.v1.Requests.Originals;
using PersonnelManagement.Contracts.v1.Requests.Queries;
using PersonnelManagement.Contracts.v1.Responses;
using PersonnelManagement.Contracts.v1.Responses.Originals;
using PersonnelManagement.Contracts.v1.Routes;
using PersonnelManagement.Sdk.Originals;
using PersonnelManagement.WebClient.Infrastructure.Managers.Identity;
using PersonnelManagement.WebClient.Options;
using Refit;
using System.Net;

namespace PersonnelManagement.WebClient.Infrastructure.Managers.Originals
{
    public class OriginalManager : BaseManager, IOriginalManager
    {
        private IOriginalRestService _originalService;
        private HttpClient _httpClient;

        public OriginalManager(IHttpClientFactory httpClientFactory,
            ManagersApiOptions apiOptions, IIdentityManager identityManager,
            ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider)
            : base(identityManager, localStorage, authenticationStateProvider)
        {
            _httpClient = httpClientFactory.CreateClient(apiOptions.ClientName);
            _originalService = RestService.For<IOriginalRestService>(_httpClient);
        }

        public async Task<PagedResponse<GetOriginalResponse>> GetAllAsync(PaginationQueryRequest queryRequest = null, GetAllOriginalsQuery query = null)
        {
            try
            {
                var response = await _originalService.GetAllAsync(queryRequest, query);

                if (response?.Error?.StatusCode == HttpStatusCode.Unauthorized)
                {
                    if (await TryRefreshTokenOrLogout())
                    {
                        response = await _originalService.GetAllAsync(queryRequest, query);
                    }
                }

                return response?.Content;
            }
            catch (HttpRequestException)
            {
                return new PagedResponse<GetOriginalResponse>();
            }
            catch (ApiException)
            {
                return new PagedResponse<GetOriginalResponse>();
            }
        }

        public async Task<Response<GetOriginalResponse>> GetAsync(Guid originalId)
        {
            try
            {
                var response = await _originalService.GetAsync(originalId);

                if (response?.Error?.StatusCode == HttpStatusCode.Unauthorized)
                {
                    if (await TryRefreshTokenOrLogout())
                    {
                        response = await _originalService.GetAsync(originalId);
                    }
                }

                return response?.Content;
            }
            catch (HttpRequestException)
            {
                return new Response<GetOriginalResponse>(new GetOriginalResponse());
            }
            catch (ApiException)
            {
                return new Response<GetOriginalResponse>(new GetOriginalResponse());
            }
        }

        public string GetFileDownloadEndpoint(Guid originalId, string token)
        {
            return _httpClient.BaseAddress + 
                $"{ApiRoutes.Originals.DownloadFile.Replace("{originalId}", originalId.ToString())}?token={token}";
        }

        public async Task<Response<GetOriginalResponse>> UpdateAsync(Guid originalId, UpdateOriginalRequest updateRequest)
        {
            try
            {
                var response = await _originalService.UpdateAsync(originalId, updateRequest);

                if (response?.Error?.StatusCode == HttpStatusCode.Unauthorized)
                {
                    if (await TryRefreshTokenOrLogout())
                    {
                        response = await _originalService.UpdateAsync(originalId, updateRequest);
                    }
                }

                return response?.Content;
            }
            catch (HttpRequestException)
            {
                return new Response<GetOriginalResponse>(new GetOriginalResponse());
            }
            catch (ApiException)
            {
                return new Response<GetOriginalResponse>(new GetOriginalResponse());
            }
        }

        public async Task<Response<GetOriginalResponse>> CreateAsync(int originalEntity, int originalType, Guid entityId, StreamPart file)
        {
            try
            {
                var response = await _originalService.CreateAsync(originalEntity, originalType, entityId, file);

                if (response?.Error?.StatusCode == HttpStatusCode.Unauthorized)
                {
                    if (await TryRefreshTokenOrLogout())
                    {
                        response = await _originalService.CreateAsync(originalEntity, originalType, entityId, file);
                    }
                }

                return response?.Content;
            }
            catch (HttpRequestException)
            {
                return new Response<GetOriginalResponse>(new GetOriginalResponse());
            }
            catch (ApiException)
            {
                return new Response<GetOriginalResponse>(new GetOriginalResponse());
            }
        }

        public async Task<IApiResponse> DeleteAsync(Guid originalId)
        {
            return await _originalService.DeleteAsync(originalId);
        }
    }
}
