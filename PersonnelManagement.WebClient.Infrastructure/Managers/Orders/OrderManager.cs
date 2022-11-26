using PersonnelManagement.Contracts.v1.Requests;
using PersonnelManagement.Contracts.v1.Requests.Orders;
using PersonnelManagement.Contracts.v1.Requests.Originals;
using PersonnelManagement.Contracts.v1.Requests.Queries;
using PersonnelManagement.Contracts.v1.Responses;
using PersonnelManagement.Contracts.v1.Responses.Orders;
using PersonnelManagement.Contracts.v1.Responses.Originals;
using PersonnelManagement.Sdk.Orders;
using PersonnelManagement.WebClient.Options;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.WebClient.Infrastructure.Managers.Orders
{
    public class OrderManager : IOrderManager
    {
        private IOrderRestService _orderService;

        public OrderManager(IHttpClientFactory httpClientFactory,
            ManagersApiOptions apiOptions)
        {
            var httpClient = httpClientFactory.CreateClient(apiOptions.ClientName);
            _orderService = RestService.For<IOrderRestService>(httpClient);
        }

        public async Task<PagedResponse<GetOrderResponse>> GetAllAsync(PaginationQueryRequest queryRequest = null, GetAllOrdersQuery query = null)
        {
            try
            {
                var response = await _orderService.GetAllAsync(queryRequest, query);
                return response?.Content;
            }
            catch (HttpRequestException)
            {
                return new PagedResponse<GetOrderResponse>();
            }
            catch (ApiException)
            {
                return new PagedResponse<GetOrderResponse>();
            }
        }

        public async Task<Response<GetOrderResponse>> CreateAsync(CreateOrderRequest createRequest)
        {
            try
            {
                var response = await _orderService.CreateAsync(createRequest);
                return response?.Content;
            }
            catch (HttpRequestException)
            {
                return new Response<GetOrderResponse>(new GetOrderResponse());
            }
            catch (ApiException)
            {
                return new Response<GetOrderResponse>(new GetOrderResponse());
            }
        }

        public async Task<Response<GetOrderResponse>> UpdateAsync(Guid orderId, UpdateOrderRequest updateRequest)
        {
            try
            {
                var response = await _orderService.UpdateAsync(orderId, updateRequest);
                return response?.Content;
            }
            catch (HttpRequestException)
            {
                return new Response<GetOrderResponse>(new GetOrderResponse());
            }
            catch (ApiException)
            {
                return new Response<GetOrderResponse>(new GetOrderResponse());
            }
        }

        public async Task<IApiResponse> DeleteAsync(Guid id)
        {
            return await _orderService.DeleteAsync(id);
        }

        public async Task<Response<AcceptOrderSuccessResponse>> AcceptOrder(Guid orderId)
        {
            try
            {
                var response = await _orderService.AcceptOrder(orderId);
                return response?.Content;
            }
            catch (HttpRequestException)
            {
                return new Response<AcceptOrderSuccessResponse>(new AcceptOrderSuccessResponse { Success = false });
            }
            catch (ApiException)
            {
                return new Response<AcceptOrderSuccessResponse>(new AcceptOrderSuccessResponse { Success = false });
            }
        }

        public async Task<Response<AcceptOrderSuccessResponse>> RollbackOrder(Guid orderId)
        {
            try
            {
                var response = await _orderService.RollbackOrder(orderId);
                return response?.Content;
            }
            catch (HttpRequestException)
            {
                return new Response<AcceptOrderSuccessResponse>(new AcceptOrderSuccessResponse { Success = false });
            }
            catch (ApiException)
            {
                return new Response<AcceptOrderSuccessResponse>(new AcceptOrderSuccessResponse { Success = false });
            }
        }
    }
}
