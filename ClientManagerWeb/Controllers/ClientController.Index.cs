using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using AutoMapper;
using ClientManagerBase.Helpers;
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
        /// Gets the client short details.
        /// </summary>
        /// <param name="tagFilterString">The tag filter string.</param>
        /// <param name="userId">The user ID.</param>
        /// <param name="typeFilter">The type filter.</param>
        /// <returns>
        /// The client short details.
        /// </returns>
        private IEnumerable<ShortDetailsViewModel> GetClientShortDetails(string tagFilterString, int userId, int typeFilter)
        {
            IQueryable<Client> src;

            if (typeFilter == 0)
                src = _db.All<Client>()
                    .Include(c => c.Type)
                    .Include(c => c.User)
                    .Include(c => c.Tags);
            else
                src = _db.All<Client>()
                    .Include(c => c.Type)
                    .Include(c => c.User)
                    .Include(c => c.Tags)
                    .Where(c => c.TypeId == typeFilter);

            if (userId != 0)
            {
                src = src.Where(c => c.UserId == userId);
            }

            src =
                src.Where(
                    DynamicExpression.ParseLambda<Client, bool>(
                        FilterByTagHelper.ConvertInputStringToSQLWhereStatement(tagFilterString)));

            Mapper.CreateMap<Client, ShortDetailsViewModel>()
                .ForMember(m => m.ClientName, p => p.MapFrom(v => string.Format("{0} {1}", v.FirstName, v.SecondName)))
                .ForMember(m => m.Tags, p => p.MapFrom(v => string.Join(", ", (v.Tags ?? new Collection<Tag>()).Select(t => t.Value))))
                .ForMember(m => m.User, p => p.MapFrom(v => v.User.Name))
                .ForMember(m => m.Type, p => p.MapFrom(v => v.Type.Value));

            var dto = Mapper.Map<IQueryable<Client>, List<ShortDetailsViewModel>>(src);
            return dto;
        }
    }
}