using Application.DTOs;
using Application.Interfaces;
using Application.Mappings;
using Application.Services.Interfaces;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class IncidentService : IIncidentService
{
    private readonly IIncidentRepository _incidentRepository;
    private readonly ITeamMemberRepository _teamMemberRepository;
    private readonly IFileStorageService _storageService;
    
    private readonly IValidator<CreateIncidentRequest> _createValidator;
    private readonly IValidator<UpdateIncidentRequest> _updateValidator;
    private readonly ILogger<IncidentService> _logger;

    public IncidentService(
        IIncidentRepository incidentRepository, 
        ITeamMemberRepository teamMemberRepository,
        IFileStorageService storageService,
        IValidator<CreateIncidentRequest> createValidator, 
        IValidator<UpdateIncidentRequest> updateValidator, 
        ILogger<IncidentService> logger)
    {
        _incidentRepository = incidentRepository;
        _teamMemberRepository = teamMemberRepository;
        _storageService = storageService;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
        _logger = logger;
        
    }
    
    public async Task<Result<IncidentDto>> AssignTeamMemberAsync(AssignIncidentRequest request)
    {
        _logger.LogInformation("Asignando miembro {MemberId} al incidente {IncidentId}", 
            request.TeamMemberId, request.IncidentId);
        try
        {
            var incident = await _incidentRepository.GetByIdAsync(request.IncidentId);
            if (incident == null)
            {
                return Result<IncidentDto>.Failure("El incidente especificado no existe.");
            }
            
            var teamMember = await _teamMemberRepository.GetByIdAsync(request.TeamMemberId);
            if (teamMember == null)
            {
                return Result<IncidentDto>.Failure("No se encontró el miembro de equipo.");
            }
            
            incident.AssignTo(teamMember);
            
            _incidentRepository.Update(incident);
            await _incidentRepository.SaveChangesAsync();

            return Result<IncidentDto>.Success(incident.ToDto(), "Miembro asignado correctamente.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al asignar miembro al incidente {IncidentId}", request.IncidentId);
            return Result<IncidentDto>.Failure("Error interno al realizar la asignación.");
        }
    }

    public async Task<Result<Guid>> CreateIncidentAsync(CreateIncidentRequest request)
    {
        _logger.LogInformation("Iniciando creación de incidente: {Title}", request.Title);

        var validationResult = await _createValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return Result<Guid>.Failure("Errores de validación", 
                validationResult.Errors.ToDictionary(e => e.PropertyName, e => e.ErrorMessage));
        }

        try
        {
            var incident = Incident.Create(
                request.Title,
                request.Description,
                request.Severity,
                DateTime.UtcNow,
                request.ScreenshotUrl,
                request.Recommendation
                );

            await _incidentRepository.AddAsync(incident);
            await _incidentRepository.SaveChangesAsync();

            return Result<Guid>.Success(incident.Id, "Incidente creado con éxito.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al persistir el incidente: {Title}", request.Title);
            return Result<Guid>.Failure("Ocurrió un error interno al procesar su solicitud.");
        }
    }

    public async Task<(IEnumerable<IncidentDto> Items, int TotalRecords)> GetAllIncidentsAsync(IncidentQueryDto query)
    {
        _logger.LogDebug("Consultando incidentes paginados. Filtros: {Name}, {Severity}", query.name, query.severity);

        var (items, totalCount) = await _incidentRepository.GetPagedAsync(query.name, query.severity, query.page, query.size);
        
        return (items.ToDtoList(), totalCount);
    }

    public async Task<Result<IncidentDto>> GetByIdAsync(Guid id)
    {
        var incident = await _incidentRepository.GetByIdAsync(id, track: false);

        if (incident == null)
        {
            return Result<IncidentDto>.Failure($"No se encontró el incidente con ID: {id}");
        }

        return Result<IncidentDto>.Success(incident.ToDto());
    }

    public async Task<Result<IncidentDto>> UpdateIncidentAsync(UpdateIncidentRequest request)
    {
        _logger.LogInformation("Actualizando incidente: {IncidentId}", request.Id);

        var validationResult = await _updateValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return Result<IncidentDto>.Failure("Errores de validación", 
                validationResult.Errors.ToDictionary(e => e.PropertyName, e => e.ErrorMessage));
        }
        
        var incident = await _incidentRepository.GetByIdAsync(request.Id);

        if (incident == null)
        {
            return Result<IncidentDto>.Failure("Incidente no encontrado.");
        }
        
        if (!string.IsNullOrEmpty(incident.ScreenshotUrl) && 
            incident.ScreenshotUrl != request.ScreenshotUrl)
        {
            var oldPublicId = GetPublicIdFromUrl(incident.ScreenshotUrl);
            if (oldPublicId != null)
            {
                await _storageService.DeleteImageAsync(oldPublicId);
                _logger.LogInformation("Imagen reemplazada y/o eliminada de Cloudinary: {PublicId}", oldPublicId);
            }
                
        }
        
        if (incident.Title != request.Title)
            incident.ChangeTitle(request.Title);
        
        if(incident.Description != request.Description)
            incident.ChangeDescription(request.Description);
        
        if(incident.Severity != request.Severity)
            incident.ChangeSeverity(request.Severity);
        
        if(incident.Status != request.Status)
            incident.ChangeStatus(request.Status);
        
        if(incident.ScreenshotUrl != request.ScreenshotUrl)
            incident.ChangeScreenshot(request.ScreenshotUrl);
        
        if(incident.Recommendation != request.Recommendation)
            incident.ChangeRecommendation(request.Recommendation);

        _incidentRepository.Update(incident);
        await _incidentRepository.SaveChangesAsync();

        return Result<IncidentDto>.Success(incident.ToDto(), "Incidente actualizado correctamente.");
    }

    public async Task<Result<IncidentDto>> DeleteIncidentAsync(Guid id)
    {
        var incident = await _incidentRepository.GetByIdAsync(id);

        if (incident == null)
        {
            return Result<IncidentDto>.Failure("El incidente no existe.");
        }
        
        var imageUrl = incident.ScreenshotUrl;
        _incidentRepository.Delete(incident);
        await _incidentRepository.SaveChangesAsync();
        if (!string.IsNullOrEmpty(imageUrl))
        {
            var publicId = GetPublicIdFromUrl(imageUrl);
            if (publicId != null)
            {
                await _storageService.DeleteImageAsync(publicId);
                _logger.LogInformation("Imagen eliminada de Cloudinary: {PublicId}", publicId);
            }
        }

        _logger.LogWarning("Incidente eliminado: {IncidentId}", id);
        return Result<IncidentDto>.Success(incident.ToDto(), "Incidente y su imagen eliminados.");
    }
    
    private string? GetPublicIdFromUrl(string url)
    {
        if (string.IsNullOrEmpty(url)) return null;
        try 
        {
            var uri = new Uri(url);
            var segments = uri.AbsolutePath.Split('/');
            var fileNameWithExtension = segments.Last();
            var fileName = Path.GetFileNameWithoutExtension(fileNameWithExtension);
            return $"devpulse/incidents/{fileName}";
        }
        catch { return null; }
    }
}