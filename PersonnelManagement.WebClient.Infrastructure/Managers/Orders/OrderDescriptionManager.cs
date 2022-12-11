using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using PersonnelManagement.Contracts.v1.Requests;
using PersonnelManagement.Contracts.v1.Requests.Orders;
using PersonnelManagement.Contracts.v1.Requests.Queries;
using PersonnelManagement.Contracts.v1.Responses;
using PersonnelManagement.Contracts.v1.Responses.OrdersDescription;
using PersonnelManagement.Sdk.Orders;
using PersonnelManagement.WebClient.Infrastructure.Managers.Identity;
using PersonnelManagement.WebClient.Options;
using Refit;
using System.Net;

namespace PersonnelManagement.WebClient.Infrastructure.Managers.Orders
{
    public class OrderDescriptionManager : BaseManager, IOrderDescriptionManager
    {
        private readonly IOrderDescriptionRestService _orderDescService;

        public OrderDescriptionManager(IHttpClientFactory httpClientFactory,
            ManagersApiOptions apiOptions, IIdentityManager identityManager,
            ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider) 
            : base(identityManager, localStorage, authenticationStateProvider)
        {
            var httpClient = httpClientFactory.CreateClient(apiOptions.ClientName);
            _orderDescService = RestService.For<IOrderDescriptionRestService>(httpClient);
        }

        public async Task<Response<GetOrderDescriptionResponse>> GetAsync(Guid id)
        {
            try
            {
                var response = await _orderDescService.Get(id);

                if (response?.Error?.StatusCode == HttpStatusCode.Unauthorized)
                {
                    if (await TryRefreshTokenOrLogout())
                    {
                        response = await _orderDescService.Get(id);
                    }
                }

                return response?.Content;
            }
            catch (HttpRequestException)
            {
                return new Response<GetOrderDescriptionResponse>(new GetOrderDescriptionResponse());
            }
            catch (ApiException)
            {
                return new Response<GetOrderDescriptionResponse>(new GetOrderDescriptionResponse());
            }
        }

        public async Task<PagedResponse<GetOrderDescriptionResponse>> GetAllAsync(PaginationQueryRequest queryRequest = null, GetAllOrderDescriptionsQuery query = null)
        {
            try
            {
                var response = await _orderDescService.GetAllAsync(queryRequest, query);

                if(response?.Error?.StatusCode == HttpStatusCode.Unauthorized)
                {
                    if (await TryRefreshTokenOrLogout())
                    {
                        response = await _orderDescService.GetAllAsync(queryRequest, query);
                    }
                }

                return response?.Content;
            }
            catch (HttpRequestException)
            {
                return new PagedResponse<GetOrderDescriptionResponse>();
            }
            catch (ApiException)
            {
                return new PagedResponse<GetOrderDescriptionResponse>();
            }
        }

        public async Task<Response<GetOrderDescriptionResponse>> CreateAsync(CreateOrderDescriptionRequest createRequest)
        {
            try
            {
                var response = await _orderDescService.CreateAsync(createRequest);

                if (response?.Error?.StatusCode == HttpStatusCode.Unauthorized)
                {
                    if (await TryRefreshTokenOrLogout())
                    {
                        response = await _orderDescService.CreateAsync(createRequest);
                    }
                }

                return response?.Content;
            }
            catch (HttpRequestException)
            {
                return new Response<GetOrderDescriptionResponse>(new GetOrderDescriptionResponse());
            }
            catch (ApiException)
            {
                return new Response<GetOrderDescriptionResponse>(new GetOrderDescriptionResponse());
            }
        }

        public async Task<Response<GetOrderDescriptionResponse>> UpdateAsync(Guid orderDescriptionId, UpdateOrderDescriptionRequest updateRequest)
        {
            try
            {
                var response = await _orderDescService.UpdateAsync(orderDescriptionId, updateRequest);

                if (response?.Error?.StatusCode == HttpStatusCode.Unauthorized)
                {
                    if (await TryRefreshTokenOrLogout())
                    {
                        response = await _orderDescService.UpdateAsync(orderDescriptionId, updateRequest);
                    }
                }

                return response?.Content;
            }
            catch (HttpRequestException)
            {
                return new Response<GetOrderDescriptionResponse>(new GetOrderDescriptionResponse());
            }
            catch (ApiException)
            {
                return new Response<GetOrderDescriptionResponse>(new GetOrderDescriptionResponse());
            }
        }

        public async Task<IApiResponse> DeleteAsync(Guid id)
        {
            return await _orderDescService.DeleteAsync(id);
        }
    }
}
