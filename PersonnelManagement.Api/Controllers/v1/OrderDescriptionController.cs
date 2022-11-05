using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.Application.Orders.Interfaces;
using PersonnelManagement.Contracts.v1.Requests;
using PersonnelManagement.Contracts.v1.Requests.Orders;
using PersonnelManagement.Contracts.v1.Requests.Queries;
using PersonnelManagement.Contracts.v1.Responses;
using PersonnelManagement.Contracts.v1.Responses.OrdersDescription;
using PersonnelManagement.Contracts.v1.Routes;
using PersonnelManagement.Domain.Models;
using PersonnelManagement.Domain.Models.Filters;
using PersonnelManagement.Domain.Orders;
using PersonnelManagement.Server.Helpers;
using PersonnelManagement.Server.Services.PaginationServices.OrderDescriptions;
using PersonnelManagement.Server.Services.UriServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PersonnelManagement.Api.Controllers.v1
{
    [ApiController]
    public class OrderDescriptionController : ControllerBase
    {
        private readonly IOrderDescriptionService _orderDescriptionService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        private readonly IOrderDescriptionPaginationService _paginationService;

        public OrderDescriptionController(IOrderDescriptionService orderDescriptionService,
            IMapper mapper, IUriService uriService, IOrderDescriptionPaginationService paginationService)
        {
            _orderDescriptionService = orderDescriptionService;
            _mapper = mapper;
            _uriService = uriService;
            _paginationService = paginationService;
        }

        // GET: api/orderDescriptions
        [HttpGet(ApiRoutes.OrderDescriptions.GetAll)]
        public async Task<IActionResult> Get([FromQuery] PaginationQueryRequest queryRequest, [FromQuery] GetAllOrderDescriptionsQuery query)
        {
            var paginationFilter = _mapper.Map<PaginationQuery>(queryRequest);
            var filter = _mapper.Map<GetAllOrderDescriptionsFilter>(query);
            var orderDescriptions = await _orderDescriptionService.GetAllAsync(paginationFilter, filter);

            var totalAmount = await _orderDescriptionService.GetOrderDescriptionsAmount();
            var response = _mapper.Map<List<GetOrderDescriptionResponse>>(orderDescriptions);

            var pagedResponse = _paginationService.CreatePaginatedResponse(paginationFilter, response, totalAmount);

            return Ok(pagedResponse);
        }

        // GET api/orderDescriptions/5
        [HttpGet(ApiRoutes.OrderDescriptions.Get)]
        public async Task<IActionResult> Get(Guid orderDescriptionId)
        {
            var orderDesc = await _orderDescriptionService.GetAsync(orderDescriptionId);
            var response = _mapper.Map<GetOrderDescriptionResponse>(orderDesc);
            return Ok(new Response<GetOrderDescriptionResponse>(response));
        }

        // POST api/orderDescriptions
        [HttpPost(ApiRoutes.OrderDescriptions.Create)]
        public async Task<IActionResult> Post([FromBody] CreateOrderDescriptionRequest createRequest)
        {
            var orderDesc = _mapper.Map<OrderDescription>(createRequest);
            var createdOrderDesc = await _orderDescriptionService.CreateAsync(orderDesc);

            if(createdOrderDesc == null)
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = new List<ErrorModel>
                    {
                        new ErrorModel
                        {
                            Message = "The order description was not created."
                        }
                    }
                });
            }

            var response = _mapper.Map<GetOrderDescriptionResponse>(createdOrderDesc);
            return Created(_uriService.GetOrderDescriptionUri(createdOrderDesc.Id.ToString()), 
                new Response<GetOrderDescriptionResponse>(response));
        }

        // PUT api/orderDescriptions/5
        [HttpPut(ApiRoutes.OrderDescriptions.Update)]
        public async Task<IActionResult> Put(Guid orderDescriptionId, [FromBody] UpdateOrderDescriptionRequest updateRequest)
        {
            var orderDesc = await _orderDescriptionService.GetAsync(orderDescriptionId);

            if (orderDesc != null)
            {
                orderDesc.OrderDescriptionTitle = updateRequest.OrderDescriptionTitle;

                if (await _orderDescriptionService.UpdateAsync(orderDesc))
                {
                    var response = _mapper.Map<GetOrderDescriptionResponse>(orderDesc);
                    return Ok(new Response<GetOrderDescriptionResponse>(response));
                }
            }

            return NotFound();
        }

        // DELETE api/orderDescriptions/5
        [HttpDelete(ApiRoutes.OrderDescriptions.Delete)]
        public async Task<IActionResult> Delete(Guid orderDescriptionId)
        {
            if (await _orderDescriptionService.DeleteAsync(orderDescriptionId))
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
