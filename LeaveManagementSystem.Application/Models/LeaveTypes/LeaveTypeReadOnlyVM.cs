using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.Application.Models.LeaveTypes
{
    public class LeaveTypeReadOnlyVM : BaseLeaveTypeVM
    {
        

        public string Name { get; set; } = default!;

        [Display(Name = "Maximum Allocation of Days")]
        public int NumberOfDays { get; set; } = default!;
    }
}
