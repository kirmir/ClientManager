using ClientManagerBase.Models.Clients;

namespace ClientManagerWeb.ViewModels.Tags
{
    /// <summary>
    /// View model for <see cref="Tag"/> for list of all tags.
    /// </summary>
    public class TagsListRecordViewModel
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
        /// Gets or sets the attached templates number.
        /// </summary>
        /// <value>
        /// The attached templates number.
        /// </value>
        public int AttachedTemplates { get; set; }
    }
}