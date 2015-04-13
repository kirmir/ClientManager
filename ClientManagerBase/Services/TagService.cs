using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ClientManagerBase.Models.Clients;
using ClientManagerBase.Repository;

namespace ClientManagerBase.Services
{
    /// <summary>
    /// Service to work with tags.
    /// </summary>
    public class TagService
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
        public TagService(IDataRepository repository)
        {
            if (repository == null)
                throw new ArgumentNullException("repository", "Repository can't be null.");

            _repository = repository;
        }

        /// <summary>
        /// Get tags from string that represents their names separated by comma. Non-existent tags names
        /// will be created and saved to database. Method returns list of all tags which names were specified.
        /// </summary>
        /// <param name="str">Tags names separated by comma ("tag1, tag2, tag3, ...").</param>
        /// <returns>Tags objects from database.</returns>
        /// <exception cref="ArgumentException">One of the tags names has invalid characters.</exception>
        public ICollection<Tag> GetTagsFromString(string str)
        {
            if (string.IsNullOrEmpty(str))
                return null;

            // Get array of tags names from string.
            var names = str.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            
            // Trim every name and check for it correctness.
            var trimmed = new List<string>();
            var regex = new Regex(@"^[a-zA-Z0-9 ]+$");
            foreach (string t in names)
            {
                var name = t.Trim();

                if (string.IsNullOrEmpty(name))
                    continue;

                if (!regex.IsMatch(name))
                    throw new ArgumentException(@"One of the tags names has invalid characters.");

                trimmed.Add(name);
            }

            // Generate Tags.
            var tags = new List<Tag>();
            foreach (var n in trimmed)
            {
                var name = n;

                var tag = _repository.Single<Tag>(t => t.Value == name) ?? new Tag(name);

                if (!tags.Exists(t => t.Value == tag.Value))
                    tags.Add(tag);
            }

            return tags;
        }
    }
}
