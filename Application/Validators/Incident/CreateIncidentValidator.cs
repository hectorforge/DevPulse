using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class CreateIncidentValidator : AbstractValidator<CreateIncidentRequest>
{
    public CreateIncidentValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("El título no puede estar vacío.")
            .MaximumLength(100).WithMessage("El título no puede tener más de 100 caracteres.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("La descripción es obligatoria.");

        RuleFor(x => x.Severity)
            .IsInEnum().WithMessage("La severidad no es válida.");
        
        RuleFor(x => x.ScreenshotUrl)
            .NotEmpty().WithMessage("La captura de imagen es obligatoria.");
    }
}