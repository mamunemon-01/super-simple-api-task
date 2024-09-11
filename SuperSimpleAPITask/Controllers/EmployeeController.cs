using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SuperSimpleAPITask.Models;
using SuperSimpleAPITask.Models.ViewModels;

namespace SuperSimpleAPITask.Controllers{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController:ControllerBase {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public EmployeeController(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository){
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
        }

        [HttpGet("Retrieve")]
        public async Task<IActionResult> Retrieve(){
            var employees = await _employeeRepository.GetAllAsync();
            
            List<CreateEmployeeDto> employeeDtos = new List<CreateEmployeeDto>();

            foreach(Employee employee in employees){
                Department department = await _departmentRepository.GetByIdAsync(employee.DeptId);
                employeeDtos.Add(new CreateEmployeeDto{
                    Id = employee.Id,
                    Name = employee.Name,
                    PhoneNo = employee.PhoneNo,
                    DeptName = department.Name
                });
            }

            return Ok(employeeDtos);
        }

        [HttpGet("Retrieve/{id}")]
        public async Task<IActionResult> Retrieve(Guid id){
            var employee = await _employeeRepository.GetByIdAsync(id);
            if(employee==null){
                return NotFound();
            }
            Department department = await _departmentRepository.GetByIdAsync(employee.DeptId);
            CreateEmployeeDto employeeDto = new CreateEmployeeDto{
                Id = employee.Id,
                Name = employee.Name,
                PhoneNo = employee.PhoneNo,
                DeptName = department.Name
            };
            return Ok(employeeDto);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromForm] CreateEmployeeDto employeeDto){
            var departments = await _departmentRepository.SearchByName(employeeDto.DeptName);
            Employee employee = new Employee{
                Id = (Guid)((employeeDto.Id.HasValue && employeeDto.Id != Guid.Empty)?employeeDto.Id:Guid.NewGuid()),
                Name = employeeDto.Name,
                PhoneNo = employeeDto.PhoneNo,
                DeptId = departments.ElementAt(0).Id
            };
            var result = await _employeeRepository.CreateAsync(employee);
            if(result==0){
                return BadRequest();
            }
            return CreatedAtAction(nameof(Retrieve), new { employee.Id }, employee);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromForm] CreateEmployeeDto employeeDto){
            Employee existingEmployee = await _employeeRepository.GetByIdAsync(id);

            if(existingEmployee == null) return NotFound();

            if(employeeDto.Id.HasValue && employeeDto.Id != Guid.Empty){
                existingEmployee.Id = (Guid) employeeDto.Id;
            }
            if(!employeeDto.Name.IsNullOrEmpty()){
                existingEmployee.Name = employeeDto.Name;
            }
            if(!employeeDto.PhoneNo.IsNullOrEmpty()){
                existingEmployee.PhoneNo = employeeDto.PhoneNo;
            }
            if(!employeeDto.DeptName.IsNullOrEmpty()){
                var departments = await _departmentRepository.SearchByName(employeeDto.DeptName);
                if(departments.Count() > 0){
                    existingEmployee.DeptId = departments.ElementAt(0).Id;
                }
                else{
                    return BadRequest("Department not found.");
                }
            };

            var result = await _employeeRepository.UpdateAsync(existingEmployee);
            if(result==0) return NotFound();
            return NoContent();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id){
            var result = await _employeeRepository.DeleteAsync(id);
            if(result==0) return NotFound();
            return NoContent();
        }

        [HttpGet("Search/name/{name}")]
        public async Task<IActionResult> SearchByName(string name){
            var results = await _employeeRepository.SearchByName(name);

            List<CreateEmployeeDto> employeeDtos = new List<CreateEmployeeDto>();

            foreach(Employee result in results){
                Department department = await _departmentRepository.GetByIdAsync(result.DeptId);
                employeeDtos.Add(new CreateEmployeeDto{
                    Name = result.Name,
                    PhoneNo = result.PhoneNo,
                    DeptName = department.Name
                });
            }
            return Ok(employeeDtos);
        }

        [HttpGet("Search/phoneNo/{phoneNo}")]
        public async Task<IActionResult> SearchByPhoneNo(string phoneNo){
            var results = await _employeeRepository.SearchByPhoneNo(phoneNo);

            List<CreateEmployeeDto> employeeDtos = new List<CreateEmployeeDto>();

            foreach(Employee result in results){
                Department department = await _departmentRepository.GetByIdAsync(result.DeptId);
                employeeDtos.Add(new CreateEmployeeDto{
                    Name = result.Name,
                    PhoneNo = result.PhoneNo,
                    DeptName = department.Name
                });
            }

            return Ok(employeeDtos);
        }

        [HttpGet("Search/deptId/{deptId}")]
        public async Task<IActionResult> SearchByDeptId(Guid deptId){
            var results = await _employeeRepository.SearchByDept(deptId);

            List<CreateEmployeeDto> employeeDtos = new List<CreateEmployeeDto>();

            foreach(Employee result in results){
                Department department = await _departmentRepository.GetByIdAsync(result.DeptId);
                employeeDtos.Add(new CreateEmployeeDto{
                    Name = result.Name,
                    PhoneNo = result.PhoneNo,
                    DeptName = department.Name
                });
            }

            return Ok(employeeDtos);
        }
    }
}