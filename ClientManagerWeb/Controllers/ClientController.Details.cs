using System.Data.Entity;
using System.Linq;
using AutoMapper;
using ClientManagerBase.Models.Clients;
using ClientManagerWeb.ViewModels.Client;

namespace ClientManagerWeb.Controllers
{
    /// <summary>
    /// The client controller.
    /// </summary>
    public partial class ClientController
    {
        /// <summary>
        /// Get the details view model from client.
        /// </summary>
        /// <param name="clientId">The client ID.</param>
        /// <returns>The instance of DetailsViewModel.</returns>
        private DetailsViewModel GetDetailsFromClient(int clientId)
        {
            var client = _db.All<Client>()
                .Include(c => c.User)
                .Include(c => c.Type)
                .Include(c => c.Emails)
                .Include(c => c.Addresses)
                .Include(c => c.Websites)
                .Include(c => c.PhoneNumbers)
                .Include(c => c.Tags)
                .FirstOrDefault(c => c.Id == clientId);

            if (client == null) return null;
            
            Mapper.CreateMap<Client, DetailsViewModel>();
            var dto = Mapper.Map<Client, DetailsViewModel>(client);

            dto.User = client.User.Name;
            dto.Type = client.Type.Value;
            dto.Emails = client.Emails.Select(c => c.Value);
            dto.PhoneNumbers = client.PhoneNumbers.Select(c => c.Value);
            dto.Addresses = client.Addresses.Select(c => c.Value);
            dto.Websites = client.Websites.Select(c => c.Value);
            dto.Tags = string.Join(", ", client.Tags.Select(c => c.Value));

            return dto;
        }
    }
}