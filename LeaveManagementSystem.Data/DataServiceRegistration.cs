using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystem.Data;

public static class DataServiceRegistration
{
    public static IServiceCollection AddDataServices( this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Could not find Connection String");

        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));


        services.AddDatabaseDeveloperPageExceptionFilter();

        return services;
    }
}
