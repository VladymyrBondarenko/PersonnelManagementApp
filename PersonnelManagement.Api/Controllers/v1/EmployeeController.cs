using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.Application.Employees;
using PersonnelManagement.Contracts.v1.Requests;
using PersonnelManagement.Contracts.v1.Requests.Employees;
using PersonnelManagement.Contracts.v1.Requests.Queries;
using PersonnelManagement.Contracts.v1.Responses;
using PersonnelManagement.Contracts.v1.Responses.Employees;
using PersonnelManagement.Contracts.v1.Routes;
using PersonnelManagement.Domain.Employees;
using PersonnelManagement.Domain.Models;
using PersonnelManagement.Domain.Models.Filters;
using PersonnelManagement.Server.Services.PaginationServices.Employees;
using PersonnelManagement.Server.Services.UriServices;

namespace PersonnelManagement.Server.Controllers.v1
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        private readonly IEmployeePaginationService _paginationService;

        public EmployeeController(IEmployeeService employeeService,
            IMapper mapper, IUriService uriService, IEmployeePaginationService paginationService)
        {
            _employeeService = employeeService;
            _mapper = mapper;
            _uriService = uriService;
            _paginationService = paginationService;
        }

        // GET: api/employees
        [HttpGet(ApiRoutes.Employees.GetAll)]
        public async Task<IActionResult> Get([FromQuery] PaginationQueryRequest queryRequest, [FromQuery] GetAllEmployeesQuery query)
        {
            var paginationFilter = _mapper.Map<PaginationQuery>(queryRequest);
            var filter = _mapper.Map<GetAllEmployeesFilter>(query);
            var employees = await _employeeService.GetAllAsync(paginationFilter, filter);

            var totalAmount = await _employeeService.GetEmployeesAmountAsync();
            var response = _mapper.Map<List<GetEmployeeResponse>>(employees);

            var pagedResponse = _paginationService.CreatePaginatedResponse(paginationFilter, response, totalAmount);

            return Ok(pagedResponse);
        }

        // GET api/employees/5
        [HttpGet(ApiRoutes.Employees.Get)]
        public async Task<IActionResult> Get(Guid employeeId)
        {
            var employee = await _employeeService.GetAsync(employeeId);
            var response = _mapper.Map<GetEmployeeResponse>(employee);
            return Ok(new Response<GetEmployeeResponse>(response));
        }

        // POST api/employees
        [HttpPost(ApiRoutes.Employees.Create)]
        public async Task<IActionResult> Post([FromBody] CreateEmployeeRequest createRequest)
        {
            var createdEmployee = await _employeeService.CreateAsync(_mapper.Map<Employee>(createRequest));

            if (createdEmployee == null)
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = new List<ErrorModel>
                    {
                        new ErrorModel
                        {
                            Message = "The employee was not created."
                        }
                    }
                });
            }

            var response = _mapper.Map<GetEmployeeResponse>(createdEmployee);
            return Created(_uriService.GetEmployeeUri(createdEmployee.Id.ToString()), new Response<GetEmployeeResponse>(response));
        }

        // PUT api/employees/5
        [HttpPut(ApiRoutes.Employees.Update)]
        public async Task<IActionResult> Put(Guid employeeId, [FromBody] UpdateEmployeeRequest updateRequest)
        {
            var employee = await _employeeService.GetAsync(employeeId);

            if (employee != null)
            {
                employee.FirstName = updateRequest.FirstName;
                employee.LastName = updateRequest.LastName;
                employee.DepartmentId = updateRequest.DepartmentId;
                employee.PositionId = updateRequest.PositionId;
                employee.HireDate = updateRequest.HireDate;

                if (await _employeeService.UpdateAsync(employee))
                {
                    var response = _mapper.Map<GetEmployeeResponse>(employee);
                    return Ok(new Response<GetEmployeeResponse>(response));
                }
            }

            return NotFound();
        }

        // DELETE api/employees/5
        [HttpDelete(ApiRoutes.Employees.Delete)]
        public async Task<IActionResult> Delete(Guid employeeId)
        {
            if (await _employeeService.DeleteAsync(employeeId))
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
