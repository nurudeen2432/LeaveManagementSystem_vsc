using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeaveManagementSystem.Web.Models.LeaveAllocations;

namespace LeaveManagementSystem.Web.Services.LeaveAllocations
{
    public interface ILeaveAllocationService
    {
        Task AllocateLeave(string employeeId);

      Task<List<LeaveAllocation>> GetLeaveAllocation();
      Task<EmployeeAllocationVM> GetEmployeeAllocations(string? userid);
      Task<List<EmployeeListVM>> GetEmployees();

      Task<LeaveAllocationEditVM> GetEmployeeAllocation(Guid allocationId);
      Task<LeaveAllocation> GetCurrentAllocation(Guid leaveTypeId, string employeeId);
        Task EditAllocation(LeaveAllocationEditVM allocationEditVM);
    }
}