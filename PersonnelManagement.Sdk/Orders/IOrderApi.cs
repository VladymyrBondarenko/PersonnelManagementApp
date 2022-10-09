using PersonnelManagement.Contracts.v1.Responses;
using PersonnelManagement.Contracts.v1.Responses.Orders;
using PersonnelManagement.Contracts.v1.Routes;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Sdk.Orders
{
    public interface IOrderApi
    {
        [Get($"/{ApiRoutes.Orders.GetAll}")]
        Task<ApiResponse<Response<List<GetOrderResponse>>>> GetAllAsync();
    }
}
