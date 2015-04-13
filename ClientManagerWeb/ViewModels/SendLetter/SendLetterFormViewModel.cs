using System.ComponentModel.DataAnnotations;

namespace ClientManagerWeb.ViewModels.SendLetter
{
    /// <summary>
    /// The view model for send letter form.
    /// </summary>
    public class SendLetterFormViewModel
    {
        /// <summary>
        /// Gets or sets the client ID.
        /// </summary>
        /// <value>
        /// The client ID.
        /// </value>
        [Display(Name = "Choose client")]
        public int ClientId { get; set; }

        /// <summary>
        /// Gets or sets the client type ID.
        /// </summary>
        /// <value>
        /// The client type ID.
        /// </value>
        [Display(Name = "Choose client type")]
        public int ClientTypeId { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>
        /// The subject.
        /// </value>
        [Display(Name = "Letter subject")]
        [Required(ErrorMessage = "Subject can not be empty")]
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the letter body.
        /// </summary>
        /// <value>
        /// The letter body.
        /// </value>
        [Display(Name = "Letter content")]
        public string LetterBody { get; set; }
    }
}