using System.Data.Entity;
using ClientManagerBase.Models;
using ClientManagerBase.Models.Clients;
using ClientManagerBase.Models.Contacts;
using ClientManagerBase.Models.Letters;

namespace ClientManagerBase.Context
{
    /// <summary>
    /// Provides access to database.
    /// </summary>
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        /// <summary>
        /// Gets or sets the clients.
        /// </summary>
        /// <value>
        /// The clients.
        /// </value>
        public IDbSet<Client> Clients { get; set; }

        /// <summary>
        /// Gets or sets the client types.
        /// </summary>
        /// <value>
        /// The client types.
        /// </value>
        public IDbSet<ClientType> ClientTypes { get; set; }

        /// <summary>
        /// Gets or sets the emails.
        /// </summary>
        /// <value>
        /// The emails.
        /// </value>
        public IDbSet<Email> Emails { get; set; }

        /// <summary>
        /// Gets or sets the addresses.
        /// </summary>
        /// <value>
        /// The addresses.
        /// </value>
        public IDbSet<Address> Addresses { get; set; }

        /// <summary>
        /// Gets or sets the phone numbers.
        /// </summary>
        /// <value>
        /// The phone numbers.
        /// </value>
        public IDbSet<PhoneNumber> PhoneNumbers { get; set; }

        /// <summary>
        /// Gets or sets the websites.
        /// </summary>
        /// <value>
        /// The websites.
        /// </value>
        public IDbSet<Website> Websites { get; set; }

        /// <summary>
        /// Gets or sets the letters.
        /// </summary>
        /// <value>
        /// The letters.
        /// </value>
        public IDbSet<Letter> Letters { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        public IDbSet<Tag> Tags { get; set; }

        /// <summary>
        /// Gets or sets the templates.
        /// </summary>
        /// <value>
        /// The templates.
        /// </value>
        public IDbSet<Template> Templates { get; set; }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public IDbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        /// <value>
        /// The settings.
        /// </value>
        public IDbSet<Setting> Settings { get; set; }
    }
}