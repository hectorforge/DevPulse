using Domain.Entities;
using Domain.Enums;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.TeamMembers.AnyAsync()) return;

        // Members
        var members = new List<TeamMember>
        {
            new TeamMember { 
                Id = Guid.NewGuid(), Name = "Alice Johnson", Email = "alice@company.com", Role = Role.ProjectManager,
                AuditRecord = new SimpleAuditRecord { CreatedAt = DateTime.UtcNow.AddMonths(-6), LastModifiedAt = DateTime.UtcNow.AddMonths(-6) }
            },
            new TeamMember { 
                Id = Guid.NewGuid(), Name = "Bob Smith", Email = "bob@company.com", Role = Role.Developer,
                AuditRecord = new SimpleAuditRecord { CreatedAt = DateTime.UtcNow.AddMonths(-5), LastModifiedAt = DateTime.UtcNow.AddMonths(-5) }
            },
            new TeamMember { 
                Id = Guid.NewGuid(), Name = "Charlie Davis", Email = "charlie@company.com", Role = Role.QualityAssurance,
                AuditRecord = new SimpleAuditRecord { CreatedAt = DateTime.UtcNow.AddMonths(-4), LastModifiedAt = DateTime.UtcNow.AddMonths(-4) }
            },
            new TeamMember { 
                Id = Guid.NewGuid(), Name = "Diana Prince", Email = "diana@company.com", Role = Role.Developer,
                AuditRecord = new SimpleAuditRecord { CreatedAt = DateTime.UtcNow.AddMonths(-3), LastModifiedAt = DateTime.UtcNow.AddMonths(-3) }
            }
        };

        await context.TeamMembers.AddRangeAsync(members);
        
        var incidentId1 = Guid.NewGuid();
        var incidentId2 = Guid.NewGuid();
        var incidentId3 = Guid.NewGuid();
        var incidentId4 = Guid.NewGuid();

        /*
        var incidents = new List<Incident>
        {
            new Incident
            {
                Id = incidentId1,
                Title = "Error de Conexión en Base de Datos",
                Description = "El pool de conexiones llegó al máximo en producción.",
                Severity = Severity.Critical,
                Status = IncidentStatus.Resolved,
                ResolvedAt = DateTime.UtcNow.AddDays(-14),
                AuditRecord = new AuditRecord { 
                    CreatedAt = DateTime.UtcNow.AddDays(-15), 
                    CreatedBy = "System.Monitor",
                    LastModifiedAt = DateTime.UtcNow.AddDays(-14),
                    LastModifiedBy = "Alice Johnson"
                }
            },
            new Incident
            {
                Id = incidentId2,
                Title = "Lentitud en el Checkout",
                Description = "Los usuarios reportan que el botón de pago tarda 30 segundos.",
                Severity = Severity.High,
                Status = IncidentStatus.Resolved,
                ResolvedAt = DateTime.UtcNow.AddDays(-5),
                AuditRecord = new AuditRecord { 
                    CreatedAt = DateTime.UtcNow.AddDays(-6), 
                    CreatedBy = "Charlie Davis",
                    LastModifiedAt = DateTime.UtcNow.AddDays(-5),
                    LastModifiedBy = "Bob Smith"
                }
            },
            new Incident
            {
                Id = incidentId3,
                Title = "Certificado SSL Expirado",
                Description = "El subdominio de API muestra advertencia de seguridad.",
                Severity = Severity.Medium,
                Status = IncidentStatus.Investigating,
                AuditRecord = new AuditRecord { 
                    CreatedAt = DateTime.UtcNow.AddHours(-10), 
                    CreatedBy = "System.Checker",
                    LastModifiedAt = DateTime.UtcNow.AddHours(-2),
                    LastModifiedBy = "Alice Johnson"
                }
            },
            new Incident
            {
                Id = incidentId4,
                Title = "Fuga de Memoria en Microservicio de Auth",
                Description = "Reinicio constante de pods en Kubernetes.",
                Severity = Severity.High,
                Status = IncidentStatus.Reported,
                AuditRecord = new AuditRecord { 
                    CreatedAt = DateTime.UtcNow.AddMinutes(-45), 
                    CreatedBy = "Bob Smith",
                    LastModifiedAt = DateTime.UtcNow.AddMinutes(-45),
                    LastModifiedBy = "Bob Smith"
                }
            }
        };

        await context.Incidents.AddRangeAsync(incidents);

        // Postmortems
        var postMortems = new List<PostMortem>
        {
            new PostMortem
            {
                Id = Guid.NewGuid(),
                IncidentId = incidentId1,
                RootCause = "Configuración incorrecta del Max Pool Size en el Connection String.",
                LessonsLearned = "Escalar la base de datos y revisar logs de telemetría semanalmente.",
                AuditRecord = new AuditRecord { 
                    CreatedAt = DateTime.UtcNow.AddDays(-13), 
                    CreatedBy = "Alice Johnson" 
                }
            },
            new PostMortem
            {
                Id = Guid.NewGuid(),
                IncidentId = incidentId2,
                RootCause = "Un query de SQL no tenía el índice apropiado tras la última migración.",
                LessonsLearned = "Añadir validación de índices en el pipeline de CI/CD.",
                AuditRecord = new AuditRecord { 
                    CreatedAt = DateTime.UtcNow.AddDays(-4), 
                    CreatedBy = "Bob Smith" 
                }
            },

            new PostMortem
            {
                Id = Guid.NewGuid(),
                IncidentId = incidentId3,
                RootCause = "Falta de renovación automática en Let's Encrypt.",
                LessonsLearned = "Configurar alertas de expiración con 30 días de antelación.",
                AuditRecord = new AuditRecord { CreatedAt = DateTime.UtcNow, CreatedBy = "Diana Prince" }
            },
            new PostMortem
            {
                Id = Guid.NewGuid(),
                IncidentId = incidentId4,
                RootCause = "Uso excesivo de caché estática en memoria sin desalojo.",
                LessonsLearned = "Implementar Redis para caché distribuida.",
                AuditRecord = new AuditRecord { CreatedAt = DateTime.UtcNow, CreatedBy = "Bob Smith" }
            }
        };

        await context.PostMortems.AddRangeAsync(postMortems);
        */
        await context.SaveChangesAsync();
    }
}