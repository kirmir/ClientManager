using System;
using ClientManagerNotifier.Helpers;
using ClientManagerNotifier.Models.RegistryData;
using Microsoft.Practices.Prism.Commands;

namespace ClientManagerNotifier.ViewModels
{
    /// <summary>
    /// The user's data.
    /// </summary>
    public class UserDataViewModel
    {
        private const int PasswordMinLength = 6;
        private string _userName;
        private string _userPass;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public UserDataViewModel()
        {
            try
            {
                var registry = new RegistryService();
                var data = registry.GetUserNameAndPassword();
                UserName = data.UserName;
                UserPass = string.Empty;
                OldUserPass = data.UserPasswordHash;
            }
            catch
            {
                UserName = string.Empty;
                UserPass = string.Empty;
                OldUserPass = string.Empty;                
            }

            SaveData = new DelegateCommand(SaveDataHandler, SaveDataCanExecuted);
        }

        /// <summary>
        /// Save data command.
        /// </summary>
        public DelegateCommand SaveData { get; private set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName
        {
            get
            {
                return _userName;
            }

            set
            {
                _userName = value;
                if (SaveData != null) SaveData.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Gets or sets the user password.
        /// </summary>
        /// <value>
        /// The user password.
        /// </value>
        public string UserPass
        {
            get
            {
                return _userPass;
            }

            set
            {
                _userPass = value;
                if (SaveData != null) SaveData.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// The close window action.
        /// </summary>
        public Action CloseWindowEvent { get; set; }

        /// <summary>
        /// Gets the password tool tip.
        /// </summary>
        public string PasswordToolTip
        {
            get
            {
                return
                    string.Format(
                        "Password should have {0} characters or more. \r\n" +
                        "If your password already saved, and you don't want to change it leave this field empty.",
                        PasswordMinLength);
            }
        }

        /// <summary>
        /// Gets or sets the old user pass.
        /// </summary>
        /// <value>
        /// The old user pass.
        /// </value>
        public string OldUserPass { get; set; }

        /// <summary>
        /// Saves the data can executed.
        /// </summary>
        /// <returns>Command execute access.</returns>
        private bool SaveDataCanExecuted()
        {
            if (string.IsNullOrEmpty(UserName)) return false;
            if (UserPass == null) return false;
            if (OldUserPass == null) return false;

            if(OldUserPass == string.Empty)
                if (UserPass.Length < PasswordMinLength)
                    return false;
                else
                    return true;

            if ((UserPass == string.Empty) || (UserPass.Length >= PasswordMinLength))
                return true;
            return false;
        }

        /// <summary>
        /// Saves the data handler.
        /// </summary>
        private void SaveDataHandler()
        {
            var registry = new RegistryService();
            registry.SaveUserAndPassword(UserName,
                                         UserPass == string.Empty ? OldUserPass : HashHelper.CalcHash(UserPass));
            if (CloseWindowEvent != null) CloseWindowEvent(); 
        }
    }
}