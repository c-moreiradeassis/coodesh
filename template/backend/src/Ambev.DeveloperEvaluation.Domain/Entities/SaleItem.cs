using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents an item within a sale transaction.
/// </summary>
public class SaleItem : BaseEntity
{
    /// <summary>
    /// Gets or sets the sale identifier this item belongs to.
    /// </summary>
    public Guid SaleId { get; set; }

    /// <summary>
    /// Gets or sets the product information using External Identities pattern.
    /// </summary>
    public Product Product { get; set; } = new();

    /// <summary>
    /// Gets or sets the quantity of products sold.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the unit price of the product.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Gets or sets the discount applied to this item.
    /// </summary>
    public decimal Discount { get; set; }

    /// <summary>
    /// Gets or sets whether this item is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// Gets the total amount for this item (quantity * unit price - discount).
    /// </summary>
    public decimal TotalAmount => (Quantity * UnitPrice) - Discount;

    public SaleItem() { }

    /// <summary>
    /// Calculates and applies the appropriate discount based on business rules.
    /// </summary>
    public void ApplyDiscount()
    {
        if (Quantity < 4)
        {
            Discount = 0;
            return;
        }

        if (Quantity >= 4 && Quantity < 10)
        {
            Discount = (Quantity * UnitPrice) * 0.10m;
        }
        else if (Quantity >= 10 && Quantity <= 20)
        {
            Discount = (Quantity * UnitPrice) * 0.20m;
        }
        else
        {
            throw new InvalidOperationException("Cannot sell more than 20 identical items");
        }
    }

    /// <summary>
    /// Validates the item according to business rules.
    /// </summary>
    public void ValidateBusinessRules()
    {
        if (Quantity > 20)
            throw new InvalidOperationException("Cannot sell more than 20 identical items");

        if (Quantity < 4 && Discount > 0)
            throw new InvalidOperationException("Purchases below 4 items cannot have a discount");
    }
}