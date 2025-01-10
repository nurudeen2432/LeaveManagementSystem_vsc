using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveManagementSystem.Data.Configurations
{
    public class LeaveRequestStatusConfiguration : IEntityTypeConfiguration<LeaveRequestStatus>
    {
        public void Configure(EntityTypeBuilder<LeaveRequestStatus> builder)
        {
            builder.HasData(
            new LeaveRequestStatus
            {
                Id = new  Guid("2caff07e-1485-4603-813d-839098f2cc62"),
                Name ="Pending"
            },
             new LeaveRequestStatus
            {
                Id = new  Guid("a07fcf48-e449-4638-94ea-07be6c232f55"),
                Name ="Approved"
            },
             new LeaveRequestStatus
            {
                Id = new  Guid("388d0617-2de5-49a2-9408-f9f5819b0d5a"),
                Name ="Declined"
            },
             new LeaveRequestStatus
            {
                Id = new  Guid("c565c527-ff11-41ef-af5d-6f1d66ba3418"),
                Name ="Canceled"
            }
          
            
            );
        }
    }
}