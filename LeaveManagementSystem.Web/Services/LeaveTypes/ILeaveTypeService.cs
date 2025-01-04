using LeaveManagementSystem.Web.Models.LeaveTypes;

namespace LeaveManagementSystem.Web.Services.LeaveTypes;

public interface ILeaveTypeService 
{
    Task Create (LeaveTypeCreateVM model);
    Task Edit (LeaveTypeEditVM model);

    Task<T?>Get<T>(Guid id) where T : class;

    Task<List<IndexVM>> GetAll();

    Task Remove(Guid? id);

    Task<bool> CheckIfLeaveTypeNameExist(string name);
    Task<bool> DaysExceedMaximum(Guid leaveTypeId, int days);

    Task<bool> CheckIfLeaveTypeNameExistForEdit(LeaveTypeEditVM leaveTypeEdit);
    bool LeaveTypeExists(Guid id);

}