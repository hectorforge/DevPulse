using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class IncidentQueryValidator : AbstractValidator<IncidentQueryDto>
{
    public IncidentQueryValidator()
    {
        RuleFor(x => x.page).GreaterThanOrEqualTo(1)
            .WithMessage("El numero para la pagina no puede ser negativo");
        
        RuleFor(x => x.size).InclusiveBetween(1, 100)
            .WithMessage("El tamaño de la pagina no puede ser menor que 1 y mayor que 100");
        
        RuleFor(x => x.name).MaximumLength(100)
            .WithMessage("El nombre a consultar no debe exeder los 100 caracteres.");
    }
}