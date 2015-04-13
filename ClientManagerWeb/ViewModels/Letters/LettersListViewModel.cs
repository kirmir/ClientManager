namespace ClientManagerWeb.ViewModels.Letters
{
    /// <summary>
    /// View model for list of letters ready to send.
    /// </summary>
    public class LettersListViewModel
    {
        /// <summary>
        /// Gets or sets the letters.
        /// </summary>
        /// <value>
        /// The letters.
        /// </value>
        public LettersListRecordViewModel[] Letters { get; set; }

        /// <summary>
        /// Gets or sets the selected letters IDs.
        /// </summary>
        /// <value>
        /// The selected letters IDs.
        /// </value>
        public int[] SelectedLetters { get; set; }
    }
}