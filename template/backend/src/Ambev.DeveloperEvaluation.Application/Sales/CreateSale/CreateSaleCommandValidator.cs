using Ambev.DeveloperEvaluation.Application.Sales.Dtos;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Validator for CreateSaleCommand.
/// </summary>
public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
    /// <summary>
    /// Initializes validation rules for CreateSaleCommand.
    /// </summary>
    public CreateSaleCommandValidator()
    {
        RuleFor(x => x.SaleNumber)
            .NotEmpty()
            .WithMessage("Sale number is required")
            .MaximumLength(50)
            .WithMessage("Sale number cannot exceed 50 characters");

        RuleFor(x => x.SaleDate)
            .NotEmpty()
            .WithMessage("Sale date is required")
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("Sale date cannot be in the future");

        RuleFor(x => x.Customer)
            .NotNull()
            .WithMessage("Customer information is required");

        RuleFor(x => x.Customer.ExternalId)
            .NotEmpty()
            .WithMessage("Customer external ID is required");

        RuleFor(x => x.Customer.Name)
            .NotEmpty()
            .WithMessage("Customer name is required")
            .MaximumLength(200)
            .WithMessage("Customer name cannot exceed 200 characters");

        RuleFor(x => x.Customer.Email)
            .NotEmpty()
            .WithMessage("Customer email is required")
            .EmailAddress()
            .WithMessage("Customer email must be valid");

        RuleFor(x => x.Branch)
            .NotNull()
            .WithMessage("Branch information is required");

        RuleFor(x => x.Branch.ExternalId)
            .NotEmpty()
            .WithMessage("Branch external ID is required");

        RuleFor(x => x.Branch.Name)
            .NotEmpty()
            .WithMessage("Branch name is required");

        RuleFor(x => x.Items)
            .NotEmpty()
            .WithMessage("At least one sale item is required");

        RuleForEach(x => x.Items).SetValidator(new SaleItemDtoValidator());
    }
}