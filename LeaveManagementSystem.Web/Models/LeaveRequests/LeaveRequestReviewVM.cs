using LeaveManagementSystem.Web.Models.LeaveAllocations;

namespace LeaveManagementSystem.Web.Models.LeaveRequests
{
    public class LeaveRequestReviewVM: LeaveRequestListVM
    {
        public EmployeeListVM? Employee { get; set; } = new EmployeeListVM();
    }
}