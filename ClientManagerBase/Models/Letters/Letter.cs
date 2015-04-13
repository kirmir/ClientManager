using System;
using System.ComponentModel.DataAnnotations;
using ClientManagerBase.Models.Clients;

namespace ClientManagerBase.Models.Letters
{
    /// <summary>
    /// The letter to send.
    /// </summary>
    public class Letter
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        /// <value>
        /// The ID.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the client.
        /// </summary>
        /// <value>
        /// The client.
        /// </value>
        public Client Client { get; set; }

        /// <summary>
        /// Gets or sets the letter subject.
        /// </summary>
        /// <value>
        /// The letter subject.
        /// </value>
        [Required]
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the content of the letter.
        /// </summary>
        /// <value>
        /// The content of the letter.
        /// </value>
        [Required]
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the template.
        /// </summary>
        /// <value>
        /// The template.
        /// </value>
        public Template Template { get; set; }

        /// <summary>
        /// Gets or sets the send date when the letter should be sent.
        /// </summary>
        /// <value>
        /// The send date.
        /// </value>
        public DateTime ExpectedSendDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating when this letter was sent.
        /// </summary>
        /// <value>
        /// <c>null</c> if this letter wasn't sent yet; otherwise, <c>date</c>.
        /// </value>
        public DateTime? ActualSentDate { get; set; }

        /// <summary>
        /// Gets or sets representation of sender ("Sender name &lt;sender_mail@mail.com&gt;").
        /// </summary>
        /// <value>
        /// Representation of sender (name and email).
        /// </value>
        [Required]
        public string FromNameEmail { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this letter is canceled to send.
        /// </summary>
        /// <value>
        /// <c>true</c> if this letter is canceled; otherwise, <c>false</c>.
        /// </value>
        public bool IsCanceled { get; set; }

        #region Foreign Keys

        /// <summary>
        /// Gets or sets the client ID.
        /// </summary>
        /// <value>
        /// The client ID.
        /// </value>
        public int ClientId { get; set; }

        /// <summary>
        /// Gets or sets the template ID.
        /// </summary>
        /// <value>
        /// The template ID.
        /// </value>
        public int? TemplateId { get; set; }

        #endregion
    }
}
