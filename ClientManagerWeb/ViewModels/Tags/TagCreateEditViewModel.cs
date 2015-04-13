using System.ComponentModel.DataAnnotations;

namespace ClientManagerWeb.ViewModels.Tags
{
    /// <summary>
    /// View model for creating tag page that supplies tag details and list of templates.
    /// </summary>
    public class TagCreateEditViewModel
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
        [Required(ErrorMessage = "Tag name can not be empty.")]
        [Display(Name = "* Name", Description = "Tag name")]
        [StringLength(30, ErrorMessage = "Tag name must be 1-30 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9 ]+$", ErrorMessage = "Only letters, numbers and spaces are allowed.")]
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the selected templates.
        /// </summary>
        /// <value>
        /// The selected templates.
        /// </value>
        public int[] SelectedTemplates { get; set; }
    }
}