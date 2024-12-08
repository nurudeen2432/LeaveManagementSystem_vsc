using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveManagementSystem.Web.Data;

public class LeaveType
{

    public Guid Id {get; set;}
    
    [Column(TypeName ="nvarchar(150)")]
    public string Name {get; set;} = default!;

    public int NumberOfDays {get; set;}

}