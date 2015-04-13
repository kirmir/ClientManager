using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ClientManagerBase.Context;

namespace ClientManagerBase.Repository
{
    /// <summary>
    /// A database repository for working with data.
    /// </summary>
    public class DatabaseRepository : IDataRepository
    {
        /// <summary>
        /// Database context.
        /// </summary>
        private readonly IDatabaseContext _context;

        /// <summary>
        /// Determines should context be disposed with repository dispose or not.
        /// </summary>
        private readonly bool _disposeContext;

        /// <summary>
        /// Shows whether repository is already disposed or not.
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseRepository"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="disposeContext">If set to <c>true</c> context will be disposed with repository.</param>
        /// <exception cref="ArgumentNullException"><paramref name="context"/> is <c>null</c>.</exception>
        public DatabaseRepository(IDatabaseContext context, bool disposeContext = true)
        {
            if (context == null)
                throw new ArgumentNullException("context", "Context can't be null.");

            _context = context;
            _disposeContext = disposeContext;
        }

        /// <summary>
        /// Gets all objects with the specified type.
        /// </summary>
        /// <typeparam name="T">Type of the objects.</typeparam>
        /// <returns>The objects.</returns>
        public IQueryable<T> All<T>() where T : class
        {
            return _context.Set<T>().AsQueryable();
        }

        /// <summary>
        /// Gets all objects with the specified type.
        /// </summary>
        /// <typeparam name="T">Type of the objects.</typeparam>
        /// <param name="expression">The expression that determines the objects.</param>
        /// <returns>The objects.</returns>
        public IQueryable<T> All<T>(Expression<Func<T, bool>> expression) where T : class
        {
            return _context.Set<T>().Where(expression);
        }

        /// <summary>
        /// Retrieve the single object from database.
        /// </summary>
        /// <typeparam name="T">Type of the object.</typeparam>
        /// <param name="expression">The expression that determines the object.</param>
        /// <returns>The single object.</returns>
        public T Single<T>(Expression<Func<T, bool>> expression) where T : class
        {
            return All<T>().FirstOrDefault(expression);
        }

        /// <summary>
        /// Adds the specified object to database.
        /// </summary>
        /// <typeparam name="T">Type of the object.</typeparam>
        /// <param name="item">The object to add.</param>
        public void Add<T>(T item) where T : class
        {
            _context.Set<T>().Add(item);
        }

        /// <summary>
        /// Adds the specified objects to database.
        /// </summary>
        /// <typeparam name="T">The type of the objects.</typeparam>
        /// <param name="items">The objects to add.</param>
        public void Add<T>(IEnumerable<T> items) where T : class
        {
            foreach (var item in items)
            {
                Add(item);
            }
        }

        /// <summary>
        /// Deletes the specified objects from database.
        /// </summary>
        /// <typeparam name="T">Type of the objects.</typeparam>
        /// <param name="expression">The expression that determines the objects.</param>
        public void Delete<T>(Expression<Func<T, bool>> expression) where T : class
        {
            foreach (var item in All(expression))
            {
                Delete(item);
            }
        }

        /// <summary>
        /// Deletes the specified object from database.
        /// </summary>
        /// <typeparam name="T">Type of the object.</typeparam>
        /// <param name="item">The object to delete.</param>
        public void Delete<T>(T item) where T : class
        {
            _context.Set<T>().Remove(item);
        }

        /// <summary>
        /// Deletes all objects from the database.
        /// </summary>
        /// <typeparam name="T">Type of the objects.</typeparam>
        public void DeleteAll<T>() where T : class
        {
            var query = All<T>();
            foreach (var item in query)
            {
                Delete(item);
            }
        }

        /// <summary>
        /// Attaches the specified object to database.
        /// </summary>
        /// <typeparam name="T">Type of the object.</typeparam>
        /// <param name="item">The object to attach.</param>
        public void Attach<T>(T item) where T : class
        {
            _context.Set<T>().Attach(item);
        }

        /// <summary>
        /// Saves the changes to database.
        /// </summary>
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources;
        /// <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (_disposeContext && disposing)
                    _context.Dispose();
            }

            _disposed = true;
        }
    }
}
