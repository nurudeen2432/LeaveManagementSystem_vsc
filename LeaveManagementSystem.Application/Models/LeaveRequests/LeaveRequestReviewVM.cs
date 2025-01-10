using LeaveManagementSystem.Application.Models.LeaveAllocations;

namespace LeaveManagementSystem.Application.Models.LeaveRequests
{
    public class LeaveRequestReviewVM: LeaveRequestListVM
    {
        public EmployeeListVM? Employee { get; set; } = new EmployeeListVM();
    }
}