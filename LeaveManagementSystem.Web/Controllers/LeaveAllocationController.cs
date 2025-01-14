using LeaveManagementSystem.Common.Static;
using LeaveManagementSystem.Application.Models.LeaveAllocations;
using LeaveManagementSystem.Application.Services.LeaveAllocations;
using LeaveManagementSystem.Application.Services.LeaveTypes;


namespace LeaveManagementSystem.Web.Controllers
{
    public class LeaveAllocationController(
        ILeaveAllocationService _leaveAllocationService,

        ILeaveTypeService _leaveTypeService

    ) : Controller
    {
        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> Index()
        {

            var employees = await _leaveAllocationService.GetEmployees();
            return View(employees);
        }

        [Authorize(Roles = Roles.Administrator)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AllocateLeave(string? id)
        {

            await _leaveAllocationService.AllocateLeave(id);
            return RedirectToAction(nameof(Details), new { userid = id });
        }

        [Authorize(Roles = Roles.Administrator)]

        public async Task<IActionResult> EditAllocation(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var allocation = await _leaveAllocationService.GetEmployeeAllocation(id.Value);

            if (allocation == null)
            {
                return NotFound();
            }

            return View(allocation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> EditAllocation(LeaveAllocationEditVM allocationEditVM)
        {
            if (await _leaveTypeService.DaysExceedMaximum(allocationEditVM.LeaveType.Id, allocationEditVM.Days))
            {
                ModelState.AddModelError("Days", "The Allocation exceeds the maximum leave type value");
            }

            if (ModelState.IsValid)
            {
                await _leaveAllocationService.EditAllocation(allocationEditVM);

                return RedirectToAction(nameof(Details), new { userId = allocationEditVM.Employee.Id });
            }

            var days = allocationEditVM.Days;


            var allocation = await _leaveAllocationService.GetEmployeeAllocation(allocationEditVM.Id );

            allocation.Days = days;
            
            return View(allocationEditVM);

        }

        public async Task<IActionResult> Details(string? userid)
        {

            var employeeVm = await _leaveAllocationService.GetEmployeeAllocations(userid);
           
            return View(employeeVm);
        }


    }

}
