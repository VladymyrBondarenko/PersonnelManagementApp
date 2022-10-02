using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.Application.Departments;
using PersonnelManagement.Contracts.v1.Requests;
using PersonnelManagement.Contracts.v1.Responses;
using PersonnelManagement.Contracts.v1.Responses.Departments;
using PersonnelManagement.Contracts.v1.Routes;
using PersonnelManagement.Domain.Departments;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PersonnelManagement.Api.Controllers.v1
{
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentService departmentService,
            IMapper mapper)
        {
            _departmentService = departmentService;
            _mapper = mapper;
        }

        // GET: api/<DepartmentController>
        [HttpGet(ApiRoutes.Departments.GetAll)]
        public async Task<IActionResult> Get()
        {
            var departments = await _departmentService.GetAllAsync();

            var respoonse = _mapper.Map<List<GetDepartmentResponse>>(departments);
            return Ok(new Response<List<GetDepartmentResponse>>(respoonse));
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
            return Ok(new Response<GetDepartmentResponse>(response));
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
            if(await _departmentService.UpdateAsync(department))
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
