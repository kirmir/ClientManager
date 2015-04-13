using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ClientManagerBase.Models.Letters;

namespace ClientManagerBase.Models.Clients
{
    /// <summary>
    /// Represents tag for clients.
    /// </summary>
    public class Tag
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Tag"/> class.
        /// </summary>
        /// <param name="value">The tag name.</param>
        public Tag(string value = null)
        {
            Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Tag"/> class.
        /// </summary>
        public Tag()
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
        /// The value.
        /// </value>
        [Required]
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the attached templates.
        /// </summary>
        /// <value>
        /// The attached templates.
        /// </value>
        public ICollection<Template> Templates { get; set; }

        /// <summary>
        /// Gets or sets the clients.
        /// </summary>
        /// <value>
        /// The clients.
        /// </value>
        public ICollection<Client> Clients { get; set; }
    }
}
