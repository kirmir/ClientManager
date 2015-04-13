using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClientManagerWeb.ViewModels.Client
{
    /// <summary>
    /// View model for creating and editing client's data.
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
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        [Display(Name = "Title", Description = "Descriptive client name")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the personal details.
        /// </summary>
        /// <value>
        /// The personal details.
        /// </value>
        [Display(Name = "Details", Description = "Detailed information about client")]
        public string Details { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [Display(Name = "First name", Description = "Client's first name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the name of the second.
        /// </summary>
        /// <value>
        /// The name of the second.
        /// </value>
        [Display(Name = "Second name", Description = "Client's second name")]
        public string SecondName { get; set; }

        /// <summary>
        /// Gets or sets the name of the company.
        /// </summary>
        /// <value>
        /// The name of the company.
        /// </value>
        [Display(Name = "Company name", Description = "Name of clients company")]
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [Display(Name = "Description", Description = "Client's description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        [Display(Name = "User", Description = "User that works with client")]
        public string User { get; set; }

        /// <summary>
        /// Gets or sets the type ID.
        /// </summary>
        /// <value>
        /// The type ID.
        /// </value>
        [Display(Name = "Client type", Description = "Current clients type")]
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
        /// Gets or sets the main email.
        /// </summary>
        /// <value>
        /// The main email.
        /// </value>
        [Display(Name = "Main e-mail", Description = "Default box by mail passing")]
        public string MainEmail { get; set; }

        /// <summary>
        /// Gets or sets the additional emails.
        /// </summary>
        /// <value>
        /// The additional emails.
        /// </value>
        [Display(Name = "Full e-mails list", Description = "List of e-mails that are associated with client")]
        public IEnumerable<string> Emails { get; set; }

        /// <summary>
        /// Gets or sets the addresses.
        /// </summary>
        /// <value>
        /// The addresses.
        /// </value>
        [Display(Name = "Addresses", Description = "Client's addresses")]
        public IEnumerable<string> Addresses { get; set; }

        /// <summary>
        /// Gets or sets the phone numbers.
        /// </summary>
        /// <value>
        /// The phone numbers.
        /// </value>
        [Display(Name = "Phone numbers", Description = "Client's phone numbers")]
        public IEnumerable<string> PhoneNumbers { get; set; }

        /// <summary>
        /// Gets or sets the websites.
        /// </summary>
        /// <value>
        /// The websites.
        /// </value>
        [Display(Name = "Web sites", Description = "Client's web sites")]
        public IEnumerable<string> Websites { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        [Display(Name = "Tags", Description = "Associated tags")]        
        public string Tags { get; set; }
    }
}