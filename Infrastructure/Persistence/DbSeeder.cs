using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Persistence;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (context.Incidents.Any()) return;

        var incidents = new List<Incident>
        {
            new Incident
            {
                Title = "Caída del Servidor de Autenticación",
                Description = "El servicio de Identity no responde a las peticiones JWT.",
                Severity = Severity.High,
                Status = IncidentStatus.Resolved,
                CreatedAt = DateTime.UtcNow.AddDays(-2),
                ResolvedAt = DateTime.UtcNow.AddDays(-1),
                PostMortem = new PostMortem
                {
                    RootCause = "Falta de memoria en el contenedor Docker debido a un leak.",
                    LessonsLearned = "Implementar límites de recursos en Kubernetes."
                }
            },
            new Incident
            {
                Title = "Error 500 en Pasarela de Pagos",
                Description = "Los clientes de Brasil no pueden procesar tarjetas Visa.",
                Severity = Severity.Low,
                Status = IncidentStatus.Investigating,
                CreatedAt = DateTime.UtcNow.AddHours(-5)
            },
            new Incident
            {
                Title = "Lentitud en el Dashboard",
                Description = "La carga de gráficos tarda más de 10 segundos.",
                Severity = Severity.Medium,
                Status = IncidentStatus.Reported,
                CreatedAt = DateTime.UtcNow.AddMinutes(-30)
            }
        };

        await context.Incidents.AddRangeAsync(incidents);
        await context.SaveChangesAsync();
    }
}