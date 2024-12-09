namespace LeaveManagementSystem.Web.Models.LeaveTypes;

public class DetailsVM 
{
public Guid Id {get; set;}

public string Name {get; set;} = default!;

public int NumberOfDays  {get; set;} = default!;


}