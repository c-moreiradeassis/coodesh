using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events;

/// <summary>
/// Event raised when a sale item is cancelled.
/// </summary>
public class ItemCancelledEvent
{
    public SaleItem SaleItem { get; }
    public Guid SaleId { get; }

    public ItemCancelledEvent(SaleItem saleItem, Guid saleId)
    {
        SaleItem = saleItem;
        SaleId = saleId;
    }
}