using System;
using ClientManagerBase.Helpers;
using ClientManagerBase.Models;
using ClientManagerBase.Repository;

namespace ClientManagerBase.Services
{
    /// <summary>
    /// Service to work with users.
    /// </summary>
    public class UserService
    {
        /// <summary>
        /// Repository to access data source.
        /// </summary>
        private readonly IDataRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="repository">The repository to access data source.</param>
        /// <exception cref="ArgumentNullException"><paramref name="repository"/> is <c>null</c>.</exception>
        public UserService(IDataRepository repository)
        {
            if (repository == null)
                throw new ArgumentNullException("repository", "Repository can't be null.");

            _repository = repository;
        }

        /// <summary>
        /// Gets the user by his name.
        /// </summary>
        /// <param name="name">The user name.</param>
        /// <returns>The user.</returns>
        public User GetUserByName(string name)
        {
            var user = _repository.Single<User>(u => u.Name == name);
            return user;
        }

        /// <summary>
        /// Validates the user.
        /// </summary>
        /// <param name="name">The user name.</param>
        /// <param name="password">The user password.</param>
        /// <returns><c>true</c> if user is exists and password is correct; otherwise - <c>false</c>.</returns>
        public bool ValidateUser(string name, string password)
        {
            var hash = CalcSha1.CalcHash(password);

            var user = _repository.Single<User>(u => u.Name == name && u.PasswordHash == hash);
            return user != null;
        }

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="password">The password.</param>
        /// <param name="email">The email.</param>
        /// <param name="representation">Representation of user for email sending.</param>
        /// <returns><see cref="User"/> if creation succeed; otherwise - <c>null</c>.</returns>
        public User CreateUser(string name, string password, string email, string representation)
        {
            // Check if user is already exists.
            if (GetUserByName(name) != null)
                return null;

            // Add new user to database.
            var user = new User
                       {
                           Name = name,
                           PasswordHash = CalcSha1.CalcHash(password),
                           Email = email,
                           Representation = representation
                       };

            _repository.Add(user);
            _repository.SaveChanges();

            return user;
        }

        /// <summary>
        /// Changes the user data.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="oldPassword">The old password.</param>
        /// <param name="newPassword">The new password.</param>
        /// <param name="email">The email.</param>
        /// <param name="representation">Representation of user for email sending.</param>
        /// <returns><c>true</c> if changed successfully; otherwise - <c>false</c>.</returns>
        public bool EditUserInfo(string name, string oldPassword, string newPassword, string email, string representation)
        {
            var user = GetUserByName(name);

            // Check if user exists.
            if (user == null)
                return false;

            if (!string.IsNullOrEmpty(oldPassword) && !string.IsNullOrEmpty(newPassword))
            {
                // Check if old password is correct.
                if (user.PasswordHash != CalcSha1.CalcHash(oldPassword))
                    return false;

                // Change password.
                user.PasswordHash = CalcSha1.CalcHash(newPassword);
            }

            if (!string.IsNullOrEmpty(email))
                user.Email = email;

            user.Representation = representation;

            _repository.SaveChanges();

            return true;
        }
    }
}
