using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UPT.Diploma.Management.Domain.Models;
using UPT.Diploma.Management.Persistence.Database;

namespace UPT.Diploma.Management.Persistence.Extensions;

public static class PersistenceExtensions
{
    public static IServiceCollection AddPersistenceWithIdentity(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
                b => b.MigrationsAssembly(Assembly.GetAssembly(typeof(ApplicationDbContext)).FullName)));
        
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }
}