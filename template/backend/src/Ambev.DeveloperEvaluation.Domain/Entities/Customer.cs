using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents a customer using External Identities pattern.
    /// </summary>
    public class Customer : BaseEntity
    {
        /// <summary>
        /// Gets or sets the external customer identifier.
        /// </summary>
        public Guid ExternalId { get; set; }

        /// <summary>
        /// Gets or sets the customer name (denormalized).
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the customer email (denormalized).
        /// </summary>
        public string Email { get; set; } = string.Empty;

        public Customer() { }
    }
}
