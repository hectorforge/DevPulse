using Application.DTOs;
using Domain.Common;
using Domain.Enums;

namespace Application.Services.Interfaces;

public interface IIncidentService
{
    Task<Result<Guid>> CreateIncidentAsync(CreateIncidentRequest request);
    Task<IEnumerable<IncidentDto>> GetAllActiveIncidentsAsync(string? name ,Severity? sevirity,int page, int size);
    Task<Result<IncidentDto>> GetByIdAsync(Guid id);
    Task<Result<IncidentDto>> DeleteIncidentAsync(Guid id);
    Task<Result<IncidentDto>> UpdateIncidentAsync(UpdateIncidentRequest request);
}