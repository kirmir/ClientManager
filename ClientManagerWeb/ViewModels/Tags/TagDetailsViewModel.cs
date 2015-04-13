using System.Collections.Generic;

namespace ClientManagerWeb.ViewModels.Tags
{
    /// <summary>
    /// View model for tag details view.
    /// </summary>
    public class TagDetailsViewModel
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
        /// The value.
        /// </value>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the attached templates.
        /// </summary>
        /// <value>
        /// The attached templates.
        /// </value>
        public ICollection<TagTemplateViewModel> AttachedTemplates { get; set; }
    }
}