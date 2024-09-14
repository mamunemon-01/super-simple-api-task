using Microsoft.AspNetCore.Mvc;
using SuperSimpleMVC;
using SuperSimpleMVC.Models;

public class EmployeeController : Controller {
    private readonly ApiClient _apiClient;

    public EmployeeController(ApiClient apiClient)
    {
        _apiClient = apiClient;
    }
    public IActionResult Index() {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var employees = await _apiClient.GetAllEmployeesAsync();
        return Json(employees);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Employee employee) {
        var newEmployee = await _apiClient.AddEmployeeAsync(employee);
        return Json(newEmployee);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, [FromBody] Employee employee)
    {
        var updatedEmployee = await _apiClient.UpdateEmployeeAsync(id, employee);
        return Json(updatedEmployee);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _apiClient.DeleteEmployeeAsync(id);
        return Ok();
    }
}