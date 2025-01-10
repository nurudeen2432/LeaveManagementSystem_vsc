using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.Application.Models.LeaveTypes;

public class DetailsVM : BaseLeaveTypeVM
{

[Required]
[Length(4, 150, ErrorMessage = "You have violated the required number of characters")]
public string Name {get; set;} = default!;


[Display(Name ="Maximum Allocation of Days")]
public int NumberOfDays  {get; set;} = default!;


}