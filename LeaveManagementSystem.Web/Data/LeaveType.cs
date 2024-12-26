using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveManagementSystem.Web.Data;

public class LeaveType
{

    public Guid Id {get; set;}
    
    [MaxLength(150)]
    public string Name {get; set;} = default!;

    public int NumberOfDays {get; set;}

}