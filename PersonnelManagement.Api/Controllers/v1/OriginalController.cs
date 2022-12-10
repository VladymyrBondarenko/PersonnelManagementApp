using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.Application.FileOperations.Originals;
using PersonnelManagement.Application.Identities;
using PersonnelManagement.Contracts.v1.Requests;
using PersonnelManagement.Contracts.v1.Requests.Originals;
using PersonnelManagement.Contracts.v1.Requests.Queries;
using PersonnelManagement.Contracts.v1.Responses;
using PersonnelManagement.Contracts.v1.Responses.Originals;
using PersonnelManagement.Contracts.v1.Routes;
using PersonnelManagement.Domain.Models;
using PersonnelManagement.Domain.Models.Filters;
using PersonnelManagement.Domain.Models.Originals;
using PersonnelManagement.Server.Services.PaginationServices.Originals;
using PersonnelManagement.Server.Services.UriServices;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PersonnelManagement.Server.Controllers.v1
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OriginalController : ControllerBase
    {
        private readonly IOriginalService _originalService;
        private readonly IMapper _mapper;
        private readonly IOriginalPaginationService _paginationService;
        private readonly IUriService _uriService;
        private readonly IIdentityService _identityService;

        public OriginalController(IOriginalService originalService, 
            IMapper mapper, IOriginalPaginationService paginationService, 
            IUriService uriService, IIdentityService identityService)
        {
            _originalService = originalService;
            _mapper = mapper;
            _paginationService = paginationService;
            _uriService = uriService;
            _identityService = identityService;
        }

        // GET: api/originals
        [HttpGet(ApiRoutes.Originals.GetAll)]
        public async Task<IActionResult> Get([FromQuery] PaginationQueryRequest queryRequest, [FromQuery] GetAllOriginalsQuery query)
        {
            var paginationFilter = _mapper.Map<PaginationQuery>(queryRequest);
            var filter = _mapper.Map<GetAllOriginalsFilter>(query);
            var originals = await _originalService.GetOriginalsAsync(paginationFilter, filter);

            var totalAmount = await _originalService.GetOriginalsAmountAsync(filter);
            var response = _mapper.Map<List<GetOriginalResponse>>(originals);

            var pagedResponse = _paginationService.CreatePaginatedResponse(paginationFilter, response, totalAmount);

            return Ok(pagedResponse);
        }

        // GET api/originals/5
        [HttpGet(ApiRoutes.Originals.Get)]
        public async Task<IActionResult> Get(Guid originalId)
        {
            var original = await _originalService.GetOriginalAsync(originalId);
            var response = _mapper.Map<GetOriginalResponse>(original);
            return Ok(new Response<GetOriginalResponse>(response));
        }

        // GET api/originals/download/5
        [AllowAnonymous()]
        [HttpGet(ApiRoutes.Originals.DownloadFile)]
        public async Task<IActionResult> DownloadFile(Guid originalId, string token)
        {
            if (!_identityService.ValidateToken(token))
            {
                return Unauthorized();
            }

            var original = await _originalService.GetOriginalAsync(originalId);

            if(original == null)
            {
                return NotFound();
            }

            var bytes = await _originalService.GetOriginalBytesAsync(originalId);
            return File(bytes, "application/octet-stream", original.FileName);
        }

        // POST api/originals
        [HttpPost(ApiRoutes.Originals.Create)]
        public async Task<IActionResult> Post(int originalEntity, int originalType, Guid entityId, [FromForm] IFormFile file)
        {
            var filePath = getFilePath(file);

            var original = await _originalService.AddOriginalAsync(new OriginalCreateParams
            {
                FileName = file.FileName,
                EntityId = entityId,
                OriginalEntity = (OriginalEntity)originalEntity,
                Bytes = System.IO.File.ReadAllBytes(filePath),
                OriginalType = (OriginalType)originalType
            });

            if(original == null)
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = new List<ErrorModel>
                    {
                        new ErrorModel
                        {
                            Message = "The attachment was not added."
                        }
                    }
                });
            }

            System.IO.File.Delete(filePath);

            var response = _mapper.Map<GetOriginalResponse>(original);
            return Created(_uriService.GetOriginalUri(original.Id.ToString()),
                new Response<GetOriginalResponse>(response));
        }

        // PUT api/originals/5
        [HttpPut(ApiRoutes.Originals.Update)]
        public async Task<IActionResult> Put(Guid originalId, [FromBody] UpdateOriginalRequest updateRequest)
        {
            var original = await _originalService.GetOriginalAsync(originalId);

            if(original != null)
            {
                original.OriginalTitle = updateRequest.OriginalTitle;

                if (await _originalService.UpdateOriginal(original))
                {
                    return Ok(new Response<GetOriginalResponse>(_mapper.Map<GetOriginalResponse>(original)));
                }
            }

            return NotFound();
        }

        // DELETE api/originals/5
        [HttpDelete(ApiRoutes.Originals.Delete)]
        public async Task<IActionResult> Delete(Guid originalId)
        {
            if(await _originalService.DeleteOriginalAsync(originalId))
            {
                return NoContent();
            }

            return BadRequest();
        }

        private string getFilePath(IFormFile file)
        {
            string executableLocation = Path.GetDirectoryName(
               Assembly.GetExecutingAssembly().Location);
            var filePath = Path.Combine(executableLocation, file.FileName);

            using var stream = System.IO.File.Create(filePath);
            file.CopyTo(stream);
            stream.Close();

            return filePath;
        }
    }
}