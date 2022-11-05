using PersonnelManagement.Contracts.v1.Requests;
using PersonnelManagement.Contracts.v1.Requests.Originals;
using PersonnelManagement.Contracts.v1.Requests.Queries;
using PersonnelManagement.Contracts.v1.Responses;
using PersonnelManagement.Contracts.v1.Responses.Originals;
using PersonnelManagement.Contracts.v1.Routes;
using PersonnelManagement.Sdk.Originals;
using PersonnelManagement.WebClient.Options;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.WebClient.Infrastructure.Managers.Originals
{
    public class OriginalManager : IOriginalManager
    {
        private IOriginalRestService _originalService;
        private HttpClient _httpClient;

        public OriginalManager(IHttpClientFactory httpClientFactory,
            ManagersApiOptions apiOptions)
        {
            _httpClient = httpClientFactory.CreateClient(apiOptions.ClientName);
            _originalService = RestService.For<IOriginalRestService>(_httpClient);
        }

        public async Task<ApiResponse<PagedResponse<GetOriginalResponse>>> GetAllAsync(PaginationQueryRequest queryRequest = null, GetAllOriginalsQuery query = null)
        {
            return await _originalService.GetAllAsync(queryRequest, query);
        }

        public async Task<ApiResponse<Response<GetOriginalResponse>>> GetAsync(Guid originalId)
        {
            return await _originalService.GetAsync(originalId);
        }

        public string GetFileDownloadEndpoint(Guid originalId)
        {
            return _httpClient.BaseAddress + $"{ApiRoutes.Originals.DownloadFile.Replace("{originalId}", originalId.ToString())}";
        }

        public async Task<ApiResponse<Response<GetOriginalResponse>>> UpdateAsync(Guid originalId, UpdateOriginalRequest updateRequest)
        {
            return await _originalService.UpdateAsync(originalId, updateRequest);
        }

        public async Task<ApiResponse<Response<GetOriginalResponse>>> CreateAsync(int originalEntity, Guid entityId, StreamPart file)
        {
            return await _originalService.CreateAsync(originalEntity, entityId, file);
        }

        public async Task<IApiResponse> DeleteAsync(Guid originalId)
        {
            return await _originalService.DeleteAsync(originalId);
        }
    }
}
