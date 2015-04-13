using System.ComponentModel.DataAnnotations;

namespace ClientManagerWeb.ViewModels.Letters
{
    /// <summary>
    /// View model for letter editing.
    /// </summary>
    public class LetterEditViewModel
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        /// <value>
        /// The ID.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the client name and email.
        /// </summary>
        /// <value>
        /// The client name and email.
        /// </value>
        [Display(Name = "To")]
        public string To { get; set; }

        /// <summary>
        /// Gets or sets the letter subject.
        /// </summary>
        /// <value>
        /// The letter subject.
        /// </value>
        [Required(ErrorMessage = "Letter subject can not be empty.")]
        [Display(Name = "* Subject", Description = "Letter subject")]
        [StringLength(300, ErrorMessage = "Letter subject must be 1-300 characters.")]
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the content of the letter.
        /// </summary>
        /// <value>
        /// The content of the letter.
        /// </value>
        [Required(ErrorMessage = "Letter content can not be empty.")]
        [Display(Name = "* Content", Description = "Letter content")]
        public string Content { get; set; }
    }
}