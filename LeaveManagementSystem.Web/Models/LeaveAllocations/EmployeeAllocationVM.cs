namespace LeaveManagementSystem.Web.Models.LeaveAllocations
{
    public class EmployeeAllocationVM : EmployeeListVM
    {

        [Display(Name = "Date Of Birth")]
        [DisplayFormat(DataFormatString = "{0: yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateOnly DateOfBirth { get; set; }
        //just to confirm if they have completed allocation for period in effect
        public bool IsCompletedAllocation  { get; set; }
        public List<LeaveAllocationVM> LeaveAllocations { get; set; }


    }
}
