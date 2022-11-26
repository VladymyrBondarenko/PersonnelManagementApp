using PersonnelManagement.Contracts.v1.Requests;
using PersonnelManagement.Contracts.v1.Requests.Positions;
using PersonnelManagement.Contracts.v1.Requests.Queries;
using PersonnelManagement.Contracts.v1.Responses;
using PersonnelManagement.Contracts.v1.Responses.Positions;
using PersonnelManagement.Sdk.Positions;
using PersonnelManagement.WebClient.Options;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.WebClient.Infrastructure.Managers.Positions
{
    public class PositionManager : IPositionManager
    {
        private IPositionRestService _positionService;

        public PositionManager(IHttpClientFactory httpClientFactory,
            ManagersApiOptions apiOptions)
        {
            var httpClient = httpClientFactory.CreateClient(apiOptions.ClientName);
            _positionService = RestService.For<IPositionRestService>(httpClient);
        }

        public async Task<PagedResponse<GetPositionResponse>> GetAllAsync(PaginationQueryRequest queryRequest = null, GetAllPositionsQuery query = null)
        {
            try
            {
                var response = await _positionService.GetAllAsync(queryRequest, query);
                return response?.Content;
            }
            catch (HttpRequestException)
            {
                return new PagedResponse<GetPositionResponse>();
            }
            catch (ApiException)
            {
                return new PagedResponse<GetPositionResponse>();
            }
        }

        public async Task<Response<GetPositionResponse>> CreateAsync(CreatePositionRequest createRequest)
        {
            try
            {
                var response = await _positionService.CreateAsync(createRequest);
                return response?.Content;
            }
            catch (HttpRequestException)
            {
                return new Response<GetPositionResponse>(new GetPositionResponse());
            }
            catch (ApiException)
            {
                return new Response<GetPositionResponse>(new GetPositionResponse());
            }
        }

        public async Task<Response<GetPositionResponse>> UpdateAsync(Guid positionId, UpdatePositionRequest updateRequest)
        {
            try
            {
                var response = await _positionService.UpdateAsync(positionId, updateRequest);
                return response?.Content;
            }
            catch (HttpRequestException)
            {
                return new Response<GetPositionResponse>(new GetPositionResponse());
            }
            catch (ApiException)
            {
                return new Response<GetPositionResponse>(new GetPositionResponse());
            }
        }

        public async Task<IApiResponse> DeleteAsync(Guid id)
        {
            return await _positionService.DeleteAsync(id);
        }
    }
}
