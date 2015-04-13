using System;
using System.ComponentModel.DataAnnotations;

namespace ClientManagerWeb.ViewModels.Letters
{
    /// <summary>
    /// View model for letter details.
    /// </summary>
    public class LetterDetailsViewModel
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        /// <value>
        /// The ID.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the client name.
        /// </summary>
        /// <value>
        /// The client name.
        /// </value>
        public string ToSecondFirstName { get; set; }

        /// <summary>
        /// Gets or sets the client email.
        /// </summary>
        /// <value>
        /// The client email.
        /// </value>
        public string ToEmail { get; set; }

        /// <summary>
        /// Gets or sets the client ID.
        /// </summary>
        /// <value>
        /// The client ID.
        /// </value>
        public int ClientId { get; set; }

        /// <summary>
        /// Gets or sets the letter subject.
        /// </summary>
        /// <value>
        /// The letter subject.
        /// </value>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the content of the letter.
        /// </summary>
        /// <value>
        /// The content of the letter.
        /// </value>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the template name.
        /// </summary>
        /// <value>
        /// The template name.
        /// </value>
        public string TemplateName { get; set; }

        /// <summary>
        /// Gets or sets the template ID.
        /// </summary>
        /// <value>
        /// The template ID.
        /// </value>
        public int TemplateId { get; set; }

        /// <summary>
        /// Gets or sets the send date when the letter should be sent.
        /// </summary>
        /// <value>
        /// The send date.
        /// </value>
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime ExpectedSendDate { get; set; }

        /// <summary>
        /// Gets or sets representation of sender (name).
        /// </summary>
        /// <value>
        /// Representation of sender (name).
        /// </value>
        public string FromName { get; set; }

        /// <summary>
        /// Gets or sets representation of sender (email).
        /// </summary>
        /// <value>
        /// Representation of sender (email).
        /// </value>
        public string FromEmail { get; set; }
    }
}