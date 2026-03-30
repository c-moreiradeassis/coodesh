using Ambev.DeveloperEvaluation.Application.Sales.Dtos;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    /// <summary>
    /// Validator for SaleItemDto.
    /// </summary>
    public class SaleItemDtoValidator : AbstractValidator<SaleItemDto>
    {
        public SaleItemDtoValidator()
        {
            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than 0")
                .LessThanOrEqualTo(20)
                .WithMessage("Cannot sell more than 20 identical items");

            RuleFor(x => x.UnitPrice)
                .GreaterThan(0)
                .WithMessage("Unit price must be greater than 0");

            RuleFor(x => x.Product)
                .NotNull()
                .WithMessage("Product information is required");

            RuleFor(x => x.Product.ExternalId)
                .NotEmpty()
                .WithMessage("Product external ID is required");

            RuleFor(x => x.Product.Name)
                .NotEmpty()
                .WithMessage("Product name is required");
        }
    }
}
