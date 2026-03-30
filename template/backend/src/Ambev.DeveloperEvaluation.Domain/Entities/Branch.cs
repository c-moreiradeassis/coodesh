using Ambev.DeveloperEvaluation.Domain.Common;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents a branch using External Identities pattern.
    /// </summary>
    public class Branch : BaseEntity
    {
        /// <summary>
        /// Gets or sets the external branch identifier.
        /// </summary>
        public Guid ExternalId { get; set; }

        /// <summary>
        /// Gets or sets the branch name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the branch location.
        /// </summary>
        public string Location { get; set; } = string.Empty;

        public Branch() { }
    }
}
