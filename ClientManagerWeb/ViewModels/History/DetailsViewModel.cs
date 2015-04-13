using System;
using System.ComponentModel.DataAnnotations;

namespace ClientManagerWeb.ViewModels.History
{
    /// <summary>
    /// The details view model.
    /// </summary>
    public class DetailsViewModel
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        /// <value>
        /// The ID.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the letter subject.
        /// </summary>
        /// <value>
        /// The letter subject.
        /// </value>
        [Display(Name = "Letter subject")]
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the content of the letter.
        /// </summary>
        /// <value>
        /// The content of the letter.
        /// </value>
        [Display(Name = "Letter content")]
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets a value indicating when this letter was sent.
        /// </summary>
        /// <value>
        /// <c>null</c> if this letter wasn't sent yet; otherwise, <c>date</c>.
        /// </value>
        [Display(Name = "Actual sent date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]     
        public DateTime? ActualSentDate { get; set; }

        /// <summary>
        /// Gets or sets the send date when the letter should be sent.
        /// </summary>
        /// <value>
        /// The send date.
        /// </value>
        [Display(Name = "Expected sent date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime ExpectedSendDate { get; set; }

        /// <summary>
        /// Gets or sets representation of sender ("Sender name &lt;sender_mail@mail.com&gt;").
        /// </summary>
        /// <value>
        /// Representation of sender (name and email).
        /// </value>
        [Display(Name = "Sender")]
        public string FromNameEmail { get; set; }

        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        /// <value>
        /// The ID.
        /// </value>
        public int ClientId { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        [Display(Name = "Client title")]
        public string ClientTitle { get; set; }
    }
}