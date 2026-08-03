using Application.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options => 
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IIncidentRepository,IncidentRepository>();
        services.AddScoped<ITeamMemberRepository,TeamMemberRepository>();
        return services;
    }
}