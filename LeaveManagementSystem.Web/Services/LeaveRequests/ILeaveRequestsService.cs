using LeaveManagementSystem.Web.Models.LeaveRequests;

namespace LeaveManagementSystem.Web.Services.LeaveRequests
{
    public interface ILeaveRequestsService
    {
         Task CreateLeaveRequest(LeaveRequestCreateVM model);

        Task<List<LeaveRequestListVM>> GetEmployeeLeaveRequests();

        Task CancelLeaveRequest(Guid leaveRequestId);
        Task<LeaveRequestListVM> GetAllLeaveRequests();
        Task ReviewLeaveRequest (Guid leaveRequestId, bool approved);
        Task<EmployeeLeaveRequestListVM> AdminGetAllLeaveRequests();

        Task<bool> RequestDateExceedAllocation(LeaveRequestCreateVM model);
        Task<ReviewLeaveRequestVM> GetLeaveRequestForReview(Guid id);
    }
}