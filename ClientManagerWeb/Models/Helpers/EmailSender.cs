using ClientManagerBase.Repository;
using ClientManagerBase.Services;

namespace ClientManagerWeb.Models.Helpers
{
    /// <summary>
    /// Provides email sanding service.
    /// </summary>
    public class EmailSender : EmailSenderService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public EmailSender(IDataRepository repository) : base(repository)
        {
            SmtpHost = SettingsProvider.GetSmtpHost(repository);
        }
    }
}