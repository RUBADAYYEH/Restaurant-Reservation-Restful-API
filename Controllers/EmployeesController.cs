using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservationDb.domain;
using RetaurantReservationAPI.Models;
using RetaurantReservationAPI.Services;

namespace RetaurantReservationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _repo;
        private readonly IMapper _mapper;

        public EmployeesController(IEmployeeRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployees()
        {
            var employees = await _repo.GetEmployeesAsync();

            return Ok(_mapper.Map<IEnumerable<EmployeeDto>>(employees));
        }
        [HttpGet("{id}", Name = "GetEmployee")]
        public async Task<ActionResult<EmployeeDto>> GetEmployee(int id)
        {
            var employee = await _repo.GetEmployeeByIdAsync(id);
            if (employee is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<EmployeeDto>(employee));
        }
        [HttpGet("managers")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetManagers()
        {
            var managers = await _repo.GetManagersAsync();
            return Ok(_mapper.Map<IEnumerable<EmployeeDto>>(managers));
        }
        [HttpPost("{restaurantId}/{employeeId}")]
        public async Task<ActionResult<EmployeeDto>> AddEmployee(int restaurantId, [FromBody] EmployeeDto employee)
        {
            if (!await _repo.RestaurantExists(restaurantId))
            {
                return NotFound();
            }
            var finalemp = _mapper.Map<Employee>(employee);
            await _repo.AddEmployeeForRestaurant(restaurantId, finalemp);
            await _repo.SaveChangesAsync();
            var employeeDto = _mapper.Map<EmployeeDto>(finalemp);
            return CreatedAtAction("GetEmployee", new { id = finalemp.EmployeeId }, employeeDto);
        }
        [HttpPut("{restaurantId}/{employeeId}")]
        public async Task<ActionResult> UpdateEmployee(int restaurantId, int employeeid, EmployeeDto empDto)
        {
            if (!await _repo.RestaurantExists(restaurantId))
            {
                return NotFound();
            }
            var emp = await _repo.GetEmployeeByIdAsync(employeeid);
            if (emp == null)
            {
                return NotFound();
            }
            _mapper.Map(empDto, emp);
            await _repo.SaveChangesAsync();
            return NoContent();

        }
        [HttpPatch("{restaurantId}/{employeeId}")]
        public async Task<ActionResult> PartiallyUpdateEmployee(int restaurantId, int employeeid, JsonPatchDocument<EmployeeDto> patchDocument)
        {
            if (!await _repo.RestaurantExists(restaurantId))
            {
                return NotFound();
            }
            var emp = await _repo.GetEmployeeByIdAsync(employeeid);
            if (emp == null)
            {
                return NotFound();
            }
            var empToPatch = _mapper.Map<EmployeeDto>(emp);
            patchDocument.ApplyTo(empToPatch, (Microsoft.AspNetCore.JsonPatch.Adapters.IObjectAdapter)ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!TryValidateModel(empToPatch))
            {
                return BadRequest(ModelState);
            }
            _mapper.Map(empToPatch, emp);

            await _repo.SaveChangesAsync();
            return NoContent();

        }
        [HttpDelete("{restaurantId}/{employeeId}")]
        public async Task<ActionResult> DeleteEmployee(int restaurantId,int employeeid)
        {
            if (!await _repo.RestaurantExists(restaurantId))
            {
                return NotFound();
            }
            var emp = await _repo.GetEmployeeByIdAsync(employeeid);
            if (emp == null)
            {
                return NotFound();
            }
            _repo.DeleteEmployee(emp);
            await _repo.SaveChangesAsync();
            return NoContent();

        }
        [HttpGet("{imployeeid}/average-order-amount")]
        public async Task<ActionResult> GetAverageOrderAmountForEmployee(int imployeeid)
        {
            var emp = await _repo.GetEmployeeByIdAsync(imployeeid);
            if (emp is null)
            {
                return NotFound();
            }
            var result = await _repo.GetAverageOrderAmountForEmployeeWithEmployeeId(emp.EmployeeId);
            return Ok(result);

        }

    }
}
