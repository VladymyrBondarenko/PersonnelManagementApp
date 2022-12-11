using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using PersonnelManagement.Contracts.v1.Requests.Identity;
using PersonnelManagement.Contracts.v1.Responses;
using PersonnelManagement.WebClient.Infrastructure.Authentication;
using PersonnelManagement.WebClient.Infrastructure.Constants.Storage;
using PersonnelManagement.WebClient.Infrastructure.Managers.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.WebClient.Infrastructure.Managers
{
    public abstract class BaseManager
    {
        private readonly IIdentityManager _identityManager;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public BaseManager(IIdentityManager identityManager,
            ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider)
        {
            _identityManager = identityManager;
            _localStorage = localStorage;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async virtual Task<bool> TryRefreshTokenOrLogout()
        {
            var token = await _localStorage.GetItemAsync<string>(StorageConstants.AuthToken);
            var refreshToken = await _localStorage.GetItemAsync<string>(StorageConstants.RefreshToken);

            var res = await _identityManager.RefreshAsync(new RefreshTokenRequest
            {
                Token = token,
                RefreshToken = new Guid(refreshToken)
            });
            if (!string.IsNullOrWhiteSpace(res?.Data?.Token))
            {
                await _localStorage.SetItemAsync(StorageConstants.AuthToken, res.Data.Token);
                await _localStorage.SetItemAsync(StorageConstants.RefreshToken, res.Data.RefreshToken);
                await ((AuthStateProvider)_authenticationStateProvider).StateChangedAsync();

                return true;
            }
            else
            {
                await _identityManager.LogoutAsync();
                return false;
            }
        }
    }
}
