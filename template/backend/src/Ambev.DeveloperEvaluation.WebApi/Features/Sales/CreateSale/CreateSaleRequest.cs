using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale.Dtos;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleRequest
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
    }
}
