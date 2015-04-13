using System;
using System.ComponentModel.DataAnnotations;

namespace ClientManagerWeb.ViewModels.Letters
{
    /// <summary>
    /// View model for displaying letter in the list.
    /// </summary>
    public class LettersListRecordViewModel
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        /// <value>
        /// The ID.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the first name and second name of client that will receive email.
        /// </summary>
        /// <value>
        /// The client first name and second name.
        /// </value>
        public string SendTo { get; set; }

        /// <summary>
        /// Gets or sets the letter subject.
        /// </summary>
        /// <value>
        /// The letter subject.
        /// </value>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the send date when the letter should be sent.
        /// </summary>
        /// <value>
        /// The send date.
        /// </value>
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime ExpectedSendDate { get; set; }
    }
}