using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ClientManagerBase.Models;
using ClientManagerBase.Models.Clients;
using ClientManagerBase.Repository;
using ClientManagerWeb.ViewModels.Client;

namespace ClientManagerWeb.ViewModelProviders
{
    /// <summary>
    /// Provides easy to use <see cref="SelectList"/> collections for views.
    /// </summary>
    internal class SelectCollectionsProvider
    {
        /// <summary>
        /// Repository to access data source.
        /// </summary>
        private readonly IDataRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectCollectionsProvider"/> class.
        /// </summary>
        /// <param name="repository">The repository to access data source.</param>
        /// <exception cref="ArgumentNullException"><paramref name="repository"/> is <c>null</c>.</exception>
        internal SelectCollectionsProvider(IDataRepository repository)
        {
            if (repository == null)
                throw new ArgumentNullException("repository", "Repository can't be null.");

            _repository = repository;

            MyAllFilterCollection = new MyAllFilter();
        }

        /// <summary>
        /// Gets or sets MyAll filter collection.
        /// </summary>
        /// <value>
        /// MyAll filter collection.
        /// </value>
        internal MyAllFilter MyAllFilterCollection { get; set; }

        /// <summary>
        /// Gets the collection of users.
        /// </summary>
        /// <returns>The users collection.</returns>
        internal IEnumerable<SelectListItem> GetCollectionOfUsers()
        {
            return new SelectList(_repository.All<User>(), "Id", "Name");
        }

        /// <summary>
        /// Gets the collection of client types.
        /// </summary>
        /// <returns>The client types collection.</returns>
        internal IEnumerable<SelectListItem> GetCollectionOfMyAllFilter()
        {
            return new SelectList(MyAllFilterCollection.MyAllFilterCollection);
        }

        /// <summary>
        /// Gets the collection of client types, and adds some new value to this collection if value != null.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <param name="value">The value.</param>
        /// <param name="defaultSelectedValue">The default selected value.</param>
        /// <returns>
        /// The client types collection.
        /// </returns>
        internal IEnumerable<SelectListItem> GetCollectionOfClientTypes(int id = 0, string value = null, int? defaultSelectedValue = null)
        {
            var res = _repository.All<ClientType>()
                .Select(c => new { c.Id, c.Value }).ToList();

            if (value != null)
                res.Add(new { Id = id, Value = value });

            if (defaultSelectedValue == null)
                return new SelectList(res, "Id", "Value");

            return new SelectList(res, "Id", "Value", defaultSelectedValue.Value);
        }

        /// <summary>
        /// Gets the clients list, and adds some new value to this collection if value != null.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <param name="value">The value.</param>
        /// <param name="defaultSelectedValue">The default selected value.</param>
        /// <returns>
        /// The clients list.
        /// </returns>
        internal IEnumerable<SelectListItem> GetCollectionOfClients(int id = 0, string value = null, int? defaultSelectedValue = null)
        {
            var res = _repository.All<Client>()
                .Select(c => new { c.Id, Value = c.Title }).ToList();

            if (value != null)
                res.Add(new { Id = id, Value = value });

            if (defaultSelectedValue == null)
                return new SelectList(res, "Id", "Value");
            
            return new SelectList(res, "Id", "Value", defaultSelectedValue.Value);
        }

        /// <summary>
        /// Gets the collection of tags without IDs, and adds some new value to this collection if value != null.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="defaultSelectedValue">The default selected value.</param>
        /// <returns>
        /// The tags collection.
        /// </returns>
        internal IEnumerable<SelectListItem> GetCollectionOfTags(string value = null, string defaultSelectedValue = null)
        {
            var res = _repository.All<Tag>()
                .Select(c => c.Value).ToList();

            if (value != null)
                res.Add(value);

            if (defaultSelectedValue == null)
                return new SelectList(res);

            return new SelectList(res, defaultSelectedValue);
        }
    }
}
