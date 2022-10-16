using PersonnelManagement.Domain.Models;

namespace PersonnelManagement.Server.Services.UriServices
{
    public interface IUriService
    {
        Uri GetAllDepartmentsUri(PaginationQuery paginationQuery = null);
        Uri GetAllOrderDescriptionsUri(PaginationQuery paginationQuery = null);
        Uri GetAllOrdersUri(PaginationQuery paginationQuery = null);
        Uri GetAllPositionsUri(PaginationQuery paginationQuery = null);
        Uri GetDepartmentsUri(string departmentId);
        Uri GetOrderDescriptionUri(string orderDescriptionId);
        Uri GetOrderUri(string orderId);
        Uri GetPositionUri(string positionId);
    }
}