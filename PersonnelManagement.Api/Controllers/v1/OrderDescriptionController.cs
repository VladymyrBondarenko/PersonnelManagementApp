using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.Application.Orders.Interfaces;
using PersonnelManagement.Contracts.v1.Responses;
using PersonnelManagement.Contracts.v1.Responses.OrdersDescription;
using PersonnelManagement.Contracts.v1.Routes;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PersonnelManagement.Api.Controllers.v1
{
    [ApiController]
    public class OrderDescriptionController : ControllerBase
    {
        private readonly IOrderDescriptionService _orderDescriptionService;
        private readonly IMapper _mapper;

        public OrderDescriptionController(IOrderDescriptionService orderDescriptionService,
            IMapper mapper)
        {
            _orderDescriptionService = orderDescriptionService;
            _mapper = mapper;
        }

        // GET: api/<OrderDescriptionController>
        [HttpGet(ApiRoutes.OrderDescriptions.GetAll)]
        public async Task<IActionResult> Get()
        {
            var orderDescriptions = await _orderDescriptionService.GetAllAsync();

            var response = _mapper.Map<List<GetOrderDescriptionResponse>>(orderDescriptions);
            return Ok(new Response<List<GetOrderDescriptionResponse>>(response));
        }

        //// GET api/<OrderDescriptionController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<OrderDescriptionController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<OrderDescriptionController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<OrderDescriptionController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
