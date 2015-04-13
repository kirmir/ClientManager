using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ClientManagerBase.Models.Clients;

namespace ClientManagerBase.Models.Letters
{
    /// <summary>
    /// The template.
    /// </summary>
    public class Template
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
        [Required]
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
        [Required]
        public string LetterSubject { get; set; }

        /// <summary>
        /// Gets or sets the content of the letter.
        /// </summary>
        /// <value>
        /// The content of the letter.
        /// </value>
        [Required]
        public string LetterContent { get; set; }

        /// <summary>
        /// Gets or sets the days count after which a letter with template must be sent.
        /// </summary>
        /// <value>
        /// The days count.
        /// </value>
        public int DaysCount { get; set; }

        /// <summary>
        /// Gets or sets the tags to which template is attached.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        public ICollection<Tag> Tags { get; set; }
    }
}
