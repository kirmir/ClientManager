using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using ClientManagerBase.Models;
using ClientManagerBase.Models.Clients;
using ClientManagerBase.Models.Letters;
using ClientManagerBase.Repository;

namespace ClientManagerBase.Services
{
    /// <summary>
    /// Provides easy to use methods for sending emails.
    /// </summary>
    public class EmailSenderService
    {
        /// <summary>
        /// Repository to access data source.
        /// </summary>
        private readonly IDataRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailSenderService"/> class.
        /// </summary>
        /// <param name="repository">The repository to access data source.</param>
        /// <param name="smtpHost">The SMTP host.</param>
        /// <exception cref="ArgumentNullException"><paramref name="repository"/> is <c>null</c>.</exception>
        public EmailSenderService(IDataRepository repository, string smtpHost)
        {
            if (repository == null)
                throw new ArgumentNullException("repository", "Repository can't be null.");

            if (smtpHost == null)
                throw new ArgumentNullException("smtpHost", "SmtpHost can't be null.");

            _repository = repository;

            SmtpHost = smtpHost;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailSenderService"/> class.
        /// </summary>
        /// <param name="repository">The repository to access data source.</param>
        /// <exception cref="ArgumentNullException"><paramref name="repository"/> is <c>null</c>.</exception>
        protected EmailSenderService(IDataRepository repository) : this(repository, string.Empty)
        {
        }

        /// <summary>
        /// Gets or sets the SMTP host.
        /// </summary>
        /// <value>
        /// The SMTP host.
        /// </value>
        public string SmtpHost { get; set; }

        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="toClient">To client.</param>
        /// <param name="fromEmail">From email.</param>
        /// <param name="fromName">From name.</param>
        /// <param name="subject">The email subject.</param>
        /// <param name="body">The email body.</param>
        /// <returns>The sending result: <c>true</c>, if sent successfully; otherwise - <c>false</c>.</returns>
        public bool SendEmail(Client toClient, string fromEmail, string fromName, string subject, string body)
        {
            // Specify email addresses.
            var from = new MailAddress(fromEmail, fromName, Encoding.UTF8);
            var to = new MailAddress(toClient.MainEmail);

            // Specify the message content.
            var message = new MailMessage(from, to)
                              {
                                  Body = body,
                                  BodyEncoding = Encoding.UTF8,
                                  Subject = subject,
                                  SubjectEncoding = Encoding.UTF8
                              };

            // Send email.
            var client = new SmtpClient(SmtpHost);

            try
            {
                client.Send(message);
            }
            catch (Exception)
            {
                return false;
            }

            _repository.Add(CreateForClient(toClient, subject, body, from.ToString()));

            return true;
        }

        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="letter">The letter.</param>
        /// <param name="user">The user (sender).</param>
        /// <returns>The sending result: <c>true</c>, if sent successfully; otherwise - <c>false</c>.</returns>
        public bool SendLetter(Letter letter, User user)
        {
            // Specify email addresses.
            var from = new MailAddress(user.Email, user.Representation, Encoding.UTF8);
            var to = new MailAddress(letter.Client.MainEmail);

            // Specify the message content.
            var message = new MailMessage(from, to)
            {
                Body = letter.Content,
                BodyEncoding = Encoding.UTF8,
                Subject = letter.Subject,
                SubjectEncoding = Encoding.UTF8
            };

            // Send email.
            var client = new SmtpClient(SmtpHost);

            try
            {
                client.Send(message);
            }
            catch (Exception)
            {
                return false;
            }

            letter.ActualSentDate = DateTime.Now.Date;
            letter.FromNameEmail = string.Format("{0} <{1}>", user.Representation, user.Email);

            return true;
        }

        /// <summary>
        /// Sends the letters to clients.
        /// </summary>
        /// <param name="clients">The clients.</param>
        /// <param name="userName">Name of the user who sends letter.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="letterBody">The letter body.</param>
        /// <returns>The sent letters.</returns>
        public IEnumerable<SendLetterResult> SendLetters(IEnumerable<Client> clients, string userName, string subject, string letterBody)
        {
            var user = _repository.Single<User>(c => c.Name == userName);
            string fromEmail = user.Email;
            string fromName = user.Name;

            return clients.Select(c => new SendLetterResult
                                       {
                                           Email = string.Format("{0} <{1}>", c.Title, c.MainEmail),
                                           WasSent = SendEmail(c, fromEmail, fromName, subject, letterBody)
                                       });
        }

        /// <summary>
        /// Creates custom letter for client without using templates.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="content">The content.</param>
        /// <param name="from">The sender.</param>
        /// <returns>Letter ready to send.</returns>
        private static Letter CreateForClient(Client client, string subject, string content, string from)
        {
            var letter = new Letter
            {
                Client = client,
                Subject = subject,
                Content = content,
                ExpectedSendDate = DateTime.Now,
                FromNameEmail = from,
                ActualSentDate = DateTime.Now
            };

            return letter;
        }

        /// <summary>
        /// The result of letter sending.
        /// </summary>
        public class SendLetterResult
        {
            /// <summary>
            /// Gets or sets the email.
            /// </summary>
            /// <value>
            /// The email.
            /// </value>
            public string Email { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether letter was sent.
            /// </summary>
            /// <value>
            /// <c>true</c> if letter was sent; otherwise, <c>false</c>.
            /// </value>
            public bool WasSent { get; set; }
        }
    }
}
