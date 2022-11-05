using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.Application.FileOperations.Originals;
using PersonnelManagement.Application.Orders.Interfaces;
using PersonnelManagement.Contracts.v1.Requests;
using PersonnelManagement.Contracts.v1.Requests.Orders;
using PersonnelManagement.Contracts.v1.Requests.Originals;
using PersonnelManagement.Contracts.v1.Requests.Queries;
using PersonnelManagement.Contracts.v1.Responses;
using PersonnelManagement.Contracts.v1.Responses.Orders;
using PersonnelManagement.Contracts.v1.Responses.Originals;
using PersonnelManagement.Contracts.v1.Routes;
using PersonnelManagement.Domain.Models;
using PersonnelManagement.Domain.Models.Filters;
using PersonnelManagement.Domain.Orders;
using PersonnelManagement.Server.Helpers;
using PersonnelManagement.Server.Services.PaginationServices.Orders;
using PersonnelManagement.Server.Services.UriServices;
using System.Reflection;

namespace PersonnelManagement.Api.Controllers.v1
{
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        private readonly IOrderPaginationService _paginationService;

        public OrderController(IOrderService orderService,
            IMapper mapper, IUriService uriService, IOrderPaginationService paginationService)
        {
            _orderService = orderService;
            _mapper = mapper;
            _uriService = uriService;
            _paginationService = paginationService;
        }

        // GET: api/orders
        [HttpGet(ApiRoutes.Orders.GetAll)]
        public async Task<IActionResult> Get([FromQuery] PaginationQueryRequest queryRequest, [FromQuery] GetAllOrdersQuery query)
        {
            var paginationFilter = _mapper.Map<PaginationQuery>(queryRequest);
            var filter = _mapper.Map<GetAllOrdersFilter>(query);
            var orders = await _orderService.GetAllAsync(paginationFilter, filter);

            var totalAmount = await _orderService.GetOrdersAmountAsync(filter);
            var response = _mapper.Map<List<GetOrderResponse>>(orders.Select(x => x.Order).ToList());

            var pagedResponse = _paginationService.CreatePaginatedResponse(paginationFilter, response, totalAmount);

            return Ok(pagedResponse);
        }

        // GET api/orders/5
        [HttpGet(ApiRoutes.Orders.Get)]
        public async Task<IActionResult> Get(Guid orderId)
        {
            var order = await _orderService.GetOrderAsync(orderId);

            var response = _mapper.Map<GetOrderResponse>(order.Order);
            return Ok(new Response<GetOrderResponse>(response));
        }

        // POST api/orders
        [HttpPost(ApiRoutes.Orders.Create)]
        public async Task<IActionResult> Post([FromBody] CreateOrderRequest createRequest)
        {
            var createdOrder = await _orderService.CreateAsync(_mapper.Map<Order>(createRequest));

            if(createdOrder == null)
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = new List<ErrorModel>
                    {
                        new ErrorModel
                        {
                            Message = "The order was not created."
                        }
                    }
                });
            }

            var response = _mapper.Map<GetOrderResponse>(createdOrder.Order);
            return Created(_uriService.GetOrderUri(createdOrder.Order.Id.ToString()), new Response<GetOrderResponse>(response));
        }

        /// <summary>
        /// POST api/orders/accept/5
        /// Accept order in the system
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpPost(ApiRoutes.Orders.AcceptOrder)]
        public async Task<IActionResult> AcceptOrder(Guid orderId)
        {
            var order = await _orderService.GetOrderAsync(orderId);

            if (order == null)
            {
                return NotFound();
            }

            var success = await order.AcceptOrderAsync();

            var response = new AcceptOrderSuccessResponse { Success = success };
            return Ok(new Response<AcceptOrderSuccessResponse>(response));
        }

        // PUT api/orders/5
        [HttpPut(ApiRoutes.Orders.Update)]
        public async Task<IActionResult> Put(Guid orderId, [FromBody] UpdateOrderRequest updateRequest)
        {
            var orderInfo = await _orderService.GetOrderAsync(orderId);

            if (orderInfo != null)
            {
                var order = orderInfo.Order;
                order.FirstName = updateRequest.FirstName;
                order.LastName = updateRequest.LastName;
                order.DateFrom = updateRequest.DateFrom;
                order.DateTo = updateRequest.DateTo;
                order.PositionId = updateRequest.PositionId;
                order.DepartmentId = updateRequest.DepartmentId;

                if (await _orderService.UpdateAsync(order))
                {
                    var response = _mapper.Map<GetOrderResponse>(order);
                    return Ok(new Response<GetOrderResponse>(response));
                }
            }

            return NotFound();
        }

        // DELETE api/orders/5
        [HttpDelete(ApiRoutes.Orders.Delete)]
        public async Task<IActionResult> Delete(Guid orderId)
        {
            if (await _orderService.DeleteAsync(orderId))
            {
                return NoContent();
            }

            return NotFound();
        }

        /// <summary>
        /// POST api/orders/rollback/5
        /// Rollback order in the system
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpPost(ApiRoutes.Orders.RollbackOrder)]
        public async Task<IActionResult> RollbackOrder(Guid orderId)
        {
            var order = await _orderService.GetOrderAsync(orderId);

            if (order == null)
            {
                return NotFound();
            }

            var success = await order.RollbackOrderAsync();

            var response = new AcceptOrderSuccessResponse { Success = success };
            return Ok(new Response<AcceptOrderSuccessResponse>(response));
        }
    }
}
