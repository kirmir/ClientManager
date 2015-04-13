using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using ClientManagerBase.Models;
using ClientManagerBase.Models.Clients;
using ClientManagerBase.Models.Contacts;
using ClientManagerBase.Models.Letters;

namespace ClientManagerBase.Context
{
    /// <summary>
    /// Interface for database context.
    /// </summary>
    public interface IDatabaseContext
    {
        /// <summary>
        /// Gets or sets the clients.
        /// </summary>
        /// <value>
        /// The clients.
        /// </value>
        IDbSet<Client> Clients { get; set; }

        /// <summary>
        /// Gets or sets the client types.
        /// </summary>
        /// <value>
        /// The client types.
        /// </value>
        IDbSet<ClientType> ClientTypes { get; set; }

        /// <summary>
        /// Gets or sets the emails.
        /// </summary>
        /// <value>
        /// The emails.
        /// </value>
        IDbSet<Email> Emails { get; set; }

        /// <summary>
        /// Gets or sets the addresses.
        /// </summary>
        /// <value>
        /// The addresses.
        /// </value>
        IDbSet<Address> Addresses { get; set; }

        /// <summary>
        /// Gets or sets the phone numbers.
        /// </summary>
        /// <value>
        /// The phone numbers.
        /// </value>
        IDbSet<PhoneNumber> PhoneNumbers { get; set; }

        /// <summary>
        /// Gets or sets the websites.
        /// </summary>
        /// <value>
        /// The websites.
        /// </value>
        IDbSet<Website> Websites { get; set; }

        /// <summary>
        /// Gets or sets the letters.
        /// </summary>
        /// <value>
        /// The letters.
        /// </value>
        IDbSet<Letter> Letters { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        IDbSet<Tag> Tags { get; set; }

        /// <summary>
        /// Gets or sets the templates.
        /// </summary>
        /// <value>
        /// The templates.
        /// </value>
        IDbSet<Template> Templates { get; set; }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        IDbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        /// <value>
        /// The settings.
        /// </value>
        IDbSet<Setting> Settings { get; set; }

        /// <summary>
        /// Saves all changes made in this context to the underlying data source.
        /// </summary>
        /// <returns>The number of objects written to the underlying data source.</returns>
        int SaveChanges();

        /// <summary>
        /// Gets a <see cref="DbEntityEntry"/> object for the given entity
        /// providing access to information about the entity and the ability to
        /// perform actions on the entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns>An entry for the entity.</returns>
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// Returns a set for access to entities of the given type in the context.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>A set for the given entity type.</returns>
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        void Dispose();
    }
}
