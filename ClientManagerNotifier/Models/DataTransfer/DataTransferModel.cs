using System;
using ClientManagerNotifier.ClientManagerServiceReference;
using ClientManagerNotifier.Models.RegistryData;

namespace ClientManagerNotifier.Models.DataTransfer
{
    /// <summary>
    /// The data transfer model.
    /// </summary>
    public class DataTransferModel
    {
        /// <summary>
        /// Gets the number of unread messages.
        /// </summary>
        /// <returns>The number of unread messages.</returns>
        public int GetNumberOfUnreadMessages()
        {
            var registry = new RegistryService();
            UserData data;

            try
            {
                data = registry.GetUserNameAndPassword();
            }
            catch(RegistryWorkException ex)
            {
                throw new Exception("Can't logon to service. Check for the login and password correctness.", ex);
            }

            var client = new LettersCheckServiceClient();

            int count;
            try
            {
                count = client.CheckForNewLetters(data.UserName, data.UserPasswordHash);
            }
            catch (Exception ex)
            {
                throw new DataTransferConnectionProblemException("WCF service is inaccessible! \r\nMaybe it's your internet connection problem, or WCF service is shut down!", ex);
            }

            if (count == -1) throw new DataTransferFailToLogOnException("Can't logon to service. Check login and password.", null);
            
            client.Close();
            return count;
        }
    }
}
