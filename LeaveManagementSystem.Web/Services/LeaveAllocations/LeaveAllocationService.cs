using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LeaveManagementSystem.Web.Models.LeaveAllocations;
using LeaveManagementSystem.Web.Services.Periods;
using LeaveManagementSystem.Web.Services.Users;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Web.Services.LeaveAllocations
{
    public class LeaveAllocationService(
        ApplicationDbContext _context,
        IMapper _mapper,
        IPeriodsServices _periodsServices,
        IUserService _userService
        ) : ILeaveAllocationService
    {
        public async Task AllocateLeave(string employeeId)
        {
            //get all the leave types

            var leaveTypes = await _context.LeaveTypes
                                    .Where(q => !q.LeaveAllocations.Any(
                                    x => x.EmployeeId == employeeId
                                    )).
                                ToListAsync();


            //get the current period based on the year

            

            var period = await _periodsServices.GetCurrentPeriod();

            var monthsRemaining = period.EndDate.Month - DateTime.Now.Month;

            //calculate leave based on the number of month left



            //foreach leave type, create an allocation entry

            foreach (var leaveType in leaveTypes)
            {
                var allocationExists = await IsAllocationExists(employeeId, period.Id, leaveType.Id);

                if (allocationExists)
                {
                    continue;
                }

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
            var user = await _userService.GetLoggedInUser();
            var currentDate = DateTime.Now;
            var leaveAllocation = await _context.LeaveAllocations
            .Include(q => q.LeaveType)
            .Include(q => q.Period)
            .Include(q => q.Employee)
            .Where(q => q.EmployeeId == user.Id && q.Period.EndDate.Year == currentDate.Year)
            .ToListAsync();

            return leaveAllocation;
        }



        public async Task<EmployeeAllocationVM> GetEmployeeAllocations(string? userid)
        {

            var user = string.IsNullOrEmpty(userid)
             ? await _userService.GetLoggedInUser()
              : await _userService.GetUserById(userid);

            var allocations = await GetAllocation(user.Id);

            //we are mapping from list of List leave allocation object into a list of Leave allocation VM object


            var allocationVMList = _mapper.Map<List<LeaveAllocation>, List<LeaveAllocationVM>>(allocations);

            var leaveTypeCount = await _context.LeaveTypes.CountAsync();


            var employeeVm = new EmployeeAllocationVM
            {
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id,
                LeaveAllocations = allocationVMList,
                IsCompletedAllocation = leaveTypeCount == allocationVMList.Count()
            };

            return employeeVm;
        }


        public async Task<LeaveAllocationEditVM> GetEmployeeAllocation(Guid allocationId)
        {
            var allocation = await _context.LeaveAllocations.
                                    Include(q => q.LeaveType)
                                    .Include(q => q.Employee)
                                    .FirstOrDefaultAsync(q => q.Id == allocationId);

            var model = _mapper.Map<LeaveAllocationEditVM>(allocation);

            return model;
        }

        public async Task<List<EmployeeListVM>> GetEmployees()
        {
            var users = await _userService.GetEmployees();

            var employee = _mapper.Map<List<ApplicationUser>, List<EmployeeListVM>>(users.ToList());

            return employee;
        }


        public async Task EditAllocation(LeaveAllocationEditVM allocationEditVM)
        
        {
            //null coalescing expression
            var leaveAllocation = await GetEmployeeAllocation(allocationEditVM.Id) ?? throw new Exception("Leave allocation record does not exist");

            leaveAllocation.Days = allocationEditVM.Days;

            // _context.Update(leaveAllocation);

            await _context.LeaveAllocations
                    .Where(q => q.Id == allocationEditVM.Id)
                    .ExecuteUpdateAsync(s => s.SetProperty(e => e.Days, allocationEditVM.Days));

        }
   
      public async Task<LeaveAllocation> GetCurrentAllocation(Guid leaveTypeId, string employeeId)
   {
       var period = await _periodsServices.GetCurrentPeriod();

       var allocation = await _context.LeaveAllocations.FirstAsync(q => q.LeaveTypeId == leaveTypeId && q.EmployeeId == employeeId
       && q.PeriodId == period.Id
       );

       return allocation;
   }
        private async Task<List<LeaveAllocation>> GetAllocation(string? userid)
        {

            var currentDate = DateTime.Now;

            var leaveAllocations = await _context.LeaveAllocations
                .Include(q => q.LeaveType)
                .Include(q => q.Period)
                .Where(q => q.EmployeeId == userid && q.Period.EndDate.Year == currentDate.Year)
                .ToListAsync();

            return leaveAllocations;
        }

        private async Task<bool> IsAllocationExists(string userid, Guid periodId, Guid leaveTypeId)
        {
            var exists = await _context.LeaveAllocations.AnyAsync(
                q => q.EmployeeId == userid
                && q.Period.Id == periodId
                && q.LeaveTypeId == leaveTypeId

            );
            return exists;
        }

    }
}