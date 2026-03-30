using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Contains unit tests for the SaleItem entity class.
/// Tests cover discount application, business rule validation, and calculation scenarios.
/// </summary>
public class SaleItemTests
{
    /// <summary>
    /// Tests that no discount is applied when quantity is less than 4 items.
    /// </summary>
    [Theory(DisplayName = "Sale item should have no discount when quantity is below 4")]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void Given_SaleItemWithLowQuantity_When_ApplyDiscount_Then_DiscountShouldBeZero(int quantity)
    {
        // Arrange
        var saleItem = SaleItemTestData.GenerateSaleItemWithQuantity(quantity);

        // Act
        saleItem.ApplyDiscount();

        // Assert
        saleItem.Discount.Should().Be(0m);
    }

    /// <summary>
    /// Tests that 10% discount is applied when quantity is between 4 and 9 items.
    /// </summary>
    [Theory(DisplayName = "Sale item should have 10% discount when quantity is 4-9")]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(9)]
    public void Given_SaleItemWithMediumQuantity_When_ApplyDiscount_Then_DiscountShouldBe10Percent(int quantity)
    {
        // Arrange
        var saleItem = SaleItemTestData.GenerateSaleItemWithQuantity(quantity);
        var expectedDiscount = (quantity * saleItem.UnitPrice) * 0.10m;

        // Act
        saleItem.ApplyDiscount();

        // Assert
        saleItem.Discount.Should().Be(expectedDiscount);
    }

    /// <summary>
    /// Tests that 20% discount is applied when quantity is between 10 and 20 items.
    /// </summary>
    [Theory(DisplayName = "Sale item should have 20% discount when quantity is 10-20")]
    [InlineData(10)]
    [InlineData(15)]
    [InlineData(20)]
    public void Given_SaleItemWithHighQuantity_When_ApplyDiscount_Then_DiscountShouldBe20Percent(int quantity)
    {
        // Arrange
        var saleItem = SaleItemTestData.GenerateSaleItemWithQuantity(quantity);
        var expectedDiscount = (quantity * saleItem.UnitPrice) * 0.20m;

        // Act
        saleItem.ApplyDiscount();

        // Assert
        saleItem.Discount.Should().Be(expectedDiscount);
    }

    /// <summary>
    /// Tests that applying discount with quantity over 20 throws an exception.
    /// </summary>
    [Fact(DisplayName = "Sale item should throw exception when quantity exceeds 20")]
    public void Given_SaleItemWithExcessiveQuantity_When_ApplyDiscount_Then_ShouldThrowException()
    {
        // Arrange
        var saleItem = SaleItemTestData.GenerateSaleItemWithExcessiveQuantity();

        // Act
        var act = () => saleItem.ApplyDiscount();

        // Assert
        act.Should().Throw<InvalidOperationException>()
           .WithMessage("Cannot sell more than 20 identical items");
    }

    /// <summary>
    /// Tests that business rule validation passes for valid sale items.
    /// </summary>
    [Fact(DisplayName = "Business rule validation should pass for valid sale item")]
    public void Given_ValidSaleItem_When_ValidateBusinessRules_Then_ShouldNotThrowException()
    {
        // Arrange
        var saleItem = SaleItemTestData.GenerateValidSaleItem();

        // Act
        var act = () => saleItem.ValidateBusinessRules();

        // Assert
        act.Should().NotThrow();
    }

    /// <summary>
    /// Tests that business rule validation fails when quantity exceeds 20.
    /// </summary>
    [Fact(DisplayName = "Business rule validation should fail when quantity exceeds 20")]
    public void Given_SaleItemWithExcessiveQuantity_When_ValidateBusinessRules_Then_ShouldThrowException()
    {
        // Arrange
        var saleItem = SaleItemTestData.GenerateSaleItemWithExcessiveQuantity();

        // Act
        var act = () => saleItem.ValidateBusinessRules();

        // Assert
        act.Should().Throw<InvalidOperationException>()
           .WithMessage("Cannot sell more than 20 identical items");
    }

    /// <summary>
    /// Tests that business rule validation fails when discount is applied to low quantity items.
    /// </summary>
    [Fact(DisplayName = "Business rule validation should fail when discount is applied to low quantity")]
    public void Given_SaleItemWithInvalidDiscount_When_ValidateBusinessRules_Then_ShouldThrowException()
    {
        // Arrange
        var saleItem = SaleItemTestData.GenerateSaleItemWithInvalidDiscount();

        // Act
        var act = () => saleItem.ValidateBusinessRules();

        // Assert
        act.Should().Throw<InvalidOperationException>()
           .WithMessage("Purchases below 4 items cannot have a discount");
    }

    /// <summary>
    /// Tests that total amount is calculated correctly without discount.
    /// </summary>
    [Fact(DisplayName = "Total amount should be calculated correctly without discount")]
    public void Given_SaleItemWithoutDiscount_When_CalculatingTotalAmount_Then_ShouldReturnQuantityTimesUnitPrice()
    {
        // Arrange
        var saleItem = SaleItemTestData.GenerateSaleItemWithQuantity(3);
        var expectedTotal = saleItem.Quantity * saleItem.UnitPrice;

        // Act
        var totalAmount = saleItem.TotalAmount;

        // Assert
        totalAmount.Should().Be(expectedTotal);
    }

    /// <summary>
    /// Tests that total amount is calculated correctly with discount applied.
    /// </summary>
    [Fact(DisplayName = "Total amount should be calculated correctly with discount")]
    public void Given_SaleItemWithDiscount_When_CalculatingTotalAmount_Then_ShouldReturnQuantityTimesUnitPriceMinusDiscount()
    {
        // Arrange
        var quantity = 5;
        var unitPrice = 10.00m;
        var discount = 5.00m;
        var saleItem = SaleItemTestData.GenerateSaleItemWithDiscount(quantity, discount);
        var expectedTotal = (quantity * unitPrice) - discount;

        // Act
        var totalAmount = saleItem.TotalAmount;

        // Assert
        totalAmount.Should().Be(expectedTotal);
    }

    /// <summary>
    /// Tests the complete discount application workflow for a 10% discount scenario.
    /// </summary>
    [Fact(DisplayName = "Complete workflow should apply 10% discount correctly")]
    public void Given_SaleItemWith5Items_When_ApplyingDiscountWorkflow_Then_ShouldHave10PercentDiscount()
    {
        // Arrange
        var saleItem = SaleItemTestData.GenerateSaleItemWithQuantity(5);
        var originalTotal = saleItem.Quantity * saleItem.UnitPrice;
        var expectedDiscount = originalTotal * 0.10m;
        var expectedTotal = originalTotal - expectedDiscount;

        // Act
        saleItem.ValidateBusinessRules(); // Should not throw
        saleItem.ApplyDiscount();
        var finalTotal = saleItem.TotalAmount;

        // Assert
        saleItem.Discount.Should().Be(expectedDiscount);
        finalTotal.Should().Be(expectedTotal);
    }

    /// <summary>
    /// Tests the complete discount application workflow for a 20% discount scenario.
    /// </summary>
    [Fact(DisplayName = "Complete workflow should apply 20% discount correctly")]
    public void Given_SaleItemWith15Items_When_ApplyingDiscountWorkflow_Then_ShouldHave20PercentDiscount()
    {
        // Arrange
        var saleItem = SaleItemTestData.GenerateSaleItemWithQuantity(15);
        var originalTotal = saleItem.Quantity * saleItem.UnitPrice;
        var expectedDiscount = originalTotal * 0.20m;
        var expectedTotal = originalTotal - expectedDiscount;

        // Act
        saleItem.ValidateBusinessRules(); // Should not throw
        saleItem.ApplyDiscount();
        var finalTotal = saleItem.TotalAmount;

        // Assert
        saleItem.Discount.Should().Be(expectedDiscount);
        finalTotal.Should().Be(expectedTotal);
    }

    /// <summary>
    /// Tests that cancelled sale items don't affect calculations inappropriately.
    /// </summary>
    [Fact(DisplayName = "Cancelled sale item should maintain its properties")]
    public void Given_ValidSaleItem_When_CancelledIsSet_Then_ShouldMaintainOtherProperties()
    {
        // Arrange
        var saleItem = SaleItemTestData.GenerateValidSaleItem();
        var originalQuantity = saleItem.Quantity;
        var originalUnitPrice = saleItem.UnitPrice;
        var originalDiscount = saleItem.Discount;

        // Act
        saleItem.IsCancelled = true;

        // Assert
        saleItem.IsCancelled.Should().BeTrue();
        saleItem.Quantity.Should().Be(originalQuantity);
        saleItem.UnitPrice.Should().Be(originalUnitPrice);
        saleItem.Discount.Should().Be(originalDiscount);
        saleItem.TotalAmount.Should().Be((originalQuantity * originalUnitPrice) - originalDiscount);
    }
}