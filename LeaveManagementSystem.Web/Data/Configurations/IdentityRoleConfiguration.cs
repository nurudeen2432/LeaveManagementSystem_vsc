using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveManagementSystem.Web.Data.Configurations
{
    public class IdentityRoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData( new IdentityRole 
            {
                Id = "6010b61c-2a36-4ef7-a446-a306e25c175f",
                Name = "Employee",
                NormalizedName = "EMPLOYEE"

            },
            new IdentityRole 
            {
                Id = "8fe08bf0-d35f-4ee2-828e-b07041a26940",
                Name = "Supervisor",
                NormalizedName = "SUPERVISOR"
            },
            new IdentityRole 
            {
                Id = "e8b48d34-ea1b-4cfc-82c5-fbebb938b92e",
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR",
            });
        }
    }
}