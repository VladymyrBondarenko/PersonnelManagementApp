using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.Application.Departments;
using PersonnelManagement.Contracts.v1.Requests;
using PersonnelManagement.Contracts.v1.Responses;
using PersonnelManagement.Contracts.v1.Responses.Departments;
using PersonnelManagement.Contracts.v1.Routes;
using PersonnelManagement.Domain.Departments;
using PersonnelManagement.Server.Services;
using PersonnelManagement.Server.Helpers;
using PersonnelManagement.Domain.Models;
using PersonnelManagement.Contracts.v1.Requests.Queries;
using PersonnelManagement.Domain.Models.Filters;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PersonnelManagement.Api.Controllers.v1
{
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public DepartmentController(IDepartmentService departmentService,
            IMapper mapper, IUriService uriService)
        {
            _departmentService = departmentService;
            _mapper = mapper;
            _uriService = uriService;
        }

        // GET: api/<DepartmentController>
        [HttpGet(ApiRoutes.Departments.GetAll)]
        public async Task<IActionResult> Get([FromQuery] PaginationQueryRequest queryRequest, [FromQuery] GetAllDepartmentsQuery query)
        {
            var paginationFilter = _mapper.Map<PaginationQuery>(queryRequest);
            var filter = _mapper.Map<GetAllDepartmentsFilter>(query);

            var departments = await _departmentService.GetAllAsync(paginationFilter, filter);

            var totalAmount = await _departmentService.GetDepartmentsAmountAsync();
            var response = _mapper.Map<List<GetDepartmentResponse>>(departments);

            var pagedResponse = PaginationHelpers
               .CreatePaginatedResponse(_uriService, paginationFilter, response, totalAmount);

            return Ok(pagedResponse);
        }

        // GET api/<DepartmentController>/5
        [HttpGet(ApiRoutes.Departments.Get)]
        public async Task<IActionResult> Get(Guid departmentId)
        {
            var department = await _departmentService.GetAsync(departmentId);
            
            var response = _mapper.Map<GetDepartmentResponse>(department);
            return Ok(new Response<GetDepartmentResponse>(response));
        }

        // POST api/<DepartmentController>
        [HttpPost(ApiRoutes.Departments.Create)]
        public async Task<IActionResult> Post([FromBody] CreateDepartmentRequest createRequest)
        {
            var department = _mapper.Map<Department>(createRequest);
            var createdDepartment = await _departmentService.CreateAsync(department);

            if(createdDepartment == null)
            {
                // TODO : return some error text
                return BadRequest();
            }

            var response = _mapper.Map<GetDepartmentResponse>(createdDepartment);
            return Created(_uriService.GetDepartmentsUri(createdDepartment.Id.ToString()), 
                new Response<GetDepartmentResponse>(response));
        }

        // PUT api/<DepartmentController>/5
        [HttpPut(ApiRoutes.Departments.Update)]
        public async Task<IActionResult> Put(Guid departmentId, [FromBody] UpdateDepartmentRequest updateRequest)
        {
            var department = await _departmentService.GetAsync(departmentId);

            if(department == null)
            {
                // TODO : return some error text
                return BadRequest();
            }

            department.DepartmentTitle = updateRequest.DepartmentTitle;
            department.DepartmentDescription = updateRequest.DepartmentDescription;
            department.DateFrom = updateRequest.DateFrom;
            department.DateTo = updateRequest.DateTo;

            if (await _departmentService.UpdateAsync(department))
            {
                var response = _mapper.Map<GetDepartmentResponse>(department);
                return Ok(new Response<GetDepartmentResponse>(response));
            }

            return NotFound();
        }

        // DELETE api/<DepartmentController>/5
        [HttpDelete(ApiRoutes.Departments.Delete)]
        public async Task<IActionResult> Delete(Guid departmentId)
        {
            if (await _departmentService.DeleteAsync(departmentId))
            {
                return NoContent();
            }
            
            return NotFound();
        }
    }
}
