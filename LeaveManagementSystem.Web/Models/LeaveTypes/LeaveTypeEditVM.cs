using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.Web.Models.LeaveTypes
{

    public class LeaveTypeEditVM : BaseLeaveTypeVM
    {
        
        [Required]
        [Length(4, 150, ErrorMessage = "You have violated the length requirement")]
        public string Name { get; set; } = default!;

        [Required]
        [Range(1, 90)]
        [Display(Name = "Maximum Allocation of Days")]
        public int NumberOfDays { get; set; } = default!;

    }

}