using ClientManagerBase.Models.Letters;

namespace ClientManagerWeb.ViewModels.Templates
{
    /// <summary>
    /// View model for <see cref="Template"/> for list of all templates.
    /// </summary>
    public class TemplatesListRecordViewModel
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        /// <value>
        /// The ID.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the days count after which a letter with template must be sent.
        /// </summary>
        /// <value>
        /// The days count.
        /// </value>
        public int DaysCount { get; set; }
    }
}