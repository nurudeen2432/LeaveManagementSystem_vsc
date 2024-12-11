using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.Web.Models.LeaveTypes;

public class LeaveTypeCreateVM
{
    [Required]
    [Length(4, 150, ErrorMessage = "You have violated the required number of characters")]
    public string Name { get; set; } = default!;

[Required]
[Range(1, 90)]
[Display(Name ="Maximum Allocation of Days")]
    public string NumberOfDays { get; set; } = default!;


}