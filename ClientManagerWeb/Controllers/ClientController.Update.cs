using System.Data.Entity;
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
        /// Updates the client.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <returns>Updated client ID.</returns>
        private int UpdateClient(CreateUpdateViewModel client)
        {
            // Get current model
            var dest = _db.All<Client>(c => c.Id == client.Id).Include(c => c.Tags).First();

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

            // Update Emails
            if (client.Emails != null)
                foreach (var item in client.Emails)
                {
                    Email dbEmailRecord = item.Id == 0 ? null : _db.All<Email>().FirstOrDefault(c => c.Id == item.Id);

                    // Value figured as empty when:
                    bool empty = string.IsNullOrWhiteSpace(item.Value);

                    if (dbEmailRecord == null)
                    {
                        // Create new record
                        if (!empty)
                            dest.Emails.Add(new Email(item.Value));
                    }
                    else
                    {
                        // Edit existing
                        if (!empty)
                            dbEmailRecord.Value = item.Value;
                        else
                            _db.Delete(dbEmailRecord);
                    }
                }

            // Update addresses
            if (client.Addresses != null)
                foreach (var item in client.Addresses)
                {
                    Address dbAddressRecord = item.Id == 0 ? null : _db.All<Address>().FirstOrDefault(c => c.Id == item.Id);

                    // Value figured as empty when:
                    bool empty = string.IsNullOrWhiteSpace(item.Value);

                    if (dbAddressRecord == null)
                    {
                        // Create new record
                        if (!empty)
                            dest.Addresses.Add(new Address(item.Value));
                    }
                    else
                    {
                        // Edit existing
                        if (!empty)
                            dbAddressRecord.Value = item.Value;
                        else
                            _db.Delete(dbAddressRecord);
                    }
                }

            // Update phone numbers
            if (client.PhoneNumbers != null)
                foreach (var item in client.PhoneNumbers)
                {
                    PhoneNumber dbPhoneNumberRecord = item.Id == 0 ? null : _db.All<PhoneNumber>().FirstOrDefault(c => c.Id == item.Id);

                    // Value figured as empty when:
                    bool empty = string.IsNullOrWhiteSpace(item.Value);

                    if (dbPhoneNumberRecord == null)
                    {
                        // Create new record
                        if (!empty)
                            dest.PhoneNumbers.Add(new PhoneNumber(item.Value));
                    }
                    else
                    {
                        // Edit existing
                        if (!empty)
                            dbPhoneNumberRecord.Value = item.Value;
                        else
                            _db.Delete(dbPhoneNumberRecord);
                    }
                }

            // Update web sites
            if (client.Websites != null)
                foreach (var item in client.Websites)
                {
                    Website dbWebsiteRecord = item.Id == 0
                                                  ? null
                                                  : _db.All<Website>().FirstOrDefault(c => c.Id == item.Id);

                    // Value figured as empty when:
                    bool empty = string.IsNullOrWhiteSpace(item.Value);

                    if (dbWebsiteRecord == null)
                    {
                        // Create new record
                        if (!empty)
                            dest.Websites.Add(new Website(item.Value));
                    }
                    else
                    {
                        // Edit existing
                        if (!empty)
                            dbWebsiteRecord.Value = item.Value;
                        else
                            _db.Delete(dbWebsiteRecord);
                    }
                }

            // Add tags
            var service = new TagService(_db);
            if (dest.Tags != null)
                dest.Tags.Clear();

            dest.Tags = service.GetTagsFromString(client.TagList);

            _db.SaveChanges();

            return dest.Id;
        }
    }
}