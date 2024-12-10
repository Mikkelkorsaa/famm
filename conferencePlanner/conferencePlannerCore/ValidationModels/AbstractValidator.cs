using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazored.FluentValidation;
using conferencePlannerCore.Models;
using FluentValidation;

namespace conferencePlannerCore.ValidationModels;
public class AbstractModelValidator : AbstractValidator<Abstract>
{
    public AbstractModelValidator()
    {
        RuleFor(x => x.SenderName)
            .NotEmpty()
            .WithMessage("Navn må ikke være tom");

        RuleFor(x => x.PresenterEmail)
            .NotEmpty()
            .WithMessage("Email må ikke være tom")
            .EmailAddress()
            .WithMessage("Det skal være en gyldig email");

        RuleFor(x => x.Organization)
            .NotEmpty()
            .WithMessage("Firma må ikke være tom");

        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Titlen må ikke være tom");

        RuleFor(x => x.KeyValues)
            .NotEmpty()
            .WithMessage("Nøglebudskaber må ikke være tom")
            .MaximumLength(400)
            .WithMessage("Nøglebudskaber må maks være på 400 tegn inkl. mellemrum");

        RuleFor(x => x.AbstractText)
            .NotEmpty()
            .WithMessage("Abstrakt teksten må ikke være tom")
            .MaximumLength(2000)
            .WithMessage("Teksten må maks være på 2000 tegn inkl. mellemrum");
    }
}

