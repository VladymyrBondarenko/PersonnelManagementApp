using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.Application.Positions;
using PersonnelManagement.Contracts.v1.Responses;
using PersonnelManagement.Contracts.v1.Responses.Positions;
using PersonnelManagement.Contracts.v1.Routes;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PersonnelManagement.Api.Controllers.v1
{
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly IPositionService _positionService;
        private readonly IMapper _mapper;

        public PositionController(IPositionService positionService,
            IMapper mapper)
        {
            _positionService = positionService;
            _mapper = mapper;
        }

        // GET: api/<PositionController>
        [HttpGet(ApiRoutes.Positions.GetAll)]
        public async Task<IActionResult> Get()
        {
            var positions = await _positionService.GetAllAsync();

            var response = _mapper.Map<List<GetPositionResponse>>(positions);
            return Ok(new Response<List<GetPositionResponse>>(response));
        }

        // GET api/<PositionController>/5
        [HttpGet(ApiRoutes.Positions.Get)]
        public async Task<IActionResult> Get(Guid positionId)
        {
            var position = await _positionService.GetAsync(positionId);

            var response = _mapper.Map<GetPositionResponse>(position);
            return Ok(new Response<GetPositionResponse>(response));
        }

        // TODO: post, put, delete

        // POST api/<PositionController>
        [HttpPost(ApiRoutes.Positions.Create)]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PositionController>/5
        [HttpPut(ApiRoutes.Positions.Update)]
        public void Put(Guid id, [FromBody] string value)
        {
        }

        // DELETE api/<PositionController>/5
        [HttpDelete(ApiRoutes.Positions.Delete)]
        public void Delete(Guid id)
        {
        }
    }
}
