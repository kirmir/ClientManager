using System.ServiceModel;

namespace ClientManagerWcf
{
    /// <summary>
    /// Operations which service exposes.
    /// </summary>
    [ServiceContract]
    public interface ILettersCheckService
    {
        /// <summary>
        /// Checks for new letters for current user.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <param name="passwordHash">Password hash.</param>
        /// <returns>
        /// Number of letters to send (0..N) or -1 if user details are invalid.
        /// </returns>
        [OperationContract]
        int CheckForNewLetters(string userName, string passwordHash);
    }
}
