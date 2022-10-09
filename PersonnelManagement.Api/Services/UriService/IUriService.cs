using PersonnelManagement.Domain.Models;

namespace PersonnelManagement.Server.Services
{
    public interface IUriService
    {
        Uri GetAllDepartmentsUri(PaginationQuery paginationQuery = null);
        Uri GetDepartmentsUri(string departmentId);
    }
}