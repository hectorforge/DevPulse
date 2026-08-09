using Application.DTOs;
using Domain.Entities;

namespace Application.Mappings;

public static class PostMortemMappingExtensions
{
    public static PostMortemDto ToDto(this PostMortem entity)
    {
        return new PostMortemDto(
            entity.Id,
            entity.RootCause,
            entity.LessonsLearned,
            entity.IncidentId,
            entity.Incident?.Title ?? "Sin titulo"
        );
    }

    public static IEnumerable<PostMortemDto> ToDtoList(this IEnumerable<PostMortem> entities)
    {
        return entities.Select(ToDto);
    }
}