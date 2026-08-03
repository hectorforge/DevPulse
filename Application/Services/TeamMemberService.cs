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

public class TeamMemberService : ITeamMemberService
{
    private readonly ITeamMemberRepository _repository;
    private readonly ILogger<TeamMemberService> _logger;
    private readonly IValidator<CreateTeamMemberDto> _createTMValidator;
    private readonly IValidator<UpdateTeamMemberDto> _updateTMValidator;

    public TeamMemberService(
        ITeamMemberRepository teamMemberRepository, 
        ILogger<TeamMemberService> logger,
        IValidator<CreateTeamMemberDto> createTMValidator,
        IValidator<UpdateTeamMemberDto> updateTMValidator)
    {
        _repository = teamMemberRepository;
        _logger = logger;
        _createTMValidator = createTMValidator;
        _updateTMValidator = updateTMValidator;
    }

    public async Task<(ICollection<TeamMemberDto> Items, int TotalRecords)> GetAll(
        string? name, 
        Role? role, 
        int page = 1, 
        int size = 10)
    {
        _logger.LogDebug("Consultando miembros del equipo. Filtros: Name={Name}, Role={Role}", name, role);
        
        var (items, totalCount) = await _repository.GetPagedAsync(name, role, page, size);
        var dtos = items.toDto().ToList();
        return (dtos, totalCount);
    }

    public async Task<Result<TeamMemberDto>> Add(CreateTeamMemberDto dto)
    {
        var validation = await _createTMValidator.ValidateAsync(dto);
        if (!validation.IsValid)
        {
            return Result<TeamMemberDto>.Failure("Errores de validación", 
                validation.Errors.ToDictionary(x => x.PropertyName, x => x.ErrorMessage));
        }
        
        var existing = await _repository.GetByEmailAsync(dto.Email);
        if (existing != null)
        {
            return Result<TeamMemberDto>.Failure("El correo electrónico ya está registrado.");
        }
        
        var teamMember = TeamMember.Create(dto.Name, dto.Email, dto.Role);
        
        await _repository.AddAsync(teamMember);
        await _repository.SaveChangesAsync();
        
        _logger.LogInformation("Nuevo miembro de equipo creado: {Email}", dto.Email);
        return Result<TeamMemberDto>.Success(teamMember.toDto());
    }

    public async Task<Result<TeamMemberDto?>> Update(UpdateTeamMemberDto dto)
    {
        var validation = await _updateTMValidator.ValidateAsync(dto);
        if (!validation.IsValid)
        {
            return Result<TeamMemberDto?>.Failure("Errores de validación", 
                validation.Errors.ToDictionary(x => x.PropertyName, x => x.ErrorMessage));
        }
        
        var member = await _repository.GetByIdAsync(dto.Id);
        if (member == null)
        {
            return Result<TeamMemberDto?>.Failure("Miembro de equipo no encontrado.");
        }
        
        member.ChangeName(dto.Name);
        member.ChangeEmail(dto.Email);
        member.ChangeRole(dto.Role);

        _repository.Update(member);
        await _repository.SaveChangesAsync();

        return Result<TeamMemberDto?>.Success(member.toDto());
    }

    public async Task<Result<TeamMemberDto?>> GetById(Guid id)
    {
        var member = await _repository.GetByIdAsync(id, track: false);
        
        if (member == null)
        {
            return Result<TeamMemberDto?>.Failure("Miembro no encontrado.");
        }

        return Result<TeamMemberDto?>.Success(member.toDto());
    }

    public async Task<Result<TeamMemberDto?>> Delete(Guid id)
    {
        var member = await _repository.GetByIdAsync(id);
        
        if (member == null)
        {
            return Result<TeamMemberDto?>.Failure("No se puede eliminar: Miembro no encontrado.");
        }

        // TODO : No permitir eliminar si tiene incidentes asignados

        _repository.Delete(member);
        await _repository.SaveChangesAsync();

        _logger.LogWarning("Miembro de equipo eliminado: {Id}", id);
        return Result<TeamMemberDto?>.Success(member.toDto());
    }
}