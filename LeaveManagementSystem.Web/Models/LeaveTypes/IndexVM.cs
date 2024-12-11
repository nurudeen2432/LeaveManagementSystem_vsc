using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.Web.Models.LeaveTypes;

public class IndexVM : BaseLeaveTypeVM
{

[Required]
[Length(4, 150, ErrorMessage = "You have violated the required number of characters")]
public string Name {get; set;} = default!;

[Display(Name ="Maximum Allocation of Days")]
public int NumberOfDays  {get; set;} = default!;


}