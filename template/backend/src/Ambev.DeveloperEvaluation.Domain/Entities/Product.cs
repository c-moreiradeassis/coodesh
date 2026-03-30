using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents a product using External Identities pattern.
    /// </summary>
    public class Product : BaseEntity
    {
        /// <summary>
        /// Gets or sets the external product identifier.
        /// </summary>
        public Guid ExternalId { get; set; }

        /// <summary>
        /// Gets or sets the product name (denormalized).
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the product description (denormalized).
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the product category (denormalized).
        /// </summary>
        public string Category { get; set; } = string.Empty;

        public Product() { }
    }
}
