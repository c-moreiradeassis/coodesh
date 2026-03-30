namespace Ambev.DeveloperEvaluation.Application.Sales.Dtos
{
    public class CustomerDto
    {
        public Guid ExternalId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
