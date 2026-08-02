using Application.DTOs;
using Application.Interfaces;
using Application.Mappings;
using Application.Services.Interfaces;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Severity = Domain.Enums.Severity;

namespace Application.Services;

public class IncidentService : IIncidentService
{
    private readonly IIncidentRepository _repository;
    private readonly IValidator<CreateIncidentRequest> _createValidator;
    private readonly IValidator<UpdateIncidentRequest> _updateValidator;
    private readonly ILogger<IncidentService> _logger;

    public IncidentService(
        IIncidentRepository repository, 
        IValidator<CreateIncidentRequest> createValidator, 
        IValidator<UpdateIncidentRequest> updateValidator, 
        ILogger<IncidentService> logger)
    {
        _repository = repository;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
        _logger = logger;
    }

    public async Task<Result<Guid>> CreateIncidentAsync(CreateIncidentRequest request)
    {
        _logger.LogInformation("Iniciando creación de incidente: {Title} con Severidad: {Severity}", request.Title, request.Severity);

        var validationResult = await _createValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Validación fallida para la creación del incidente. Errores: {Errors}", 
                string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));

            var errors = validationResult.Errors.ToDictionary(e => e.PropertyName, e => e.ErrorMessage);
            return Result<Guid>.Failure("La solicitud contiene errores de validación.", errors);
        }

        try
        {
            var incident = Incident.Create(
                request.Title,
                request.Description,
                request.Severity,
                DateTime.UtcNow);

            await _repository.AddAsync(incident);
            await _repository.SaveChangesAsync();

            _logger.LogInformation("Incidente creado exitosamente con ID: {IncidentId}", incident.Id);
            return Result<Guid>.Success(incident.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inesperado al persistir el incidente: {Title}", request.Title);
            return Result<Guid>.Failure("Ocurrió un error interno al procesar su solicitud.");
        }
    }

    public async Task<IEnumerable<IncidentDto>> GetAllIncidentsAsync(IncidentQueryDto query)
    {
        
        _logger.LogDebug("Consultando lista de incidentes paginada. Filtros - Nombre: {Name}, Severidad: {Severity}, Página: {Page}", 
            query.name ?? "N/A", query.severity?.ToString() ?? "Todas", query.page);

        var (items, totalCount) = await _repository.GetPagedAsync(query.name, query.severity, query.page, query.size);

        _logger.LogInformation("Consulta completada. Se encontraron {TotalCount} incidentes totales. Retornando {CurrentCount} para la página {Page}", 
            totalCount, items.Count(), query.page);

        return items.ToDtoList();
    }

    public async Task<Result<IncidentDto>> GetByIdAsync(Guid id)
    {
        _logger.LogDebug("Buscando incidente por ID: {IncidentId}", id);

        var incident = await _repository.GetByIdAsync(id, track: false);

        if (incident == null)
        {
            _logger.LogWarning("Intento de lectura fallido: No se encontró el incidente con ID: {IncidentId}", id);
            return Result<IncidentDto>.Failure($"No se encontró el incidente con ID: {id}");
        }

        return Result<IncidentDto>.Success(incident.ToDto());
    }

    public async Task<Result<IncidentDto>> UpdateIncidentAsync(UpdateIncidentRequest request)
    {
        _logger.LogInformation("Iniciando actualización del incidente: {IncidentId}", request.Id);

        var validationResult = await _updateValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Validación de actualización fallida para ID: {IncidentId}", request.Id);
            var errors = validationResult.Errors.ToDictionary(e => e.PropertyName, e => e.ErrorMessage);
            return Result<IncidentDto>.Failure("La solicitud contiene errores de validación.", errors);
        }
        
        var incident = await _repository.GetByIdAsync(request.Id);

        if (incident == null)
        {
            _logger.LogWarning("No se pudo actualizar: El incidente {IncidentId} no existe.", request.Id);
            return Result<IncidentDto>.Failure("Incidente no encontrado para actualizar.");
        }
        
        if (incident.Status != (IncidentStatus)request.Status)
        {
            _logger.LogInformation("Cambio de estado detectado para {IncidentId}: {OldStatus} -> {NewStatus}", 
                incident.Id, incident.Status, (IncidentStatus)request.Status);
        }
        
        
        incident.ChangeTitle(request.Title);
        incident.ChangeDescription(request.Description);
        incident.ChangeSeverity(request.Severity);
        incident.ChangeStatus(request.Status);

        _repository.Update(incident);
        await _repository.SaveChangesAsync();

        _logger.LogInformation("Incidente {IncidentId} actualizado correctamente.", incident.Id);

        return Result<IncidentDto>.Success(incident.ToDto());
    }

    public async Task<Result<IncidentDto>> DeleteIncidentAsync(Guid id)
    {
        _logger.LogWarning("Iniciando proceso de eliminación para el incidente: {IncidentId}", id);

        var incident = await _repository.GetByIdAsync(id);

        if (incident == null)
        {
            _logger.LogError("Fallo al eliminar: El incidente {IncidentId} no fue encontrado.", id);
            return Result<IncidentDto>.Failure("El incidente no existe.");
        }

        _repository.Delete(incident);
        await _repository.SaveChangesAsync();

        _logger.LogCritical("Incidente eliminado definitivamente: {IncidentId} - Título: {Title}", id, incident.Title);
        
        return Result<IncidentDto>.Success(incident.ToDto());
    }
}