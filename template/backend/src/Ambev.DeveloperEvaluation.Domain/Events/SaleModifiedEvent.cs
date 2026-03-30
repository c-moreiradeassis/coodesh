using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events;

/// <summary>
/// Event raised when a sale is modified.
/// </summary>
public class SaleModifiedEvent
{
    public Sale Sale { get; }
    public Sale PreviousSale { get; }

    public SaleModifiedEvent(Sale sale, Sale previousSale)
    {
        Sale = sale;
        PreviousSale = previousSale;
    }
}