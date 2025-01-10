using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.Data;

public class LeaveRequest : BaseEntity
{
    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public LeaveType? LeaveType { get; set; }

    public Guid LeaveTypeId { get; set; }

    public LeaveRequestStatus? LeaveRequestStatus { get; set; }

    public Guid LeaveRequestStatusId { get; set; }

    public ApplicationUser? Employee { get; set; }

    public string EmployeeId { get; set; } = default!;

    public ApplicationUser? Reviewer { get; set; }

    public string? ReviewerId { get; set; }

    [StringLength(100)]
    public string? RequestComments { get; set; }
}
