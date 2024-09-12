using Microsoft.AspNetCore.Mvc;
using SuperSimpleAPITask.Models;
using SuperSimpleAPITask.Repositories.Interfaces;

namespace SuperSimpleAPITask.Controllers{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController:ControllerBase {
        //private readonly IDepartmentRepository _unitOfWork.Department;
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentController(IUnitOfWork unitOfWork){
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get(){
            var Departments = await _unitOfWork.Department.GetAllAsync();
            return Ok(Departments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id){
            var Department = await _unitOfWork.Department.GetByIdAsync(id);
            if(Department==null){
                return NotFound();
            }
            return Ok(Department);
        }

        [HttpPost]
        public async Task<IActionResult> Post(string name){
            Department department = new Department {
                Id = Guid.NewGuid(),
                Name = name
            };
            await _unitOfWork.Department.CreateAsync(department);
            //if(result==0){
            //    return BadRequest();
            //}
            _unitOfWork.SaveAsync();
            return CreatedAtAction(nameof(Get), new { department.Id }, department);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, string name){
            Department  department = new Department() { Id = id, Name = name };
            if(id!=department.Id) return BadRequest();
            _unitOfWork.Department.Update(department);
            //if(result==0) return NotFound();
            _unitOfWork.SaveAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var department = await _unitOfWork.Department.GetByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            _unitOfWork.Department.Delete(department);
            //if(result==0) return NotFound();
            _unitOfWork.SaveAsync();
            return NoContent();
        }

        [HttpGet("search/name/{name}")]
        public async Task<IActionResult> SearchByName(string name){
            var result = await _unitOfWork.Department.SearchByName(name);
            return Ok(result);
        }
    }
}