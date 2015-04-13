using System.ComponentModel.DataAnnotations;
using ClientManagerBase.Models.Clients;

namespace ClientManagerWeb.ViewModels.ClientTypes
{
    /// <summary>
    /// View model for <see cref="ClientType"/>.
    /// </summary>
    public class ClientTypeViewModel
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        /// <value>
        /// The ID.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The type value.
        /// </value>
        [Required(ErrorMessage = "Client type name can not be empty.")]
        [Display(Name = "* Name", Description = "Client type name")]
        [StringLength(30, ErrorMessage = "Client type name must be 1-30 characters.")]
        public string Value { get; set; }
    }
}