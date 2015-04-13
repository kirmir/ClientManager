using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ClientManagerBase.Models.Contacts;

namespace ClientManagerBase.Models.Clients
{
    /// <summary>
    /// Represents client information.
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        /// <value>
        /// The ID.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the personal details.
        /// </summary>
        /// <value>
        /// The personal details.
        /// </value>
        public string Details { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the name of the second.
        /// </summary>
        /// <value>
        /// The name of the second.
        /// </value>
        [Required]
        public string SecondName { get; set; }

        /// <summary>
        /// Gets or sets the name of the company.
        /// </summary>
        /// <value>
        /// The name of the company.
        /// </value>
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the main email.
        /// </summary>
        /// <value>
        /// The main email.
        /// </value>
        [Required]
        public string MainEmail { get; set; }

        /// <summary>
        /// Gets or sets the additional emails.
        /// </summary>
        /// <value>
        /// The additional emails.
        /// </value>
        public ICollection<Email> Emails { get; set; }

        /// <summary>
        /// Gets or sets the addresses.
        /// </summary>
        /// <value>
        /// The addresses.
        /// </value>
        public ICollection<Address> Addresses { get; set; }

        /// <summary>
        /// Gets or sets the phone numbers.
        /// </summary>
        /// <value>
        /// The phone numbers.
        /// </value>
        public ICollection<PhoneNumber> PhoneNumbers { get; set; }

        /// <summary>
        /// Gets or sets the websites.
        /// </summary>
        /// <value>
        /// The websites.
        /// </value>
        public ICollection<Website> Websites { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public ClientType Type { get; set; }

        /// <summary>
        /// Gets or sets the date of client record creation.
        /// </summary>
        /// <value>
        /// The date of client record creation.
        /// </value>
        [Required]
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        public ICollection<Tag> Tags { get; set; }

        /// <summary>
        /// Gets or sets the user who manages client.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public User User { get; set; }

        #region Foreign Keys

        /// <summary>
        /// Gets or sets the type ID.
        /// </summary>
        /// <value>
        /// The type ID.
        /// </value>
        public int TypeId { get; set; }

        /// <summary>
        /// Gets or sets the user ID.
        /// </summary>
        /// <value>
        /// The user ID.
        /// </value>
        public int UserId { get; set; }

        #endregion
    }
}
