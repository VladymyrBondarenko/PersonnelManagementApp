using PersonnelManagement.Contracts.v1.Requests;
using PersonnelManagement.Contracts.v1.Requests.Orders;
using PersonnelManagement.Contracts.v1.Requests.Queries;
using PersonnelManagement.Contracts.v1.Responses;
using PersonnelManagement.Contracts.v1.Responses.OrdersDescription;
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
    public class OrderDescriptionManager : IOrderDescriptionManager
    {
        private IOrderDescriptionRestService _orderDescService;

        public OrderDescriptionManager(IHttpClientFactory httpClientFactory,
            ManagersApiOptions apiOptions)
        {
            var httpClient = httpClientFactory.CreateClient(apiOptions.ClientName);
            _orderDescService = RestService.For<IOrderDescriptionRestService>(httpClient);
        }

        public async Task<ApiResponse<Response<GetOrderDescriptionResponse>>> GetAsync(Guid id)
        {
            var response = await _orderDescService.Get(id);
            return response;
        }

        public async Task<ApiResponse<PagedResponse<GetOrderDescriptionResponse>>> GetAllAsync(PaginationQueryRequest queryRequest = null, GetAllOrderDescriptionsQuery query = null)
        {
            var response = await _orderDescService.GetAllAsync(queryRequest, query);
            return response;
        }

        public async Task<ApiResponse<Response<GetOrderDescriptionResponse>>> CreateAsync(CreateOrderDescriptionRequest createRequest)
        {
            var response = await _orderDescService.CreateAsync(createRequest);
            return response;
        }

        public async Task<ApiResponse<Response<GetOrderDescriptionResponse>>> UpdateAsync(Guid orderDescriptionId, UpdateOrderDescriptionRequest updateRequest)
        {
            var response = await _orderDescService.UpdateAsync(orderDescriptionId, updateRequest);
            return response;
        }

        public async Task<IApiResponse> DeleteAsync(Guid id)
        {
            return await _orderDescService.DeleteAsync(id);
        }
    }
}
