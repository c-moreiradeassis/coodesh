using Ambev.DeveloperEvaluation.Application.Sales.Dtos;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale.Dtos
{
    public class SaleItemDto
    {
        public ProductDto Product { get; set; } = new();
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
