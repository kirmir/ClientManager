using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ClientManagerBase.Repository
{
    /// <summary>
    /// Interface for data repository.
    /// </summary>
    public interface IDataRepository : IDisposable
    {
        /// <summary>
        /// Gets all objects with the specified type.
        /// </summary>
        /// <typeparam name="T">Type of the objects.</typeparam>
        /// <returns>The objects.</returns>
        IQueryable<T> All<T>() where T : class;

        /// <summary>
        /// Gets all objects with the specified type.
        /// </summary>
        /// <typeparam name="T">Type of the objects.</typeparam>
        /// <param name="expression">The expression that determines the objects.</param>
        /// <returns>The objects.</returns>
        IQueryable<T> All<T>(Expression<Func<T, bool>> expression) where T : class;

        /// <summary>
        /// Retrieve the single object from data source.
        /// </summary>
        /// <typeparam name="T">Type of the object.</typeparam>
        /// <param name="expression">The expression that determines the object.</param>
        /// <returns>The single object.</returns>
        T Single<T>(Expression<Func<T, bool>> expression) where T : class;

        /// <summary>
        /// Adds the specified object to data source.
        /// </summary>
        /// <typeparam name="T">Type of the object.</typeparam>
        /// <param name="item">The object to add.</param>
        void Add<T>(T item) where T : class;

        /// <summary>
        /// Adds the specified objects to data source.
        /// </summary>
        /// <typeparam name="T">The type of the objects.</typeparam>
        /// <param name="items">The objects to add.</param>
        void Add<T>(IEnumerable<T> items) where T : class;

        /// <summary>
        /// Deletes the specified objects from data source.
        /// </summary>
        /// <typeparam name="T">Type of the objects.</typeparam>
        /// <param name="expression">The expression that determines the objects.</param>
        void Delete<T>(Expression<Func<T, bool>> expression) where T : class;

        /// <summary>
        /// Deletes the specified object from data source.
        /// </summary>
        /// <typeparam name="T">Type of the object.</typeparam>
        /// <param name="item">The object to delete.</param>
        void Delete<T>(T item) where T : class;

        /// <summary>
        /// Deletes all objects from the data source.
        /// </summary>
        /// <typeparam name="T">Type of the objects.</typeparam>
        void DeleteAll<T>() where T : class;

        /// <summary>
        /// Attaches the specified object to data source.
        /// </summary>
        /// <typeparam name="T">Type of the object.</typeparam>
        /// <param name="item">The object to attach.</param>
        void Attach<T>(T item) where T : class;

        /// <summary>
        /// Saves the changes to data source.
        /// </summary>
        void SaveChanges();
    }
}