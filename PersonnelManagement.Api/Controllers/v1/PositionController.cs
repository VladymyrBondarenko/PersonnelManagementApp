using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.Application.Positions;
using PersonnelManagement.Contracts.v1.Requests;
using PersonnelManagement.Contracts.v1.Requests.Positions;
using PersonnelManagement.Contracts.v1.Requests.Queries;
using PersonnelManagement.Contracts.v1.Responses;
using PersonnelManagement.Contracts.v1.Responses.Positions;
using PersonnelManagement.Contracts.v1.Routes;
using PersonnelManagement.Domain.Models;
using PersonnelManagement.Domain.Models.Filters;
using PersonnelManagement.Domain.Positions;
using PersonnelManagement.Server.Helpers;
using PersonnelManagement.Server.Services.PaginationServices.Positions;
using PersonnelManagement.Server.Services.UriServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PersonnelManagement.Api.Controllers.v1
{
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly IPositionService _positionService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        private readonly IPositionPaginationService _paginationService;

        public PositionController(IPositionService positionService,
            IMapper mapper, IUriService uriService, IPositionPaginationService paginationService)
        {
            _positionService = positionService;
            _mapper = mapper;
            _uriService = uriService;
            _paginationService = paginationService;
        }

        // GET: api/<PositionController>
        [HttpGet(ApiRoutes.Positions.GetAll)]
        public async Task<IActionResult> Get([FromQuery] PaginationQueryRequest queryRequest, [FromQuery] GetAllPositionsQuery query)
        {
            var filter = _mapper.Map<GetAllPositionsFilter>(query);
            var paginationFilter = _mapper.Map<PaginationQuery>(queryRequest);

            var positions = await _positionService.GetAllAsync(paginationFilter, filter);

            var totalAmount = await _positionService.GetPositionsAmountAsync();
            var response = _mapper.Map<List<GetPositionResponse>>(positions);

            var pagedResponse = _paginationService.CreatePaginatedResponse(paginationFilter, response, totalAmount);

            return Ok(pagedResponse);
        }

        // GET api/<PositionController>/5
        [HttpGet(ApiRoutes.Positions.Get)]
        public async Task<IActionResult> Get(Guid positionId)
        {
            var position = await _positionService.GetAsync(positionId);

            var response = _mapper.Map<GetPositionResponse>(position);
            return Ok(new Response<GetPositionResponse>(response));
        }

        // POST api/<PositionController>
        [HttpPost(ApiRoutes.Positions.Create)]
        public async Task<IActionResult> Post([FromBody] CreatePositionRequest createRequest)
        {
            var position = _mapper.Map<Position>(createRequest);
            var createdPosition = await _positionService.CreateAsync(position);

            if(createdPosition == null)
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = new List<ErrorModel>
                    {
                        new ErrorModel
                        {
                            Message = "The position was not created."
                        }
                    }
                });
            }

            var response = _mapper.Map<GetPositionResponse>(createdPosition);
            return Created(_uriService.GetPositionUri(createdPosition.Id.ToString()), 
                new Response<GetPositionResponse>(response));
        }

        // PUT api/<PositionController>/5
        [HttpPut(ApiRoutes.Positions.Update)]
        public async Task<IActionResult> Put(Guid positionId, [FromBody] UpdatePositionRequest updateRequest)
        {
            var position = await _positionService.GetAsync(positionId);

            if (position != null)
            {
                position.PositionTitle = updateRequest.PositionTitle;
                position.PositionDescription = updateRequest.PositionDescription;

                if (await _positionService.UpdateAsync(position))
                {
                    var response = _mapper.Map<GetPositionResponse>(position);
                    return Ok(new Response<GetPositionResponse>(response));
                }
            }

            return NotFound();
        }

        // DELETE api/<PositionController>/5
        [HttpDelete(ApiRoutes.Positions.Delete)]
        public async Task<IActionResult> Delete(Guid positionId)
        {
            if (await _positionService.DeleteAsync(positionId))
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
