using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LeaveManagementSystem.Application.Models.LeaveRequests;
using LeaveManagementSystem.Application.Models.LeaveTypes;
using LeaveManagementSystem.Application.Services.LeaveRequests;
using LeaveManagementSystem.Application.Services.LeaveTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace LeaveManagementSystem.Application.Controllers
{
    [Authorize]
    public class LeaveRequestsController(
        ILeaveTypeService _leaveTypeService,
        ILeaveRequestsService _leaveRequestsService
        
        ) : Controller
    {


        //Employee view Leave Requests
        public async Task<IActionResult> Index()
        {
           
            var model = await _leaveRequestsService.GetEmployeeLeaveRequests();

            return View(model);
        }

        //Employee Create requests

        public async Task<IActionResult> Create(Guid? leaveTypeId)
        {
            var leaveTypes = await _leaveTypeService.GetAll();
            var leaveTypesList = new SelectList(leaveTypes, "Id", "Name", leaveTypeId);
            var model = new LeaveRequestCreateVM 
            {
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                LeaveTypes = leaveTypesList


            };
            
            return View(model);
        }
        //Employee Create request
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("LeaveRequests/Create")]
        public async Task<IActionResult> Create(LeaveRequestCreateVM model)
        {
            if(await _leaveRequestsService.RequestDateExceedAllocation(model)){
                ModelState.AddModelError(string.Empty, "You have exceed your allocation");
                ModelState.AddModelError(nameof(model.EndDate), "The Number of Days Requested is Invalid");
            }

            if(ModelState.IsValid)
            {
                await _leaveRequestsService.CreateLeaveRequest(model);
                return  RedirectToAction(nameof(Index));

            }
            var leaveTypes = await _leaveTypeService.GetAll();
            model.LeaveTypes = new SelectList(leaveTypes, "Id", "Name");

            return View(model);
        }

        //Employee Create request   
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Cancel(Guid Id)
        {
            await _leaveRequestsService.CancelLeaveRequest(Id);
            return RedirectToAction(nameof(Index));
        }
        //Admin/supervisor review request
        
        [Authorize(Policy ="AdminSupervisorOnly")]
        public async Task<IActionResult> ListRequests(){
            var model = await _leaveRequestsService.AdminGetAllLeaveRequests();
            return View(model);
        }

        public async Task<IActionResult> Review(Guid id)
        {
           var model= await _leaveRequestsService.GetLeaveRequestForReview(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Review(Guid id, bool approved)
        {
            await _leaveRequestsService.ReviewLeaveRequest(id, approved);
            return RedirectToAction(nameof(ListRequests));
        }
     
    }
}