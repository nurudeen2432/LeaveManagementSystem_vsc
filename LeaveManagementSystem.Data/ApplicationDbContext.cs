﻿using System.Reflection;
using LeaveManagementSystem.Data.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    //Data Seeding OnModelCreating
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }


    public DbSet<LeaveType>? LeaveTypes { get; set; }

    public DbSet<LeaveAllocation> LeaveAllocations { get; set; }

    public DbSet<Period> Periods { get; set; }

    public DbSet<LeaveRequestStatus> LeaveRequestStatuses { get; set; }

    public DbSet<LeaveRequest> LeaveRequests { get; set; }



}
