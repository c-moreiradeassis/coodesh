using Ambev.DeveloperEvaluation.Application.Sales.Dtos;
using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Command for creating a new sale.
/// </summary>
public class CreateSaleCommand : IRequest<CreateSaleResult>
{
    /// <summary>
    /// Gets or sets the sale number.
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the sale date.
    /// </summary>
    public DateTime SaleDate { get; set; }

    /// <summary>
    /// Gets or sets the customer information.
    /// </summary>
    public CustomerDto Customer { get; set; } = new();

    /// <summary>
    /// Gets or sets the branch information.
    /// </summary>
    public BranchDto Branch { get; set; } = new();

    /// <summary>
    /// Gets or sets the sale items.
    /// </summary>
    public List<SaleItemDto> Items { get; set; } = new();

    public ValidationResultDetail Validate()
    {
        var validator = new CreateSaleCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}