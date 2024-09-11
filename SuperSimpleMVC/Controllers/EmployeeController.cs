using Microsoft.AspNetCore.Mvc;

public class EmployeeController : Controller {
    public IActionResult Index(){
        return View();
    }

    public IActionResult Create(){
        return View();
    }
}