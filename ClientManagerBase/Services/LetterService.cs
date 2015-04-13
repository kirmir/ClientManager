using System;
using System.Data.Entity;
using System.Data.Objects;
using System.Linq;
using ClientManagerBase.Models;
using ClientManagerBase.Models.Clients;
using ClientManagerBase.Models.Letters;
using ClientManagerBase.Repository;

namespace ClientManagerBase.Services
{
    /// <summary>
    /// Provides easy to use methods for creation letters from templates by tags.
    /// </summary>
    public class LetterService
    {
        /// <summary>
        /// Repository to access data source.
        /// </summary>
        private readonly IDataRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="repository">The repository to access data source.</param>
        /// <exception cref="ArgumentNullException"><paramref name="repository"/> is <c>null</c>.</exception>
        public LetterService(IDataRepository repository)
        {
            if (repository == null)
                throw new ArgumentNullException("repository", "Repository can't be null.");

            _repository = repository;
        }

        /// <summary>
        /// Delegate for function that returns date with days added to it. Need for using service
        /// with different data sources.
        /// </summary>
        /// <param name="dateValue">The date value.</param>
        /// <param name="addValue">The add value (days).</param>
        /// <returns>New date.</returns>
        public delegate DateTime? AddDaysMethod(DateTime? dateValue, int? addValue);

        /// <summary>
        /// Creates letter from template.
        /// </summary>
        /// <param name="template">The template.</param>
        /// <param name="client">The client.</param>
        /// <param name="from">The user who will send letter.</param>
        /// <returns>Letter ready to send.</returns>
        public static Letter CreateFromTemplate(Template template, Client client, User from)
        {
            // Prepare letter content.
            var content = template.LetterContent.Replace("{first-name}", client.FirstName);
            content = content.Replace("{second-name}", client.SecondName);

            // Create letter.
            var letter = new Letter
                         {
                             Client = client,
                             Subject = template.LetterSubject,
                             Content = content,
                             Template = template,
                             ExpectedSendDate = client.CreationDate.AddDays(template.DaysCount),
                             FromNameEmail = string.Format("{0} <{1}>", from.Representation, from.Email)
                         };

            return letter;
        }

        /// <summary>
        /// Creates letters for all clients using templates attached to their tags.
        /// Only letters with expected send date greater than today will be created.
        /// </summary>
        /// <param name="lastGenerate">The last generating date.</param>
        public void GenerateNewLetters(DateTime lastGenerate)
        {
            var history = _repository.All<Letter>();
            var clients = _repository.All<Client>();

            var templateClient = from client in clients
                                 from t in

                                     // Select all templates by user tags.
                                     (from tag in client.Tags
                                      from template in tag.Templates
                                      select template).Distinct()

                                     // Exclude templates that was already used in letters.
                                     .Except(from letter in history
                                             where letter.ClientId == client.Id
                                             select letter.Template)
                                 let date = EntityFunctions.AddDays(client.CreationDate, t.DaysCount)
                                 where date >= lastGenerate && date <= DateTime.Now
                                 select new { Template = t, Client = client, client.User };

            var letters = templateClient.ToList().Select(tc => CreateFromTemplate(tc.Template, tc.Client, tc.User));

            _repository.Add(letters);
            _repository.SaveChanges();
        }

        /// <summary>
        /// Generates the new letters for today.
        /// </summary>
        public void GenerateNewLettersForToday()
        {
            GenerateNewLetters(SettingsProvider.GetLastGenerateDate(_repository));
            SettingsProvider.SetLastGenerateDate(_repository, DateTime.Now);
        }

        /// <summary>
        /// Gets all unsent letters for current user.
        /// </summary>
        /// <param name="user">The user - owner of the clients.</param>
        /// <returns>Unsent letters.</returns>
        public IQueryable<Letter> GetAllUnsentLetters(User user)
        {
            var letters = _repository.All<Letter>(l => !l.IsCanceled && l.ActualSentDate == null)
                                     .Include(l => l.Client)
                                     .Where(l => l.Client.UserId == user.Id);

            return letters;
        }
    }
}
