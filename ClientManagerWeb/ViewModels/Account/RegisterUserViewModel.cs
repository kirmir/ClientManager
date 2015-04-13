using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ClientManagerWeb.ViewModels.Account
{
    /// <summary>
    /// View model for user register form.
    /// </summary>
    public class RegisterUserViewModel
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        [Required]
        [Display(Name = "* User name")]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "* Email address")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [Required]
        [StringLength(50, ErrorMessage = "The password must be at least 6 characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "* Password")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the confirm password.
        /// </summary>
        /// <value>
        /// The confirm password.
        /// </value>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

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