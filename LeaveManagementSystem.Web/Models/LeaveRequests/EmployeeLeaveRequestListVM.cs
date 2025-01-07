using System.ComponentModel;

namespace LeaveManagementSystem.Web.Models.LeaveRequests
{
    public class EmployeeLeaveRequestListVM
    {
        [Display(Name = "Total Number of Requests")]
        public int TotalRequest { get; set; }
  
        [DisplayName("Total Number of Approved Requests")]

        public int ApprovedRequests { get; set; }

        [DisplayName("Pending Requests")]
        public int PendingRequests { get; set; }

        [DisplayName("Rejected Requests")]

        public int RejectedRequests { get; set; }

        public List<LeaveRequestListVM>? LeaveRequests { get; set; } = new List<LeaveRequestListVM>();
    }
}