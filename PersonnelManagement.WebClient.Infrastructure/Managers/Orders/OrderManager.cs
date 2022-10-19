using PersonnelManagement.Contracts.v1.Requests;
using PersonnelManagement.Contracts.v1.Requests.Orders;
using PersonnelManagement.Contracts.v1.Requests.Queries;
using PersonnelManagement.Contracts.v1.Responses;
using PersonnelManagement.Contracts.v1.Responses.Orders;
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

        public async Task<ApiResponse<PagedResponse<GetOrderResponse>>> GetAllAsync(PaginationQueryRequest queryRequest = null, GetAllOrdersQuery query = null)
        {
            var response = await _orderService.GetAllAsync(queryRequest, query);
            return response;
        }

        public async Task<ApiResponse<Response<GetOrderResponse>>> CreateAsync(CreateOrderRequest createRequest)
        {
            var response = await _orderService.CreateAsync(createRequest);
            return response;
        }

        public async Task<ApiResponse<Response<GetOrderResponse>>> UpdateAsync(Guid orderDescriptionId, UpdateOrderRequest updateRequest)
        {
            var response = await _orderService.UpdateAsync(orderDescriptionId, updateRequest);
            return response;
        }

        public async Task<IApiResponse> DeleteAsync(Guid id)
        {
            return await _orderService.DeleteAsync(id);
        }

        public async Task<ApiResponse<Response<AcceptOrderSuccessResponse>>> AcceptOrder(Guid orderId)
        {
            var response = await _orderService.AcceptOrder(orderId);
            return response;
        }

        public async Task<ApiResponse<Response<AcceptOrderSuccessResponse>>> RollbackOrder(Guid orderId)
        {
            var response = await _orderService.RollbackOrder(orderId);
            return response;
        }
    }
}
