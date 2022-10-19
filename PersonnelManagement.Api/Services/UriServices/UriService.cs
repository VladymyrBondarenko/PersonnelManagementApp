using Microsoft.AspNetCore.WebUtilities;
using PersonnelManagement.Contracts.v1.Routes;
using PersonnelManagement.Domain.Models;

namespace PersonnelManagement.Server.Services.UriServices
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

        public Uri GetPositionUri(string positionId)
        {
            return new Uri($"{_baseUri}/{ApiRoutes.Positions.Get.Replace("{positionId}", positionId)}");
        }

        public Uri GetOrderUri(string orderId)
        {
            return new Uri($"{_baseUri}/{ApiRoutes.Orders.Get.Replace("{orderId}", orderId)}");
        }

        public Uri GetOrderDescriptionUri(string orderDescriptionId)
        {
            return new Uri($"{_baseUri}/{ApiRoutes.OrderDescriptions.Get.Replace("{orderDescriptionId}", orderDescriptionId)}");
        }
        public Uri GetEmployeeUri(string employeeId)
        {
            return new Uri($"{_baseUri}/{ApiRoutes.Employees.Get.Replace("{employeeId}", employeeId)}");
        }

        public Uri GetAllDepartmentsUri(PaginationQuery paginationQuery = null)
        {
            return getAllUri(ApiRoutes.Departments.GetAll, paginationQuery);
        }

        public Uri GetAllPositionsUri(PaginationQuery paginationQuery = null)
        {
            return getAllUri(ApiRoutes.Positions.GetAll, paginationQuery);
        }

        public Uri GetAllOrderDescriptionsUri(PaginationQuery paginationQuery = null)
        {
            return getAllUri(ApiRoutes.OrderDescriptions.GetAll, paginationQuery);
        }

        public Uri GetAllOrdersUri(PaginationQuery paginationQuery = null)
        {
            return getAllUri(ApiRoutes.Orders.GetAll, paginationQuery);
        }

        public Uri GetAllEmployeesUri(PaginationQuery paginationQuery = null)
        {
            return getAllUri(ApiRoutes.Employees.GetAll, paginationQuery);
        }

        private Uri getAllUri(string route, PaginationQuery paginationQuery = null)
        {
            if (paginationQuery == null)
            {
                return new Uri(_baseUri);
            }

            var uri = $"{_baseUri}/{route}";

            var modifiedUri = QueryHelpers.AddQueryString(uri, "pageNumber", paginationQuery.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", paginationQuery.PageSize.ToString());

            return new Uri(modifiedUri);
        }
    }
}
