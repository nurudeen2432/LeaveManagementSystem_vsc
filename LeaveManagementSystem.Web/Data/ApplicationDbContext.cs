using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Web.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {  
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole 
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
            }
            );

            var hasher = new PasswordHasher<ApplicationUser>();
            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser 
                {
                    Id = "4969fc6f-c322-46ab-bf8b-49da83b718b9",
                    Email="admin@localhost.com",
                    NormalizedEmail="ADMIN@LOCALHOST.COM",
                    UserName="admin@localhost.com",
                    PasswordHash=hasher.HashPassword(null, "P@ssword1"),
                    EmailConfirmed = true,
                    FirstName = "Default",
                    LastName="admin",
                    DateOfBirth= new DateOnly(1964, 12, 01)
                    
                }
            );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "e8b48d34-ea1b-4cfc-82c5-fbebb938b92e",
                    UserId="4969fc6f-c322-46ab-bf8b-49da83b718b9",
                    

                }
            );
    }

    public DbSet<LeaveType>? LeaveTypes {get; set;}
}
