using System.ComponentModel.DataAnnotations;
using ClientManagerBase.Models.Letters;
using DataAnnotationsExtensions;

namespace ClientManagerWeb.ViewModels.Templates
{
    /// <summary>
    /// View model for creating and editing <see cref="Template"/>.
    /// </summary>
    public class TemplateCreateEditViewModel
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        /// <value>
        /// The ID.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required(ErrorMessage = "Template name can not be empty.")]
        [Display(Name = "* Name", Description = "Template name")]
        [StringLength(100, ErrorMessage = "Template name must be 1-100 characters.")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [Display(Name = "Description", Description = "Template description")]
        [StringLength(300, ErrorMessage = "Template description must be 1-300 characters.")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the letter subject.
        /// </summary>
        /// <value>
        /// The letter subject.
        /// </value>
        [Required(ErrorMessage = "Letter subject can not be empty.")]
        [Display(Name = "* Subject", Description = "Letter subject")]
        [StringLength(300, ErrorMessage = "Letter subject must be 1-300 characters.")]
        public string LetterSubject { get; set; }

        /// <summary>
        /// Gets or sets the content of the letter.
        /// </summary>
        /// <value>
        /// The content of the letter.
        /// </value>
        [Required(ErrorMessage = "Letter content can not be empty.")]
        [Display(Name = "* Content", Description = "Letter content")]
        public string LetterContent { get; set; }

        /// <summary>
        /// Gets or sets the days count after which a letter with template must be sent.
        /// </summary>
        /// <value>
        /// The days count.
        /// </value>
        [Required(ErrorMessage = "Days interval must be specified.")]
        [Display(Name = "* Days interval", Description = "Days interval before sending")]
        [Min(1, ErrorMessage = "Days interval can not be less than 1 day.")]
        public int DaysCount { get; set; }

        /// <summary>
        /// Gets or sets the selected tags.
        /// </summary>
        /// <value>
        /// The selected tags.
        /// </value>
        public int[] SelectedTags { get; set; }

        /// <summary>
        /// Gets or sets the new tags, separated by comma.
        /// </summary>
        /// <value>
        /// The new tags, separated by comma.
        /// </value>
        [RegularExpression(@"[a-zA-Z0-9, ]+", ErrorMessage = "Only letters, numbers and spaces are allowed in tag names.")]
        public string NewTags { get; set; }
    }
}