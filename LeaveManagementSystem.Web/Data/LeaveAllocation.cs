using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveManagementSystem.Web.Data
{
    public class LeaveAllocation :BaseEntity
    {
       

        public LeaveType? LeaveType { get; set; }

        public Guid LeaveTypeId { get; set; }

        public ApplicationUser Employee { get; set; }

        public string EmployeeId { get; set; }

        public Period Period { get; set; }

        public Guid PeriodId    { get; set; }

        public int Days {get; set;}



        
    }
}