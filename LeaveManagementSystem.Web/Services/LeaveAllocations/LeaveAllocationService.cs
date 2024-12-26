using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LeaveManagementSystem.Web.Models.LeaveAllocations;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Web.Services.LeaveAllocations
{
    public class LeaveAllocationService(
        ApplicationDbContext _context,
        IHttpContextAccessor _httpContextAccessor,
        IMapper _mapper,
        UserManager<ApplicationUser> _userManager
        ) : ILeaveAllocationService
    {
        public async Task AllocateLeave(string employeeId)
        {
            //get all the leave types

            var leaveTypes = await _context.LeaveTypes.ToListAsync();


            //get the current period based on the year

            var currentDate = DateTime.Now;

            var period = await _context.Periods.SingleAsync(q => q.EndDate.Year == currentDate.Year);

            var monthsRemaining = period.EndDate.Month - currentDate.Month;

            //calculate leave based on the number of month left



            //foreach leave type, create an allocation entry

            foreach (var leaveType in leaveTypes)
            {
                var accuralRate = decimal.Divide(leaveType.NumberOfDays, 12);
                var leaveAllocation = new LeaveAllocation
                {
                    EmployeeId = employeeId,
                    LeaveTypeId = leaveType.Id,
                    PeriodId = period.Id,
                    Days = (int)Math.Ceiling(accuralRate * monthsRemaining)
                };
                _context.Add(leaveAllocation);

            }

            await _context.SaveChangesAsync();


        }


    public async Task<List<LeaveAllocation>> GetLeaveAllocation()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            var currentDate = DateTime.Now;
            var leaveAllocation = await _context.LeaveAllocations
            .Include(q => q.LeaveType)
            .Include(q => q.Period)
            .Include(q => q.Employee)
            .Where(q => q.EmployeeId == user.Id && q.Period.EndDate.Year == currentDate.Year)
            .ToListAsync();

            return leaveAllocation;
        }

        public async Task<List<LeaveAllocation>> GetAllocation(Guid? userid)
        {


            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
            var currentDate = DateTime.Now;

            var leaveAllocations = await _context.LeaveAllocations
                .Include(q => q.LeaveType)
                .Include(q => q.Period)
                .Where(q => q.EmployeeId == user.Id && q.Period.EndDate.Year == currentDate.Year)
                .ToListAsync();

            return leaveAllocations;
        }
    

        public async Task<EmployeeAllocationVM> GetEmployeeAllocations(Guid? userid)
        {
            var allocations = await GetAllocation(userid);

            //we are mapping from list of List leave allocation object into a list of Leave allocation VM object


            var allocationVMList = _mapper.Map<List<LeaveAllocation>, List<LeaveAllocationVM>>(allocations);

            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);

            var employeeVm = new EmployeeAllocationVM
            {
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id,
                LeaveAllocations = allocationVMList
            };

            return employeeVm;
        }

        public async Task<List<EmployeeListVM>> GetEmployees()
        {
            var users = await _userManager.GetUsersInRoleAsync(Roles.Employeee);

            var employee = _mapper.Map<List<ApplicationUser>,List<EmployeeListVM>>(users.ToList());

            return employee;
        }
    }
}