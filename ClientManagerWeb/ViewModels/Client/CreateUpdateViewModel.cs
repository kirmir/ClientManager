using System;
using System.ComponentModel.DataAnnotations;
using ClientManagerBase.Models.Contacts;

namespace ClientManagerWeb.ViewModels.Client
{
    /// <summary>
    /// View model for creating and editing client's data.
    /// </summary>
    public class CreateUpdateViewModel
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
        [Required(ErrorMessage = "Title cannot be empty")]
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
        [Required(ErrorMessage = "First name can't be empty")]
        [Display(Name = "First name", Description = "Client's first name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the name of the second.
        /// </summary>
        /// <value>
        /// The name of the second.
        /// </value>
        [Required(ErrorMessage = "Second name can't be empty")]
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
        [Required(ErrorMessage = "How could you done this field empty??? Are you hacker?")]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the type ID.
        /// </summary>
        /// <value>
        /// The type ID.
        /// </value>
        [Display(Name = "Client status", Description = "Current clients status")]
        [Required(ErrorMessage = "How could you done this field empty??? Are you hacker?")]
        public int TypeId { get; set; }

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
        [Required(ErrorMessage = "Choose some value from select. If it's empty type some data in email's list that is in bottom.")]
        public string MainEmail { get; set; }

        /// <summary>
        /// Gets or sets the additional emails.
        /// </summary>
        /// <value>
        /// The additional emails.
        /// </value>
        [Required(ErrorMessage = "Email cannot be empty")]
        [Display(Name = "Full e-mails list", Description = "List of e-mails that are associated with client")]
        public Email[] Emails { get; set; }

        /// <summary>
        /// Gets or sets the addresses.
        /// </summary>
        /// <value>
        /// The addresses.
        /// </value>
        [Display(Name = "Addresses", Description = "Client's addresses")]
        public Address[] Addresses { get; set; }

        /// <summary>
        /// Gets or sets the phone numbers.
        /// </summary>
        /// <value>
        /// The phone numbers.
        /// </value>
        [Display(Name = "Phone numbers", Description = "Client's phone numbers")]
        public PhoneNumber[] PhoneNumbers { get; set; }

        /// <summary>
        /// Gets or sets the websites.
        /// </summary>
        /// <value>
        /// The websites.
        /// </value>
        [Display(Name = "Web sites", Description = "Client's web sites")]
        public Website[] Websites { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        [Display(Name = "Tags", Description = "Associated tags")]
        [RegularExpression(@"^[a-zA-Z0-9, ]+$", ErrorMessage = "Only letters, numbers, commas and spaces are allowed.")]
        public string TagList { get; set; }

        /// <summary>
        /// Gets or sets the index parameter of the partial view.
        /// </summary>
        /// <value>
        /// The index parameter of the partial view.
        /// </value>
        /// <remarks>
        /// I use it because any other ways to keep parameters in the partial view are unstable.
        /// </remarks>
        public int PartialViewParameterIndex { get; set; }
    }
}