using ClientManagerBase.Models.Clients;

namespace ClientManagerBase.Models.Contacts
{
    /// <summary>
    /// Represents one website.
    /// </summary>
    public class Website
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Website"/> class.
        /// </summary>
        /// <param name="value">The website.</param>
        public Website(string value = null)
        {
            Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Website"/> class.
        /// </summary>
        public Website()
        {
        }

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
        /// Gets or sets the client.
        /// </summary>
        /// <value>
        /// The client.
        /// </value>
        public Client Client { get; set; }

        #region Foreign keys

        /// <summary>
        /// Gets or sets the client ID.
        /// </summary>
        /// <value>
        /// The client ID.
        /// </value>
        public int ClientId { get; set; }

        #endregion
    }
}
