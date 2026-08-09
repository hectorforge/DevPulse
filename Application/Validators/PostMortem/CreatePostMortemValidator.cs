using Application.DTOs;
using FluentValidation;

namespace Application.Validators.PostMortem;

public class CreatePostMortemValidator : AbstractValidator<CreatePostDto>
{
    public CreatePostMortemValidator()
    {
        RuleFor(x => x.RootCause)
            .NotEmpty().WithMessage("La causa raíz es obligatoria.")
            .MinimumLength(10).WithMessage("La causa raíz debe tener al menos 10 caracteres.")
            .MaximumLength(2000).WithMessage("La causa raíz no puede exceder los 2000 caracteres.");

        RuleFor(x => x.LessonsLearned)
            .NotEmpty().WithMessage("Las lecciones aprendidas son obligatorias.")
            .MinimumLength(10).WithMessage("Las lecciones aprendidas deben tener al menos 10 caracteres.")
            .MaximumLength(4000).WithMessage("Las lecciones aprendidas no pueden exceder los 4000 caracteres.");

        RuleFor(x => x.IncidentId)
            .NotEmpty().WithMessage("El incidente es obligatorio.")
            .NotEqual(Guid.Empty).WithMessage("El incidente no es válido.");
    }
}