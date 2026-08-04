using Application.DTOs;
using Domain.Entities;
using FluentValidation;

namespace Application.Validators.TeamMembers;

public class CreateTeamMemberValidator : AbstractValidator<CreateTeamMemberDto>
{
    public CreateTeamMemberValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(100)
            .WithMessage("Name cannot exceed 100 characters.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Email format is invalid.")
            .MaximumLength(255)
            .WithMessage("Email cannot exceed 255 characters.");

        RuleFor(x => x.Role)
            .IsInEnum()
            .WithMessage("Role is invalid.");

    }  
}