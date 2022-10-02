using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.Application.Orders.Interfaces;
using PersonnelManagement.Contracts.v1.Requests.Orders;
using PersonnelManagement.Contracts.v1.Responses;
using PersonnelManagement.Contracts.v1.Responses.Orders;
using PersonnelManagement.Contracts.v1.Routes;
using PersonnelManagement.Domain.Orders;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PersonnelManagement.Api.Controllers.v1
{
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService,
            IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        // GET: api/<OrderController>
        [HttpGet(ApiRoutes.Orders.GetAll)]
        public async Task<IActionResult> Get()
        {
            var orders = await _orderService.GetAllAsync();

            var response = _mapper.Map<List<GetOrderResponse>>(orders.Select(x => x.Order).ToList());
            return Ok(new Response<List<GetOrderResponse>>(response));
        }

        // GET api/<OrderController>/5
        [HttpGet(ApiRoutes.Orders.Get)]
        public async Task<IActionResult> Get(Guid orderId)
        {
            var order = await _orderService.GetOrderAsync(orderId);

            var response = _mapper.Map<GetOrderResponse>(order.Order);
            return Ok(new Response<GetOrderResponse>(response));
        }

        // POST api/<OrderController>
        [HttpPost(ApiRoutes.Orders.Create)]
        public async Task<IActionResult> Post([FromBody] CreateOrderRequest createRequest)
        {
            var createdOrder = await _orderService.CreateAsync(_mapper.Map<Order>(createRequest));

            if(createdOrder == null)
            {
                return BadRequest();
            }

            var response = _mapper.Map<GetOrderResponse>(createdOrder.Order);
            return Ok(new Response<GetOrderResponse>(response));
        }

        /// <summary>
        /// POST api/<OrderController>/5
        /// Accept order in the system
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpPost(ApiRoutes.Orders.AcceptOrder)]
        public async Task<IActionResult> Post(Guid orderId)
        {
            var order = await _orderService.GetOrderAsync(orderId);

            if (order == null)
            {
                return BadRequest();
            }

            var success = await order.AcceptOrderAsync();

            var response = new AcceptOrderSuccessResponse { Success = success };
            return Ok(new Response<AcceptOrderSuccessResponse>(response));
        }
    }
}
