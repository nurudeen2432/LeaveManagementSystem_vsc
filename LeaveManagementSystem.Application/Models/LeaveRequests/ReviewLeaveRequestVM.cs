using LeaveManagementSystem.Application.Models.LeaveAllocations;

namespace LeaveManagementSystem.Application.Models.LeaveRequests;

    public class ReviewLeaveRequestVM : LeaveRequestListVM
    {
        public EmployeeListVM Employee { get; set; } =  new EmployeeListVM();

        public string? RequestComments {get; set;}   
    }
