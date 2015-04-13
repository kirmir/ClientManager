using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClientManagerWeb.ViewModels.Client
{
    /// <summary>
    /// View model for indexes.
    /// </summary>
    public class IndexViewModel
    {
        /// <summary>
        /// Gets or sets the short details.
        /// </summary>
        /// <value>
        /// The short details.
        /// </value>
        public IEnumerable<ShortDetailsViewModel> ShortDetails { get; set; }

        /// <summary>
        /// Gets or sets the user for showing.
        /// </summary>
        /// <value>
        /// The user for showing.
        /// </value>
        public string MyAll { get; set; }

        /// <summary>
        /// Gets or sets the client type ID.
        /// </summary>
        /// <value>
        /// The client type ID.
        /// </value>
        public int ClientTypeId { get; set; }

        /// <summary>
        /// Gets or sets the client tag filter.
        /// </summary>
        /// <value>
        /// The client tag filter.
        /// </value>
        [RegularExpression(@"^[a-zA-Z0-9|& ]+$", ErrorMessage = "Only letters, numbers, |, & and spaces are allowed.")]
        public string ClientTags { get; set; }
    }
}