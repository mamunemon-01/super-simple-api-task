using Microsoft.AspNetCore.Mvc;
using SuperSimpleMVC;
using SuperSimpleMVC.Models;
using SuperSimpleMVC.Models.ViewModels;

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
    public async Task<IActionResult> Post([FromBody] CreateEmployeeDto employee) {
        var newEmployee = await _apiClient.AddEmployeeAsync(employee);
        return Json(newEmployee);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, [FromBody] CreateEmployeeDto employee)
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

    [HttpGet]
    public async Task<IActionResult> SearchByNameAsync(string name)
    {
        var employees = await _apiClient.GetAllEmployeesAsync();
        return Json(employees);
    }

    [HttpGet]
    public async Task<IActionResult> SearchByPhoneNoAsync(string phoneNo)
    {
        var employees = await _apiClient.GetAllEmployeesAsync();
        return Json(employees);
    }

    [HttpGet]
    public async Task<IActionResult> SearchByDeptNameAsync(string deptName)
    {
        var employees = await _apiClient.GetAllEmployeesAsync();
        return Json(employees);
    }
}