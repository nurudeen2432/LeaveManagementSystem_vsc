namespace LeaveManagementSystem.Web.Services.Periods
{
    public interface IPeriodsServices
    {
        Task<Period> GetCurrentPeriod();
    }
}
