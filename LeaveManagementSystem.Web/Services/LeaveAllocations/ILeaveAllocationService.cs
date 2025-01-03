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
      Task<EmployeeAllocationVM> GetEmployeeAllocations(Guid? userid);
      Task<List<EmployeeListVM>> GetEmployees();
        
    }
}