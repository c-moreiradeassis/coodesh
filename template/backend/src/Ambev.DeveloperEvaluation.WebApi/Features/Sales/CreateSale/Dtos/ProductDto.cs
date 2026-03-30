namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale.Dtos
{
    public class ProductDto
    {
        public Guid ExternalId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
    }
}
