using System.ComponentModel.DataAnnotations;

namespace ClientManagerBase.Models.Clients
{
    /// <summary>
    /// Represents client type: warm, cold, potential, etc.
    /// </summary>
    public class ClientType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientType"/> class.
        /// </summary>
        /// <param name="value">The client type.</param>
        public ClientType(string value)
        {
            Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientType"/> class.
        /// </summary>
        public ClientType()
        {
        }

        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        /// <value>
        /// The ID.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The type value.
        /// </value>
        [Required]
        public string Value { get; set; }
    }
}