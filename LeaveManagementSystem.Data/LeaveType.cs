using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveManagementSystem.Data;

public class LeaveType
{

    public Guid Id {get; set;}
    
    [MaxLength(150)]
    public string Name {get; set;} = default!;

    public int NumberOfDays {get; set;}

    public List<LeaveAllocation>? LeaveAllocations {get; set;}   

}