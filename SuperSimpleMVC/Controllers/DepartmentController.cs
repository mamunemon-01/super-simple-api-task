using Microsoft.AspNetCore.Mvc;
using SuperSimpleMVC;
using SuperSimpleMVC.Models;
using SuperSimpleMVC.Models.ViewModels;

public class DepartmentController : Controller {
    private readonly ApiClient _apiClient;

    public DepartmentController(ApiClient apiClient)
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

    [HttpDelete]
    public async Task<IActionResult> Delete(string id)
    {
        await _apiClient.DeleteEmployeeAsync(id);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> SearchByName(string name)
    {
        var employees = await _apiClient.SearchByNameAsync(name);
        return Json(employees);
    }

    [HttpGet]
    public async Task<IActionResult> SearchByPhoneNo(string phoneNo)
    {
        var employees = await _apiClient.SearchByPhoneNoAsync(phoneNo);
        return Json(employees);
    }

    [HttpGet]
    public async Task<IActionResult> SearchByDeptName(string deptName)
    {
        var employees = await _apiClient.SearchByDeptNameAsync(deptName);
        return Json(employees);
    }
}