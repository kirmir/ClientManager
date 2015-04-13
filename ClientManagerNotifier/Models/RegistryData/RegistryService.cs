using Microsoft.Win32;

namespace ClientManagerNotifier.Models.RegistryData
{
    /// <summary>
    /// Class for for working with data in registry.
    /// </summary>
    public class RegistryService
    {
        private const string UserNameRegistryValueName = "USERNAME";
        private const string UserPasswordRegistryValueName = "USERPASSWORD";
        private readonly string[] _pathToData = new[] { "Software", "ClientManager" };

        /// <summary>
        /// Saves the user and password.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="passwordHash">The password hash.</param>
        public void SaveUserAndPassword(string username, string passwordHash)
        {
            RegistryKey rk = Registry.CurrentUser;
            if (rk == null) throw new RegistryWorkException("Can't access to registry branch HKEY_CURRENT_USER");

            foreach (var key in _pathToData)
            {
                rk = rk.CreateSubKey(key);
                if (rk == null)
                    throw new RegistryWorkException(string.Format("Can't get registry key \"HKEY_CURRENT_USER\\{0}\" for write access", string.Join("\\", _pathToData)));
            }

            rk.SetValue(UserNameRegistryValueName, username);
            rk.SetValue(UserPasswordRegistryValueName, passwordHash);
        }

        /// <summary>
        /// Gets the user name and password.
        /// </summary>
        /// <exception cref="RegistryWorkException">If the user name or password are not present in the registry.</exception>
        /// <returns>The user name and password.</returns>
        public UserData GetUserNameAndPassword()
        {
            RegistryKey rk = Registry.CurrentUser;

            foreach (var key in _pathToData)
            {
                rk = rk.OpenSubKey(key);
                if (rk == null)
                    throw new RegistryWorkException(string.Format("Can't get registry key \"HKEY_CURRENT_USER\\{0}\" for read access", string.Join("\\", _pathToData)));
            }

            var userData = new UserData
                               {
                                   UserName = (string)rk.GetValue(UserNameRegistryValueName),
                                   UserPasswordHash = (string)rk.GetValue(UserPasswordRegistryValueName)
                               };
            if (userData.UserName == null) 
                throw new RegistryWorkException(string.Format("Can't get {0} value from {1}", 
                    UserNameRegistryValueName, 
                    string.Join("\\", _pathToData)));
            if (userData.UserPasswordHash == null) 
                throw new RegistryWorkException(string.Format("Can't get {0} value from {1}", 
                    UserPasswordRegistryValueName, 
                    string.Join("\\", _pathToData)));

            return userData;
        }
    }
}