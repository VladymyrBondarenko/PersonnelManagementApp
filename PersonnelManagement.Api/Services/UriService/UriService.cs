using Microsoft.AspNetCore.WebUtilities;
using PersonnelManagement.Contracts.v1.Routes;
using PersonnelManagement.Domain.Models;

namespace PersonnelManagement.Server.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;

        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri GetDepartmentsUri(string departmentId)
        {
            return new Uri($"{_baseUri}/{ApiRoutes.Departments.Get.Replace("{departmentId}", departmentId)}");
        }

        public Uri GetAllDepartmentsUri(PaginationQuery paginationQuery = null)
        {
            if (paginationQuery == null)
            {
                return new Uri(_baseUri);
            }

            var uri = $"{_baseUri}/{ApiRoutes.Departments.GetAll}";

            var modifiedUri = QueryHelpers.AddQueryString(uri, "pageNumber", paginationQuery.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", paginationQuery.PageSize.ToString());

            return new Uri(modifiedUri);
        }
    }
}
