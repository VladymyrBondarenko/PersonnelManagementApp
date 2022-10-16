﻿using PersonnelManagement.Contracts.v1.Responses;
using PersonnelManagement.Domain.Models;
using PersonnelManagement.Server.Services.UriServices;

namespace PersonnelManagement.Server.Services.PaginationServices.Departments
{
    public class DepartmentPaginationService : IDepartmentPaginationService
    {
        private readonly IUriService _uriService;

        public DepartmentPaginationService(IUriService uriService)
        {
            _uriService = uriService;
        }

        public PagedResponse<T> CreatePaginatedResponse<T>(PaginationQuery paginationQuery, List<T> response, int totalAmount)
        {
            var nextPage = paginationQuery.PageNumber >= 1
                ? _uriService.GetAllDepartmentsUri(new PaginationQuery(paginationQuery.PageNumber + 1, paginationQuery.PageSize)).ToString()
                : null;

            var previousPage = paginationQuery.PageNumber - 1 >= 1
                ? _uriService.GetAllDepartmentsUri(new PaginationQuery(paginationQuery.PageNumber - 1, paginationQuery.PageSize)).ToString()
                : null;

            return new PagedResponse<T>
            {
                Data = response,
                PageSize = paginationQuery.PageSize,
                PageNumber = paginationQuery.PageNumber,
                NextPage = nextPage,
                PreviousPage = previousPage,
                TotalAmount = totalAmount
            };
        }
    }
}
