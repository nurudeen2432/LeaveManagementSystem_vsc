using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeaveManagementSystem.Web.Services.LeaveAllocations;

namespace LeaveManagementSystem.Web.Controllers
{
    public class LeaveAllocationController(ILeaveAllocationService _leaveAllocationService) : Controller
    {
        [Authorize(Roles.Administrator)]
        public async Task<IActionResult> Index()
        {

            var employees = await _leaveAllocationService.GetEmployees();
            return View(employees);
        }
        public async Task<IActionResult> Details(Guid? userid)
        {

            var employeeVm = await _leaveAllocationService.GetEmployeeAllocations(userid);
            return View(employeeVm);
        }
    }

}
