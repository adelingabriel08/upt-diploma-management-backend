using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using UPT.Diploma.Management.Application.Middlewares;
using UPT.Diploma.Management.Persistence.Extensions;

namespace UPT.Diploma.Management.Application.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        string connectionString)
    {
        services.AddPersistenceWithIdentity(connectionString);
        services.AddMediatR(c => c.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        return services;
    }
    
    public static IApplicationBuilder UseApplicationMiddlewares(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}