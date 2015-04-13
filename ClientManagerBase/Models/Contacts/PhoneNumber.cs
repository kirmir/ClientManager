using ClientManagerBase.Models.Clients;

namespace ClientManagerBase.Models.Contacts
{
    /// <summary>
    /// Represents one contact phone number.
    /// </summary>
    public class PhoneNumber
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneNumber"/> class.
        /// </summary>
        /// <param name="value">The phone number.</param>
        public PhoneNumber(string value = null)
        {
            Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneNumber"/> class.
        /// </summary>
        public PhoneNumber()
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
