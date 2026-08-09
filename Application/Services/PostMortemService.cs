using Application.DTOs;
using Application.Interfaces;
using Application.Mappings;
using Application.Services.Interfaces;
using Domain.Common;
using Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class PostMortemService : IPostMortemService
{
    private readonly IPostMortemRepository _postMortemRepository;
    private readonly IIncidentRepository _incidentRepository;
    private readonly IValidator<CreatePostDto> _createValidator;
    private readonly IValidator<UpdatePostDto> _updateValidator;
    private readonly ILogger<PostMortemService> _logger;

    public PostMortemService(
        IPostMortemRepository postMortemRepository,
        IIncidentRepository incidentRepository,
        IValidator<CreatePostDto> createValidator,
        IValidator<UpdatePostDto> updateValidator,
        ILogger<PostMortemService> logger)
    {
        _postMortemRepository = postMortemRepository;
        _incidentRepository = incidentRepository;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
        _logger = logger;
    }

    public async Task<(ICollection<PostMortemDto> Items, int TotalRecords)> GetAll(string rootCause, int page = 1, int size = 10)
    {
        _logger.LogDebug("Consultando PostMortems paginados. Filtro causa raíz: {RootCause}", rootCause);

        var (items, totalCount) = await _postMortemRepository.GetPagedAsync(rootCause, page, size);
        
        return (items.ToDtoList().ToList(), totalCount);
    }

    public async Task<Result<PostMortemDto>> Add(CreatePostDto dto)
    {
        _logger.LogInformation("Iniciando creación de PostMortem para Incidente: {IncidentId}", dto.IncidentId);

        var validationResult = await _createValidator.ValidateAsync(dto);
        
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key, 
                    g => g.First().ErrorMessage
                );
            return Result<PostMortemDto?>.Failure("Errores de validación.", errors);
        }

        try
        {
            var incidentExists = await _incidentRepository.GetByIdAsync(dto.IncidentId);
            if (incidentExists == null)
            {
                return Result<PostMortemDto>.Failure("El incidente especificado no existe.");
            }

            var postMortem = PostMortem.Create(dto.RootCause, dto.LessonsLearned, dto.IncidentId);

            await _postMortemRepository.AddAsync(postMortem);
            await _postMortemRepository.SaveChangesAsync();

            return Result<PostMortemDto>.Success(postMortem.ToDto(), "PostMortem creado con éxito.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear PostMortem para Incidente: {IncidentId}", dto.IncidentId);
            return Result<PostMortemDto>.Failure("Ocurrió un error interno al procesar su solicitud.");
        }
    }

    public async Task<Result<PostMortemDto?>> Update(UpdatePostDto dto)
    {
        _logger.LogInformation("Actualizando PostMortem: {PostMortemId}", dto.Id);

        var validationResult = await _updateValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key, 
                    g => g.First().ErrorMessage
                );
            return Result<PostMortemDto?>.Failure("Errores de validación.", errors);
        }

        try
        {
            var postMortem = await _postMortemRepository.GetByIdAsync(dto.Id);
            if (postMortem == null)
            {
                return Result<PostMortemDto?>.Failure("PostMortem no encontrado.");
            }

            postMortem.Update(dto.RootCause, dto.LessonsLearned);

            _postMortemRepository.Update(postMortem);
            await _postMortemRepository.SaveChangesAsync();

            return Result<PostMortemDto?>.Success(postMortem.ToDto(), "PostMortem actualizado correctamente.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar PostMortem: {PostMortemId}", dto.Id);
            return Result<PostMortemDto?>.Failure("Error interno al actualizar.");
        }
    }

    public async Task<Result<PostMortemDto?>> Delete(Guid id)
    {
        var postMortem = await _postMortemRepository.GetByIdAsync(id);
        if (postMortem == null)
        {
            return Result<PostMortemDto?>.Failure("El PostMortem no existe.");
        }

        try
        {
            _postMortemRepository.Delete(postMortem);
            await _postMortemRepository.SaveChangesAsync();

            _logger.LogWarning("PostMortem eliminado: {PostMortemId}", id);
            return Result<PostMortemDto?>.Success(postMortem.ToDto(), "PostMortem eliminado correctamente.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar PostMortem: {PostMortemId}", id);
            return Result<PostMortemDto?>.Failure("Error interno al eliminar.");
        }
    }

    public async Task<Result<PostMortemDto?>> GetById(Guid id)
    {
        var postMortem = await _postMortemRepository.GetByIdAsync(id);

        if (postMortem == null)
        {
            return Result<PostMortemDto?>.Failure($"No se encontró el PostMortem con ID: {id}");
        }

        return Result<PostMortemDto?>.Success(postMortem.ToDto());
    }
}