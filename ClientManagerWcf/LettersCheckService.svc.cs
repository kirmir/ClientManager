using System;
using System.Linq;
using ClientManagerBase.Context;
using ClientManagerBase.Repository;
using ClientManagerBase.Services;

namespace ClientManagerWcf
{
    /// <summary>
    /// Implementation of service operations.
    /// </summary>
    public class LettersCheckService : ILettersCheckService, IDisposable
    {
        private readonly IDataRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="LettersCheckService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public LettersCheckService(IDataRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LettersCheckService"/> class.
        /// </summary>
        public LettersCheckService() : this(new DatabaseRepository(new DatabaseContext()))
        {
        }

        /// <summary>
        /// Checks for new letters for current user.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <param name="passwordHash">Password hash.</param>
        /// <returns>
        /// Number of letters to send (0..N) or -1 if user details are invalid.
        /// </returns>
        public int CheckForNewLetters(string userName, string passwordHash)
        {
            // Get the user.
            var userService = new UserService(_repository);
            var user = userService.GetUserByName(userName);

            // Validate user.
            if (user == null || user.PasswordHash != passwordHash)
                return -1;

            // Retrieve number of new unsent letters for user.
            var letterService = new LetterService(_repository);
            letterService.GenerateNewLettersForToday();
            return letterService.GetAllUnsentLetters(user).Count();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing,
        /// releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
