using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LeaveManagementSystem.Web.Models.LeaveRequests;
using Microsoft.EntityFrameworkCore;
using LeaveManagementSystem.Web.Services.LeaveRequests;

namespace LeaveManagementSystem.Web.Services.LeaveRequests
{
    public partial class LeaveRequestsService(
        IMapper _mapper,
        UserManager<ApplicationUser> _userManager,
        IHttpContextAccessor _httpContextAccessor,
        ApplicationDbContext _context,
        ILogger<LeaveRequestsService> _logger

        ) : ILeaveRequestsService
    {
        public Task CancelLeaveRequest(Guid leaveRequestId)
        {
            throw new NotImplementedException();
        }

        public async Task CreateLeaveRequest(LeaveRequestCreateVM model)
        {
            //Map data to leave request data model

            var leaveRequest = _mapper.Map<LeaveRequest>(model);

            var User = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);



            // get logged in employee Id
            leaveRequest.EmployeeId = User.Id;
            //set LeaveRequest to pending
            leaveRequest.LeaveRequestStatusId = LeaveRequestStatusMapper.ToGuid(LeaveRequestStatuses.Pending);
            //Save Leave Request
            _context.LeaveRequests.Add(leaveRequest);

            //deduct allocation date based on leave request
            var numberOfDays = model.EndDate.DayNumber - model.StartDate.DayNumber;
            var allocationToDeduct = await _context.LeaveAllocations.FirstAsync(q => q.LeaveTypeId == model.LeaveTypeId && q.EmployeeId == User.Id);
            allocationToDeduct.Days -= numberOfDays;

            await _context.SaveChangesAsync();

        }

        public Task<LeaveRequestListVM> GetAllLeaveRequests()
        {
            throw new NotImplementedException();
        }

        public async Task<List<LeaveRequestListVM>> GetEmployeeLeaveRequests()
        {
            var User = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            var leaveRequest = await _context.LeaveRequests
            .Include(q=>q.LeaveType)
            .Where(q => q.EmployeeId == User.Id)
            .ToListAsync();

            var model = leaveRequest.Select(q => new LeaveRequestListVM
            {
                StartDate = q.StartDate,
                EndDate = q.EndDate,
                Id = q.Id,
                LeaveType = q.LeaveType.Name,
                LeaveRequestStatus = LeaveRequestStatusMapper.GuidToStatusMap.TryGetValue(q.LeaveRequestStatusId, out var status)
        ? status
        : throw new InvalidOperationException($"Unknown LeaveRequestStatusId: {q.LeaveRequestStatusId}"),
                NumberOfDays = q.EndDate.DayNumber - q.StartDate.DayNumber

            }).ToList();

            return model;
        }


        public async Task<bool> RequestDateExceedAllocation(LeaveRequestCreateVM model)
        {
            var User = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            var numberOfDays = model.EndDate.DayNumber - model.StartDate.DayNumber;
            var allocation = await _context.LeaveAllocations.FirstAsync(q => q.LeaveTypeId == model.LeaveTypeId && q.EmployeeId == User.Id);



            var result = allocation.Days < numberOfDays;
            _logger.LogInformation(">>>>>>>Days Allocated: {Days}, Days Requested: {Requested}, Exceeds Allocation: {Result}",
                        allocation.Days, numberOfDays, result);

            return result;

        }

        public Task ReviewLeaveRequest(LeaveRequestReviewVM model)
        {
            throw new NotImplementedException();
        }


    }
}