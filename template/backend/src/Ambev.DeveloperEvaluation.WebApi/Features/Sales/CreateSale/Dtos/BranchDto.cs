namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale.Dtos
{
    public class BranchDto
    {
        public Guid ExternalId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
    }
}
