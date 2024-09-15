using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SuperSimpleAPITask.Models;
using SuperSimpleAPITask.Models.ViewModels;
using SuperSimpleAPITask.Repositories.Interfaces;

namespace SuperSimpleAPITask.Controllers{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController:ControllerBase {
        //private readonly IEmployeeRepository _unitOfWork.Employee;
        //private readonly IDepartmentRepository _unitOfWork.Department;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeController(IUnitOfWork unitOfWork){
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get(){
            var employees = await _unitOfWork.Employee.GetAllAsync();
            
            List<ReadEmployeeDto> employeeDtos = new List<ReadEmployeeDto>();

            foreach(Employee employee in employees){
                Department department = await _unitOfWork.Department.GetByIdAsync(employee.DeptId);
                employeeDtos.Add(new ReadEmployeeDto{
                    Id = employee.Id,
                    Name = employee.Name,
                    PhoneNo = employee.PhoneNo,
                    DeptName = department.Name
                });
            }

            return Ok(employeeDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id){
            var employee = await _unitOfWork.Employee.GetByIdAsync(id);
            if(employee==null){
                return NotFound();
            }
            Department department = await _unitOfWork.Department.GetByIdAsync(employee.DeptId);
            ReadEmployeeDto employeeDto = new ReadEmployeeDto{
                Id = employee.Id,
                Name = employee.Name,
                PhoneNo = employee.PhoneNo,
                DeptName = department.Name
            };
            return Ok(employeeDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateEmployeeDto employeeDto){
            var departments = await _unitOfWork.Department.SearchByName(employeeDto.DeptName);
            Employee employee = new Employee{
                Id = Guid.NewGuid(),
                Name = employeeDto.Name,
                PhoneNo = employeeDto.PhoneNo,
                DeptId = departments.ElementAt(0).Id
            };
            await _unitOfWork.Employee.CreateAsync(employee);
            //if(result==0){
            //    return BadRequest();
            //}
            _unitOfWork.SaveAsync();
            return CreatedAtAction(nameof(Get), new { employee.Id }, employee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, CreateEmployeeDto employeeDto){
            Employee existingEmployee = await _unitOfWork.Employee.GetByIdAsync(id);

            if(existingEmployee == null) return NotFound();

            //if(employeeDto.Id.HasValue && employeeDto.Id != Guid.Empty){
            //    existingEmployee.Id = (Guid) employeeDto.Id;
            //}
            if(!employeeDto.Name.IsNullOrEmpty()){
                existingEmployee.Name = employeeDto.Name;
            }
            if(!employeeDto.PhoneNo.IsNullOrEmpty()){
                existingEmployee.PhoneNo = employeeDto.PhoneNo;
            }
            if(!employeeDto.DeptName.IsNullOrEmpty()){
                var departments = await _unitOfWork.Department.SearchByName(employeeDto.DeptName);
                if(departments.Count() > 0){
                    existingEmployee.DeptId = departments.ElementAt(0).Id;
                }
                else{
                    return BadRequest("Department not found.");
                }
            };

            _unitOfWork.Employee.Update(existingEmployee);
            //if(result==0) return NotFound();
            _unitOfWork.SaveAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var employee = await _unitOfWork.Employee.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _unitOfWork.Employee.Delete(employee);
            //if(result==0) return NotFound();
            _unitOfWork.SaveAsync();
            return NoContent();
        }

        [HttpGet("search/name/{name}")]
        public async Task<IActionResult> SearchByName(string name){
            var results = await _unitOfWork.Employee.SearchByNameAsync(name);

            List<ReadEmployeeDto> employeeDtos = new List<ReadEmployeeDto>();

            foreach(Employee result in results){
                Department department = await _unitOfWork.Department.GetByIdAsync(result.DeptId);
                employeeDtos.Add(new ReadEmployeeDto{
                    Id = result.Id,
                    Name = result.Name,
                    PhoneNo = result.PhoneNo,
                    DeptName = department.Name
                });
            }
            return Ok(employeeDtos);
        }

        [HttpGet("search/phoneNo/{phoneNo}")]
        public async Task<IActionResult> SearchByPhoneNo(string phoneNo){
            var results = await _unitOfWork.Employee.SearchByPhoneNoAsync(phoneNo);

            List<ReadEmployeeDto> employeeDtos = new List<ReadEmployeeDto>();

            foreach(Employee result in results){
                Department department = await _unitOfWork.Department.GetByIdAsync(result.DeptId);
                employeeDtos.Add(new ReadEmployeeDto{
                    Id = result.Id,
                    Name = result.Name,
                    PhoneNo = result.PhoneNo,
                    DeptName = department.Name
                });
            }

            return Ok(employeeDtos);
        }

        [HttpGet("search/department/{deptName}")]
        public async Task<IActionResult> SearchByDeptId(string deptName){
            var departments = await _unitOfWork.Department.SearchByName(deptName);
            var results = await _unitOfWork.Employee.SearchByDeptAsync(departments.Select(d => d.Id).ToList());

            List<ReadEmployeeDto> employeeDtos = new List<ReadEmployeeDto>();

            foreach(Employee result in results){
                Department department = await _unitOfWork.Department.GetByIdAsync(result.DeptId);
                employeeDtos.Add(new ReadEmployeeDto{
                    Id = result.Id,
                    Name = result.Name,
                    PhoneNo = result.PhoneNo,
                    DeptName = department.Name
                });
            }

            return Ok(employeeDtos);
        }
    }
}