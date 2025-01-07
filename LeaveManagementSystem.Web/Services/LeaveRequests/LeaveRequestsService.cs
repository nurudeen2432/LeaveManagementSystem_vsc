using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LeaveManagementSystem.Web.Models.LeaveRequests;
using Microsoft.EntityFrameworkCore;
using LeaveManagementSystem.Web.Services.LeaveRequests;
using LeaveManagementSystem.Web.Models.LeaveAllocations;
using LeaveManagementSystem.Web.Services.Periods;

namespace LeaveManagementSystem.Web.Services.LeaveRequests
{
    public partial class LeaveRequestsService(
        IMapper _mapper,
        UserManager<ApplicationUser> _userManager,
        IHttpContextAccessor _httpContextAccessor,
        ApplicationDbContext _context,
        ILogger<LeaveRequestsService> _logger,
        IPeriodsServices _periodsServices

        ) : ILeaveRequestsService
    {
        public async Task CancelLeaveRequest(Guid leaveRequestId)
        {
            var leaveRequest = await _context.LeaveRequests.FindAsync(leaveRequestId);

            leaveRequest.LeaveRequestStatusId = LeaveRequestStatusMapper.ToGuid(LeaveRequestStatuses.Canceled);

            //restore allocation days based on cancel request
           
            var period = await _periodsServices.GetCurrentPeriod();
            var numberOfDays = leaveRequest.EndDate.DayNumber - leaveRequest.StartDate.DayNumber;

            var allocation = await _context.LeaveAllocations.FirstAsync(q => q.LeaveTypeId == leaveRequest.LeaveTypeId
            && q.EmployeeId == leaveRequest.EmployeeId
            && q.PeriodId == period.Id
            );

            allocation.Days += numberOfDays;

            await _context.SaveChangesAsync();
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
           
            var period = await _periodsServices.GetCurrentPeriod();
            var numberOfDays = model.EndDate.DayNumber - model.StartDate.DayNumber;
            var allocationToDeduct = await _context.LeaveAllocations.FirstAsync(q => q.LeaveTypeId == model.LeaveTypeId && q.EmployeeId == User.Id
            && q.PeriodId == period.Id
            );
            allocationToDeduct.Days -= numberOfDays;

            await _context.SaveChangesAsync();

        }

        public Task<LeaveRequestListVM> GetAllLeaveRequests()
        {
            throw new NotImplementedException();
        }

        public async Task<EmployeeLeaveRequestListVM> AdminGetAllLeaveRequests()
        {
            var leaveRequests = await _context.LeaveRequests.Include(q => q.LeaveType).ToListAsync();
            var approvedLeaveRequestsCount = leaveRequests.Count(q => q.LeaveRequestStatusId ==
            LeaveRequestStatusMapper.ToGuid(LeaveRequestStatuses.Approved));
            var pendingLeaveRequestsCount = leaveRequests.Count(q => q.LeaveRequestStatusId ==
          LeaveRequestStatusMapper.ToGuid(LeaveRequestStatuses.Pending));
            var rejectedLeaveRequestsCount = leaveRequests.Count(q => q.LeaveRequestStatusId ==
          LeaveRequestStatusMapper.ToGuid(LeaveRequestStatuses.Declined));

            var leaveRequestModel = leaveRequests.Select(q => new LeaveRequestListVM
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

            var model = new EmployeeLeaveRequestListVM
            {
                TotalRequest = leaveRequests.Count,
                ApprovedRequests = approvedLeaveRequestsCount,
                PendingRequests = pendingLeaveRequestsCount,
                RejectedRequests = rejectedLeaveRequestsCount,
                LeaveRequests = leaveRequestModel


            };

            return model;
        }
        public async Task<List<LeaveRequestListVM>> GetEmployeeLeaveRequests()
        {
            var User = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            var leaveRequest = await _context.LeaveRequests
            .Include(q => q.LeaveType)
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
           
            var period = await _periodsServices.GetCurrentPeriod();

            var numberOfDays = model.EndDate.DayNumber - model.StartDate.DayNumber;
            var allocation = await _context.LeaveAllocations.FirstAsync(q => q.LeaveTypeId == model.LeaveTypeId
             && q.EmployeeId == User.Id
             && q.PeriodId == period.Id
             );



            var result = allocation.Days < numberOfDays;
            _logger.LogInformation(">>>>>>>Days Allocated: {Days}, Days Requested: {Requested}, Exceeds Allocation: {Result}",
                        allocation.Days, numberOfDays, result);

            return result;

        }

        public Task ReviewLeaveRequest(LeaveRequestReviewVM model)
        {
            throw new NotImplementedException();
        }

        public async Task<ReviewLeaveRequestVM> GetLeaveRequestForReview(Guid id)
        {
            var leaveRequest = await _context.LeaveRequests.
    Include(q => q.LeaveType)
    .FirstAsync(q => q.LeaveTypeId == id);
            var user = await _userManager.FindByIdAsync(leaveRequest.EmployeeId);

            var model = new ReviewLeaveRequestVM
            {
                StartDate = leaveRequest.StartDate,
                EndDate = leaveRequest.EndDate,
                NumberOfDays = leaveRequest.EndDate.DayNumber - leaveRequest.StartDate.DayNumber,
                LeaveRequestStatus =
                LeaveRequestStatusMapper.GuidToStatusMap.TryGetValue(leaveRequest.LeaveRequestStatusId, out var status)
                                        ? status
                                        : throw new InvalidOperationException($"Unknown LeaveRequestStatusId: {leaveRequest.LeaveRequestStatusId}"),
                Id = leaveRequest.Id,
                LeaveType = leaveRequest.LeaveType.Name,
                RequestComments = leaveRequest.RequestComments,
                Employee = new EmployeeListVM
                {
                    Id = leaveRequest.EmployeeId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email

                }

            };
            return model;
        }

        public async Task ReviewLeaveRequest(Guid leaveRequestId, bool approved)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            var leaveRequest = await _context.LeaveRequests.FindAsync(leaveRequestId);
            leaveRequest.LeaveRequestStatusId = approved
            ? LeaveRequestStatusMapper.ToGuid(LeaveRequestStatuses.Approved)
            : LeaveRequestStatusMapper.ToGuid(LeaveRequestStatuses.Declined);

            leaveRequest.ReviewerId = user.Id;

            if (!approved)
            {

                var allocation = await _context.LeaveAllocations.FirstAsync(q => q.LeaveTypeId == leaveRequest.LeaveTypeId
                && q.EmployeeId == leaveRequest.EmployeeId
                );
                var numberOfDays = leaveRequest.EndDate.DayNumber - leaveRequest.StartDate.DayNumber;
                allocation.Days += numberOfDays;
            }

            await _context.SaveChangesAsync();
        }
    }
}