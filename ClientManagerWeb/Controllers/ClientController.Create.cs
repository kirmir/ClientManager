using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ClientManagerBase.Models;
using ClientManagerBase.Models.Clients;
using ClientManagerBase.Models.Contacts;
using ClientManagerBase.Services;
using ClientManagerWeb.ViewModels.Client;

namespace ClientManagerWeb.Controllers
{
    /// <summary>
    /// The client controller.
    /// </summary>
    public partial class ClientController
    {
        /// <summary>
        /// Creates the client.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <returns>The inserted id.</returns>
        private int CreateClient(CreateUpdateViewModel client)
        {
            // Get current model
            var dest = new Client();

            // Fill simple data
            dest.Title = client.Title;
            dest.CompanyName = client.CompanyName;
            dest.CreationDate = client.CreationDate;
            dest.FirstName = client.FirstName;
            dest.SecondName = client.SecondName;
            dest.Description = client.Description;
            dest.Details = client.Details;
            dest.MainEmail = client.MainEmail;
            dest.Type = _db.All<ClientType>().First(c => c.Id == client.TypeId);
            dest.User = _db.All<User>().First(c => c.Id == client.UserId);

            // Create collection instances
            dest.Emails = new Collection<Email>();
            dest.Addresses = new Collection<Address>();
            dest.PhoneNumbers = new Collection<PhoneNumber>();
            dest.Websites = new Collection<Website>();

            // Create Emails
            if (client.Emails != null)
                foreach (var item in client.Emails)
                {
                    if (!string.IsNullOrWhiteSpace(item.Value))
                        dest.Emails.Add(new Email(item.Value));
                }

            // Create addresses
            if (client.Addresses != null)
                foreach (var item in client.Addresses)
                {
                    if (!string.IsNullOrWhiteSpace(item.Value))
                        dest.Addresses.Add(new Address(item.Value));
                }

            // Create phone numbers
            if (client.PhoneNumbers != null)
                foreach (var item in client.PhoneNumbers)
                {
                    if (!string.IsNullOrWhiteSpace(item.Value))
                        dest.PhoneNumbers.Add(new PhoneNumber(item.Value));
                }

            // Create web sites
            if (client.Websites != null)
                foreach (var item in client.Websites)
                {
                    if (!string.IsNullOrWhiteSpace(item.Value))
                        dest.Websites.Add(new Website(item.Value));
                }

            // Add tags
            var service = new TagService(_db);
            dest.Tags = service.GetTagsFromString(client.TagList) ?? new List<Tag>();

            _db.Add(dest);
            _db.SaveChanges();

            return dest.Id;
        }
    }
}