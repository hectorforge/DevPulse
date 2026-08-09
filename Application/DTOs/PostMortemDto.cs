namespace Application.DTOs;

public record PostMortemDto(
    Guid Id,
    string RootCause,
    string LessonsLearned,
    Guid IncidentId,
    string? IncidentTitle
);

public record CreatePostDto(
    string RootCause,
    string LessonsLearned,
    Guid IncidentId
);

public record UpdatePostDto(
    Guid Id,
    string RootCause,
    string LessonsLearned
);