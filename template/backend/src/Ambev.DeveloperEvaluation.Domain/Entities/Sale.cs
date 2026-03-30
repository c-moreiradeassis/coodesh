using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a sale transaction in the system.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class Sale : BaseEntity
{
    /// <summary>
    /// Gets or sets the unique sale number.
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the date when the sale was made.
    /// </summary>
    public DateTime SaleDate { get; set; }

    /// <summary>
    /// Gets or sets the customer external identifier and description.
    /// </summary>
    public Customer Customer { get; set; } = new();

    /// <summary>
    /// Gets or sets the branch external identifier and description.
    /// </summary>
    public Branch Branch { get; set; } = new();

    /// <summary>
    /// Gets or sets the list of sale items.
    /// </summary>
    public List<SaleItem> Items { get; set; } = new();

    /// <summary>
    /// Gets or sets whether the sale is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// Gets the total sale amount calculated from all items.
    /// </summary>
    public decimal TotalAmount => Items.Where(x => !x.IsCancelled).Sum(x => x.TotalAmount);

    /// <summary>
    /// Gets the date and time when the sale was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets the date and time of the last update to the sale.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    public Sale() { }
}