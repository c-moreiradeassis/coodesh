using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.Dtos;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class CreateSaleHandlerTestData
{
    /// <summary>
    /// Configures the Faker to generate valid ProductDto entities.
    /// </summary>
    private static readonly Faker<ProductDto> productDtoFaker = new Faker<ProductDto>()
        .RuleFor(p => p.ExternalId, f => f.Random.Guid())
        .RuleFor(p => p.Name, f => f.Commerce.ProductName())
        .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
        .RuleFor(p => p.Category, f => f.Commerce.Categories(1).First());

    /// <summary>
    /// Configures the Faker to generate valid SaleItemDto entities.
    /// </summary>
    private static readonly Faker<SaleItemDto> saleItemDtoFaker = new Faker<SaleItemDto>()
        .RuleFor(si => si.Product, f => productDtoFaker.Generate())
        .RuleFor(si => si.Quantity, f => f.Random.Number(1, 20))
        .RuleFor(si => si.UnitPrice, f => Math.Round(f.Random.Decimal(1.0m, 1000.0m), 2));

    /// <summary>
    /// Configures the Faker to generate valid CustomerDto entities.
    /// </summary>
    private static readonly Faker<CustomerDto> customerDtoFaker = new Faker<CustomerDto>()
        .RuleFor(c => c.ExternalId, f => f.Random.Guid())
        .RuleFor(c => c.Name, f => f.Person.FullName)
        .RuleFor(c => c.Email, f => f.Person.Email);

    /// <summary>
    /// Configures the Faker to generate valid BranchDto entities.
    /// </summary>
    private static readonly Faker<BranchDto> branchDtoFaker = new Faker<BranchDto>()
        .RuleFor(b => b.ExternalId, f => f.Random.Guid())
        .RuleFor(b => b.Name, f => $"Filial {f.Address.City()}")
        .RuleFor(b => b.Location, f => $"{f.Address.City()} - {f.Address.StateAbbr()}");

    /// <summary>
    /// Configures the Faker to generate valid CreateSaleCommand entities.
    /// </summary>
    private static readonly Faker<CreateSaleCommand> createSaleCommandFaker = new Faker<CreateSaleCommand>()
        .RuleFor(c => c.SaleNumber, f => f.Random.Replace("SALE-####-####"))
        .RuleFor(c => c.SaleDate, f => f.Date.Recent(30))
        .RuleFor(c => c.Customer, f => customerDtoFaker.Generate())
        .RuleFor(c => c.Branch, f => branchDtoFaker.Generate())
        .RuleFor(c => c.Items, f => saleItemDtoFaker.Generate(f.Random.Number(1, 5)));

    /// <summary>
    /// Generates a valid CreateSaleCommand with randomized data.
    /// The generated command will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid CreateSaleCommand with randomly generated data.</returns>
    public static CreateSaleCommand GenerateValidCommand()
    {
        return createSaleCommandFaker.Generate();
    }

    /// <summary>
    /// Generates a valid CreateSaleCommand with a specific quantity for discount testing.
    /// </summary>
    /// <param name="quantity">The quantity for the sale item</param>
    /// <returns>A valid CreateSaleCommand with specified quantity.</returns>
    public static CreateSaleCommand GenerateCommandWithQuantity(int quantity)
    {
        var command = createSaleCommandFaker.Generate();
        command.Items = new List<SaleItemDto>
        {
            new SaleItemDto
            {
                Product = productDtoFaker.Generate(),
                Quantity = quantity,
                UnitPrice = 10.00m
            }
        };
        return command;
    }

    /// <summary>
    /// Generates an invalid CreateSaleCommand with empty required fields.
    /// </summary>
    /// <returns>An invalid CreateSaleCommand for testing validation scenarios.</returns>
    public static CreateSaleCommand GenerateInvalidCommand()
    {
        return new CreateSaleCommand
        {
            SaleNumber = string.Empty,
            SaleDate = DateTime.MinValue,
            Customer = new CustomerDto(),
            Branch = new BranchDto(),
            Items = new List<SaleItemDto>()
        };
    }
}