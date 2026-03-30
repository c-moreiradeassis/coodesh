using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class SaleItemTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Product entities.
    /// </summary>
    private static readonly Faker<Product> ProductFaker = new Faker<Product>()
        .RuleFor(p => p.ExternalId, f => f.Random.Guid())
        .RuleFor(p => p.Name, f => f.Commerce.ProductName())
        .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
        .RuleFor(p => p.Category, f => f.Commerce.Categories(1).First());

    /// <summary>
    /// Configures the Faker to generate valid SaleItem entities.
    /// The generated sale items will have valid:
    /// - SaleId (random GUID)
    /// - Product (valid Product entity)
    /// - Quantity (between 1 and 20)
    /// - UnitPrice (between 1.00 and 1000.00)
    /// - Discount (initially 0)
    /// - IsCancelled (false)
    /// </summary>
    private static readonly Faker<SaleItem> SaleItemFaker = new Faker<SaleItem>()
        .RuleFor(si => si.SaleId, f => f.Random.Guid())
        .RuleFor(si => si.Product, f => ProductFaker.Generate())
        .RuleFor(si => si.Quantity, f => f.Random.Number(1, 20))
        .RuleFor(si => si.UnitPrice, f => Math.Round(f.Random.Decimal(1.0m, 1000.0m), 2))
        .RuleFor(si => si.Discount, f => 0m)
        .RuleFor(si => si.IsCancelled, f => false);

    /// <summary>
    /// Generates a valid SaleItem entity with randomized data.
    /// The generated sale item will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid SaleItem entity with randomly generated data.</returns>
    public static SaleItem GenerateValidSaleItem()
    {
        return SaleItemFaker.Generate();
    }

    /// <summary>
    /// Generates a valid SaleItem with a specific quantity for discount testing.
    /// </summary>
    /// <param name="quantity">The quantity for the sale item</param>
    /// <returns>A valid SaleItem with specified quantity.</returns>
    public static SaleItem GenerateSaleItemWithQuantity(int quantity)
    {
        var saleItem = SaleItemFaker.Generate();
        saleItem.Quantity = quantity;
        saleItem.UnitPrice = 10.00m; // Fixed price for easier calculation
        saleItem.Discount = 0m; // Reset discount
        return saleItem;
    }

    /// <summary>
    /// Generates a valid SaleItem with a specific unit price for testing.
    /// </summary>
    /// <param name="unitPrice">The unit price for the sale item</param>
    /// <returns>A valid SaleItem with specified unit price.</returns>
    public static SaleItem GenerateSaleItemWithUnitPrice(decimal unitPrice)
    {
        var saleItem = SaleItemFaker.Generate();
        saleItem.UnitPrice = unitPrice;
        saleItem.Quantity = 5; // Fixed quantity
        saleItem.Discount = 0m; // Reset discount
        return saleItem;
    }

    /// <summary>
    /// Generates a SaleItem with pre-applied discount for testing scenarios.
    /// </summary>
    /// <param name="quantity">The quantity for the sale item</param>
    /// <param name="discount">The discount amount</param>
    /// <returns>A SaleItem with specified quantity and discount.</returns>
    public static SaleItem GenerateSaleItemWithDiscount(int quantity, decimal discount)
    {
        var saleItem = SaleItemFaker.Generate();
        saleItem.Quantity = quantity;
        saleItem.UnitPrice = 10.00m;
        saleItem.Discount = discount;
        return saleItem;
    }

    /// <summary>
    /// Generates a valid Product entity with randomized data.
    /// </summary>
    /// <returns>A valid Product entity with randomly generated data.</returns>
    public static Product GenerateValidProduct()
    {
        return ProductFaker.Generate();
    }

    /// <summary>
    /// Generates an invalid SaleItem for testing validation scenarios.
    /// The invalid SaleItem will have:
    /// - Quantity over the maximum limit (>20)
    /// - Zero or negative unit price
    /// </summary>
    /// <returns>An invalid SaleItem for testing negative scenarios.</returns>
    public static SaleItem GenerateInvalidSaleItem()
    {
        return new SaleItem
        {
            SaleId = Guid.NewGuid(),
            Product = GenerateValidProduct(),
            Quantity = 25, // Invalid: over 20 limit
            UnitPrice = -1.0m, // Invalid: negative price
            Discount = 0m,
            IsCancelled = false
        };
    }

    /// <summary>
    /// Generates a SaleItem with quantity over the business rule limit.
    /// </summary>
    /// <returns>A SaleItem with invalid quantity for business rule testing.</returns>
    public static SaleItem GenerateSaleItemWithExcessiveQuantity()
    {
        var saleItem = SaleItemFaker.Generate();
        saleItem.Quantity = 25; // Over the 20 item limit
        saleItem.UnitPrice = 10.00m;
        saleItem.Discount = 0m;
        return saleItem;
    }

    /// <summary>
    /// Generates a SaleItem with discount applied to low quantity (invalid scenario).
    /// </summary>
    /// <returns>A SaleItem with invalid discount application.</returns>
    public static SaleItem GenerateSaleItemWithInvalidDiscount()
    {
        var saleItem = SaleItemFaker.Generate();
        saleItem.Quantity = 3; // Below 4 items
        saleItem.UnitPrice = 10.00m;
        saleItem.Discount = 5.00m; // Invalid: discount on low quantity
        return saleItem;
    }
}