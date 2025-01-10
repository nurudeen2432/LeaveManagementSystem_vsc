namespace LeaveManagementSystem.Application.Models.Periods
{
    public class PeriodVM
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }
    }
}
