namespace LeaveManagementSystem.Application.Services.Periods
{
    public interface IPeriodsServices
    {
        Task<Period> GetCurrentPeriod();
    }
}
