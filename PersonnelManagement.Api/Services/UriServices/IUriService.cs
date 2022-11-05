using PersonnelManagement.Domain.Models;

namespace PersonnelManagement.Server.Services.UriServices
{
    public interface IUriService
    {
        Uri GetAllDepartmentsUri(PaginationQuery paginationQuery = null);
        Uri GetAllEmployeesUri(PaginationQuery paginationQuery = null);
        Uri GetAllOrderDescriptionsUri(PaginationQuery paginationQuery = null);
        Uri GetAllOrdersUri(PaginationQuery paginationQuery = null);
        Uri GetAllOriginalsUri(PaginationQuery paginationQuery = null);
        Uri GetAllPositionsUri(PaginationQuery paginationQuery = null);
        Uri GetDepartmentsUri(string departmentId);
        Uri GetEmployeeUri(string employeeId);
        Uri GetOrderDescriptionUri(string orderDescriptionId);
        Uri GetOrderUri(string orderId);
        Uri GetOriginalUri(string originalId);
        Uri GetPositionUri(string positionId);
    }
}