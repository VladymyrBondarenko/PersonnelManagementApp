using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.Application.FileOperations.Originals;
using PersonnelManagement.Application.Orders.Interfaces;
using PersonnelManagement.Contracts.v1.Requests.Orders;
using PersonnelManagement.Contracts.v1.Requests.Originals;
using PersonnelManagement.Contracts.v1.Responses;
using PersonnelManagement.Contracts.v1.Responses.Orders;
using PersonnelManagement.Contracts.v1.Responses.Originals;
using PersonnelManagement.Contracts.v1.Routes;
using PersonnelManagement.Domain.Orders;
using System.Reflection;

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

        // GET: api/orders
        [HttpGet(ApiRoutes.Orders.GetAll)]
        public async Task<IActionResult> Get()
        {
            var orders = await _orderService.GetAllAsync();

            var response = _mapper.Map<List<GetOrderResponse>>(orders.Select(x => x.Order).ToList());
            return Ok(new Response<List<GetOrderResponse>>(response));
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
                return BadRequest();
            }

            var response = _mapper.Map<GetOrderResponse>(createdOrder.Order);
            return Ok(new Response<GetOrderResponse>(response));
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
                return BadRequest();
            }

            var success = await order.AcceptOrderAsync();

            var response = new AcceptOrderSuccessResponse { Success = success };
            return Ok(new Response<AcceptOrderSuccessResponse>(response));
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
                return BadRequest();
            }

            var success = await order.RollbackOrderAsync();

            var response = new AcceptOrderSuccessResponse { Success = success };
            return Ok(new Response<AcceptOrderSuccessResponse>(response));
        }

        /// <summary>
        /// POST api/orders/originals/add
        /// Attach file to order
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpPost(ApiRoutes.Orders.AttachFileToOrder)]
        public async Task<IActionResult> AttachFileToOrder([FromForm] AttachFileToOrderRequest attachRequest)
        {
            if (attachRequest == null || attachRequest.File == null || attachRequest.OrderId == Guid.Empty) // move to request validator
            {
                return BadRequest();
            }

            string executableLocation = Path.GetDirectoryName(
               Assembly.GetExecutingAssembly().Location);
            var filePath = Path.Combine(executableLocation, attachRequest.File.FileName);

            using var stream = System.IO.File.Create(filePath);
            attachRequest.File.CopyTo(stream);
            stream.Close();

            var original = await _orderService.AddOriginalAsync(new OriginalCreateParams 
            { 
                FileName = attachRequest.File.FileName, 
                OrderId = attachRequest.OrderId,
                Bytes = System.IO.File.ReadAllBytes(filePath)
            });

            System.IO.File.Delete(filePath);

            var response = new AttachFileSuccessResponse { Success = original != null };
            return Ok(new Response<AttachFileSuccessResponse>(response));
        }

        /// <summary>
        /// POST api/orders/originals/delete/5
        /// Delete order attachment
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpDelete(ApiRoutes.Orders.DeleteOrderAttachment)]
        public async Task<IActionResult> DeleteOrderAttachment(Guid orderId, [FromBody] DeleteAttachmentRequest attachRequest)
        {
            if(attachRequest.OriginalId == Guid.Empty) // move to request validator
            {
                return BadRequest();
            }

            var success = await _orderService.DeleteOriginalAsync(new OriginalDeleteParams
            {
                OriginalId = attachRequest.OriginalId,
                OrderId = orderId
            });

            var response = new DeleteAttachmentSuccessResponse { Success = success };
            return Ok(new Response<DeleteAttachmentSuccessResponse>(response));
        }
    }
}
