using Application.DTOs;
using FluentValidation;

namespace Application.Validators.PostMortem;

public class UpdatePostMortemValidator : AbstractValidator<UpdatePostDto>
{
    public UpdatePostMortemValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("El ID del PostMortem es obligatorio.")
            .NotEqual(Guid.Empty).WithMessage("El ID del PostMortem no es válido.");

        RuleFor(x => x.RootCause)
            .NotEmpty().WithMessage("La causa raíz es obligatoria.")
            .MinimumLength(10).WithMessage("La causa raíz debe tener al menos 10 caracteres.")
            .MaximumLength(2000).WithMessage("La causa raíz no puede exceder los 2000 caracteres.");

        RuleFor(x => x.LessonsLearned)
            .NotEmpty().WithMessage("Las lecciones aprendidas son obligatorias.")
            .MinimumLength(10).WithMessage("Las lecciones aprendidas deben tener al menos 10 caracteres.")
            .MaximumLength(4000).WithMessage("Las lecciones aprendidas no pueden exceder los 4000 caracteres.");
    }
}