using Application.DTOs;
using Domain.Common;
using Domain.Enums;

namespace Application.Services.Interfaces;

public interface IIncidentService
{
    Task<Result<Guid>> CreateIncidentAsync(CreateIncidentRequest request);
    Task<(IEnumerable<IncidentDto> Items, int TotalRecords)> GetAllIncidentsAsync(IncidentQueryDto query);
    Task<Result<IncidentDto>> AssignTeamMemberAsync(AssignIncidentRequest request);
    Task<Result<IncidentDto>> GetByIdAsync(Guid id);
    Task<Result<IncidentDto>> DeleteIncidentAsync(Guid id);
    Task<Result<IncidentDto>> UpdateIncidentAsync(UpdateIncidentRequest request);
}