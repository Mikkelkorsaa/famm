using conferencePlannerCore.Models;
using FluentValidation;

namespace conferencePlannerCore.ValidationModels;
public class UserSignUpValidator : AbstractValidator<User>
{
    public UserSignUpValidator()
    {
        RuleFor(x => x.Email)
         .NotEmpty()
         .WithMessage("Email må ikke være tom")
         .EmailAddress()
         .WithMessage("Det skal være en gyldig email");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Adgangskode må ikke være tom")
            .MinimumLength(4)
            .WithMessage("Adgangskode skal være mindst 4 tegn langt")
            .Matches("[0-9]")
            .WithMessage("Adgangskode skal indeholde mindst ét tal")
            .Matches("[!@#$%^&*(),.?\":{}|<>]")
            .WithMessage("Adgangskode skal indeholde mindst ét specialtegn");
    }
}

