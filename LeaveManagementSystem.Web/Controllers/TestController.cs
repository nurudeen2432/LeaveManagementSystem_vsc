using LeaveManagementSystem.Web.Controllers;
using LeaveManagementSystem.Web.Models;
using Microsoft.AspNetCore.Mvc;


namespace LeaveManagementSystem.Web.Controllers
{

    public class TestController : Controller
    {
        public IActionResult index()
        {
            var data = new TestViewModel{
                Name = "Student of MVC Mastery",
                Id  = 1,
                DateOfBirth = new DateTime(1954,12,01)
            };
            return View(data);

        }

    }

}