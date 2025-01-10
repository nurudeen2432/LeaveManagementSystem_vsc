using System.ComponentModel;
using static LeaveManagementSystem.Application.Services.LeaveRequests.LeaveRequestsService;

namespace LeaveManagementSystem.Application.Models.LeaveRequests
{
    public class LeaveRequestListVM
    {
        public Guid Id { get; set; }    

        [DisplayName("Start Date")]
        public DateOnly StartDate { get; set; }

        [DisplayName("End Date")]

        public DateOnly EndDate { get; set; }

        [DisplayName("Total Days")]
        public int NumberOfDays { get; set; }

        [DisplayName("Leave Type")]

        public string LeaveType { get; set; } = string.Empty;

        [DisplayName("Status")]
        public LeaveRequestStatuses LeaveRequestStatus {get; set;}


    }
}