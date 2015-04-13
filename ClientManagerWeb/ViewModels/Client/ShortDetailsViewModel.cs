using System;
using System.ComponentModel.DataAnnotations;

namespace ClientManagerWeb.ViewModels.Client
{
    /// <summary>
    /// Represents short details by client.
    /// </summary>
    public class ShortDetailsViewModel
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        /// <value>
        /// The ID.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        [Display(Name = "Title", Description = "Descriptive client name")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [Display(Name = "Client name", Description = "Client's name")]
        public string ClientName { get; set; }

        /// <summary>
        /// Gets or sets the name of the company.
        /// </summary>
        /// <value>
        /// The name of the company.
        /// </value>
        [Display(Name = "Company name", Description = "Name of clients company")]
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the main email.
        /// </summary>
        /// <value>
        /// The main email.
        /// </value>
        [Display(Name = "Main e-mail", Description = "Default box by mail passing")]
        public string MainEmail { get; set; }

        /// <summary>
        /// Gets or sets the type ID.
        /// </summary>
        /// <value>
        /// The type ID.
        /// </value>
        [Display(Name = "Client status", Description = "Current clients status")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the date of client record creation.
        /// </summary>
        /// <value>
        /// The date of client record creation.
        /// </value>
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [Display(Name = "Creation date", Description = "Date of client creation")]
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        [Display(Name = "Tags", Description = "Associated tags")]
        public string Tags { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        [Display(Name = "User", Description = "User that works with client")]
        public string User { get; set; }
    }
}