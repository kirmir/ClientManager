using System.ComponentModel.DataAnnotations;
using ClientManagerBase.Models.Letters;

namespace ClientManagerWeb.ViewModels.Templates
{
    /// <summary>
    /// View model for <see cref="Template"/> in Details and Delete actions views.
    /// </summary>
    public class TemplateDetailsViewModel
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
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the letter subject.
        /// </summary>
        /// <value>
        /// The letter subject.
        /// </value>
        [Display(Name = "Letter subject", Description = "Letter subject")]
        public string LetterSubject { get; set; }

        /// <summary>
        /// Gets or sets the content of the letter.
        /// </summary>
        /// <value>
        /// The content of the letter.
        /// </value>
        [Display(Name = "Letter content", Description = "Letter content")]
        public string LetterContent { get; set; }

        /// <summary>
        /// Gets or sets the days count after which a letter with template must be sent.
        /// </summary>
        /// <value>
        /// The days count.
        /// </value>
        public int DaysCount { get; set; }

        /// <summary>
        /// Gets or sets the comma separated tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        public string SelectedTags { get; set; }
    }
}