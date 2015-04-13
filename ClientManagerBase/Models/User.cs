using System.ComponentModel.DataAnnotations;

namespace ClientManagerBase.Models
{
    /// <summary>
    /// Users record.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        /// <value>
        /// The ID.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The user name.
        /// </value>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the e-mail.
        /// </summary>
        /// <value>
        /// The e-mail.
        /// </value>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password hash.
        /// </summary>
        /// <value>
        /// The password hash.
        /// </value>
        [Required]
        public string PasswordHash { get; set; }

        /// <summary>
        /// Gets or sets the representation for email sending.
        /// </summary>
        /// <value>
        /// The representation.
        /// </value>
        public string Representation { get; set; }
    }
}
