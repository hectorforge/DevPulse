using Application.DTOs;
using Application.Services;
using Application.Services.Interfaces;
using Application.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IIncidentService, IncidentService>();
        services.AddScoped<ITeamMemberService, TeamMemberService>();
        services.AddScoped<IPostMortemService, PostMortemService>();
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        
        return services;
    }
}