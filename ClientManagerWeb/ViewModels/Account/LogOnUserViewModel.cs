using System.ComponentModel.DataAnnotations;

namespace ClientManagerWeb.ViewModels.Account
{
    /// <summary>
    /// View model for user login form.
    /// </summary>
    public class LogOnUserViewModel
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        [Required(ErrorMessage = "User name is required.")]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether user should stay logged in.
        /// </summary>
        /// <value>
        /// <c>true</c> if user should stay logged in; otherwise, <c>false</c>.
        /// </value>
        [Display(Name = "Keep me logged in")]
        public bool KeepLoggedIn { get; set; }
    }
}