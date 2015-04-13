using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ClientManagerWeb.ViewModels.Account
{
    /// <summary>
    /// View model for editing user information (password, email, etc) form.
    /// </summary>
    public class EditUserInfoViewModel
    {
        /// <summary>
        /// Gets or sets the old password.
        /// </summary>
        /// <value>
        /// The old password.
        /// </value>
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        /// <summary>
        /// Gets or sets the new password.
        /// </summary>
        /// <value>
        /// The new password.
        /// </value>
        [StringLength(50, ErrorMessage = "The password must be at least 6 characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        /// <summary>
        /// Gets or sets the confirm password.
        /// </summary>
        /// <value>
        /// The confirm password.
        /// </value>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the representation for email sending.
        /// </summary>
        /// <value>
        /// The representation.
        /// </value>
        [Display(Name = "Representation for e-mail sending")]
        [StringLength(100, ErrorMessage = "Representation must be 0-100 characters.")]
        public string Representation { get; set; }
    }
}