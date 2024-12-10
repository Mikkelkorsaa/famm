using conferencePlannerCore.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace conferencePlannerCore.ValidationModels;
public class UserLoginValidator : AbstractValidator<User>
{
    public UserLoginValidator()
    {
        RuleFor(x => x.Email)
         .NotEmpty()
         .WithMessage("Email må ikke være tom");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Adgangskode må ikke være tom");
    }
}

