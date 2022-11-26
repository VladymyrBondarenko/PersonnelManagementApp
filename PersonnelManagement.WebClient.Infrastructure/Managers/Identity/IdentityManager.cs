using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using PersonnelManagement.Contracts.v1.Requests.Identity;
using PersonnelManagement.Contracts.v1.Responses;
using PersonnelManagement.Contracts.v1.Responses.Identity;
using PersonnelManagement.Sdk.Identity;
using PersonnelManagement.WebClient.Infrastructure.Authentication;
using PersonnelManagement.WebClient.Infrastructure.Constants.Storage;
using PersonnelManagement.WebClient.Options;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.WebClient.Infrastructure.Managers.Identity
{
    public class IdentityManager : IIdentityManager
    {
        private IIdentityRestService _identityService;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public IdentityManager(IHttpClientFactory httpClientFactory,
            ManagersApiOptions apiOptions, ILocalStorageService localStorageService, 
            AuthenticationStateProvider authenticationStateProvider)
        {
            var httpClient = httpClientFactory.CreateClient(apiOptions.ClientName);
            _identityService = RestService.For<IIdentityRestService>(httpClient);
            _localStorage = localStorageService;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<Response<AuthSuccessResponse>> RegisterAsync(UserRegistrationRequest registrationRequest)
        {
            try
            {
                var response = await _identityService.RegisterAsync(registrationRequest);

                if (response != null && response.IsSuccessStatusCode)
                {
                    await _localStorage.SetItemAsync(StorageConstants.AuthToken, response?.Content?.Data?.Token);
                    await _localStorage.SetItemAsync(StorageConstants.RefreshToken, response?.Content?.Data?.RefreshToken);
                }

                return response?.Content;
            }
            catch (HttpRequestException)
            {
                return new Response<AuthSuccessResponse>(new AuthSuccessResponse());
            }
            catch (ApiException)
            {
                return new Response<AuthSuccessResponse>(new AuthSuccessResponse());
            }
        }

        public async Task<Response<AuthSuccessResponse>> LoginAsync(UserLoginRequest loginRequest)
        {
            try
            {
                var response = await _identityService.LoginAsync(loginRequest);

                if (response != null && response.IsSuccessStatusCode)
                {
                    await _localStorage.SetItemAsync(StorageConstants.AuthToken, response?.Content?.Data?.Token);
                    await _localStorage.SetItemAsync(StorageConstants.RefreshToken, response?.Content?.Data?.RefreshToken);

                    await ((AuthStateProvider)_authenticationStateProvider).StateChangedAsync();
                }

                return response?.Content;
            }
            catch (HttpRequestException)
            {
                return new Response<AuthSuccessResponse>(new AuthSuccessResponse());
            }
            catch (ApiException)
            {
                return new Response<AuthSuccessResponse>(new AuthSuccessResponse());
            }
        }

        public async Task<Response<AuthSuccessResponse>> RefreshAsync(RefreshTokenRequest request)
        {
            try
            {
                var res = await _identityService.RefreshAsync(request);
                return res?.Content;
            }
            catch (HttpRequestException)
            {
                return new Response<AuthSuccessResponse>(new AuthSuccessResponse());
            }
            catch (ApiException)
            {
                return new Response<AuthSuccessResponse>(new AuthSuccessResponse());
            }
        }
    }
}
