using Application.DTOs;
using Application.Interfaces;
using Application.Services.Interfaces;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using Severity = Domain.Enums.Severity;

namespace Application.Services;

public class IncidentService : IIncidentService
{
    private readonly IIncidentRepository _repository;
    private readonly IValidator<CreateIncidentRequest> _validator;

    public IncidentService(IIncidentRepository repository, IValidator<CreateIncidentRequest> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<Result<Guid>> CreateIncidentAsync(CreateIncidentRequest request)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .ToDictionary(
                    e => e.PropertyName,
                    e => e.ErrorMessage
                );

            return Result<Guid>.Failure(
                "La solicitud contiene errores de validación.",
                errors
            );
        }

        var incident = new Incident
        {
            Title = request.Title,
            Description = request.Description,
            Severity = (Severity)request.Severity,
            Status = IncidentStatus.Reported
        };

        await _repository.AddAsync(incident);
        await _repository.SaveChangesAsync();

        return Result<Guid>.Success(incident.Id);
    }

    public async Task<IEnumerable<IncidentDto>> GetAllActiveIncidentsAsync(string name, Severity severity, int page, int size)
    {
        var (items, _) = await _repository.GetPagedAsync(name, severity, page, size);

        return items.Select(i => new IncidentDto(
            i.Id, i.Title, i.Severity.ToString(), i.Status.ToString(), i.CreatedAt));
    }

    public async Task<Result<IncidentDto>> GetByIdAsync(Guid id)
    {
        var incident = await _repository.GetByIdAsync(id, track: false);

        if (incident == null)
            return Result<IncidentDto>.Failure($"No se encontró el incidente con ID: {id}");

        var dto = new IncidentDto(incident.Id, incident.Title, incident.Severity.ToString(), incident.Status.ToString(), incident.CreatedAt);
        return Result<IncidentDto>.Success(dto);
    }

    public async Task<Result<IncidentDto>> UpdateIncidentAsync(UpdateIncidentRequest request)
    {
        var incident = await _repository.GetByIdAsync(request.Id);

        if (incident == null)
            return Result<IncidentDto>.Failure("Incidente no encontrado para actualizar.");
        
        incident.Title = request.Title;
        incident.Description = request.Description;
        incident.Severity = (Severity)request.Severity;
        incident.Status = (IncidentStatus)request.Status;

        _repository.Update(incident);
        await _repository.SaveChangesAsync();

        return Result<IncidentDto>.Success(new IncidentDto(
                incident.Id, 
                incident.Title, 
                incident.Severity.ToString(), 
                incident.Status.ToString(), 
                incident.CreatedAt));
    }

    public async Task<Result<IncidentDto>> DeleteIncidentAsync(Guid id)
    {
        var incident = await _repository.GetByIdAsync(id);

        if (incident == null)
            return Result<IncidentDto>.Failure("El incidente no existe.");

        _repository.Delete(incident);
        await _repository.SaveChangesAsync();
        
        return Result<IncidentDto>.Success(new IncidentDto(
                incident.Id, 
                incident.Title, 
                incident.Severity.ToString(), 
                incident.Status.ToString(), 
                incident.CreatedAt));
    }
}