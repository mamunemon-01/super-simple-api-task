using Microsoft.AspNetCore.Mvc;
using SuperSimpleAPITask.Models;

namespace SuperSimpleAPITask.Controllers{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController:ControllerBase {
        private readonly IDepartmentRepository _DepartmentRepository;

        public DepartmentController(IDepartmentRepository DepartmentRepository){
            _DepartmentRepository = DepartmentRepository;
        }

        [HttpGet("Retrieve")]
        public async Task<IActionResult> Retrieve(){
            var Departments = await _DepartmentRepository.GetAllAsync();
            return Ok(Departments);
        }

        [HttpGet("Retrieve/{id}")]
        public async Task<IActionResult> Retrieve(Guid id){
            var Department = await _DepartmentRepository.GetByIdAsync(id);
            if(Department==null){
                return NotFound();
            }
            return Ok(Department);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(string name){
            Department department = new Department {
                Id = Guid.NewGuid(),
                Name = name
            };
            var result = await _DepartmentRepository.CreateAsync(department);
            if(result==0){
                return BadRequest();
            }
            return CreatedAtAction(nameof(Retrieve), new { department.Id }, department);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(Guid id, string name){
            Department  department = new Department() { Id = id, Name = name };
            if(id!=department.Id) return BadRequest();
            var result = await _DepartmentRepository.UpdateAsync(department);
            if(result==0) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id){
            var result = await _DepartmentRepository.DeleteAsync(id);
            if(result==0) return NotFound();
            return NoContent();
        }

        [HttpGet("Search/name/{name}")]
        public async Task<IActionResult> SearchByName(string name){
            var result = await _DepartmentRepository.SearchByName(name);
            return Ok(result);
        }
    }
}