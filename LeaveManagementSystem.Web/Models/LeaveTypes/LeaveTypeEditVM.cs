using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.Web.Models.LeaveTypes
{

    public class LeaveTypeEditVM
    {
        public Guid Id { get; set; }
        [Required]
        [Length(4, 150, ErrorMessage = "You have violated the length requirement")]
        public string Name { get; set; } = default!;

        [Required]
        [Range(1, 90)]
        public int NumberOfDays { get; set; } = default!;

    }

}