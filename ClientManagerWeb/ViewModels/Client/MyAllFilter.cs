namespace ClientManagerWeb.ViewModels.Client
{
    /// <summary>
    /// My all filter.
    /// </summary>
    public class MyAllFilter
    {
        /// <summary>
        /// Item "My" from MyAll filter
        /// </summary>
        public const string My = "My clients";

        /// <summary>
        /// Item "All" from MyAll filter
        /// </summary>
        public const string All = "All clients";

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public MyAllFilter()
        {
            MyAllFilterCollection = new[] { MyAllFilter.My, MyAllFilter.All };
        }

        /// <summary>
        /// Gets or sets filter "AllMy" collection.
        /// </summary>
        /// <value>
        /// Filter "AllMy" collection.
        /// </value>
        public string[] MyAllFilterCollection { get; set; }
    }
}