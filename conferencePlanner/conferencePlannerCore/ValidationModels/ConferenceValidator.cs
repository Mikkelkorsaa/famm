using conferencePlannerCore.Models;
using FluentValidation;

namespace conferencePlannerCore.ValidationModels;
public class ConferenceValidator : AbstractValidator<Conference>
{
    public ConferenceValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Conference name is required");

        RuleFor(x => x.AbstractDeadLine)
            .NotEmpty()
            .WithMessage("Abstract deadline is required")
            .Must(date => date > DateTime.Now)
            .WithMessage("Abstract deadline must be in the future");

        RuleFor(x => x.ReviewDeadline)
            .NotEmpty()
            .WithMessage("Review deadline is required")
            .Must(date => date > DateTime.Now)
            .WithMessage("Review deadline must be in the future")
            .GreaterThan(x => x.AbstractDeadLine)
            .WithMessage("Review deadline must be after abstract deadline");

        RuleFor(x => x.StartDate)
            .NotEmpty()
            .WithMessage("Start date is required")
            .Must(date => date > DateTime.Now)
            .WithMessage("Start date must be in the future");

        RuleFor(x => x.EndDate)
            .NotEmpty()
            .WithMessage("End date is required")
            .Must(date => date > DateTime.Now)
            .WithMessage("End date must be in the future")
            .GreaterThan(x => x.StartDate)
            .WithMessage("End date must be after start date");

        RuleFor(x => x.Category)
            .NotEmpty()
            .WithMessage("At least one category is required")
            .Must(categories => categories.Count > 0)
            .WithMessage("At least one category must be selected");

        RuleFor(x => x.ReviewCriteria)
            .NotEmpty()
            .WithMessage("At least one review criterion is required")
            .Must(criteria => criteria.Count > 0)
            .WithMessage("At least one review criterion must be specified");
    }
}

