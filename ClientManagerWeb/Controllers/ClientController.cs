using System;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClientManagerBase.Context;
using ClientManagerBase.Models;
using ClientManagerBase.Models.Clients;
using ClientManagerBase.Models.Contacts;
using ClientManagerBase.Repository;
using ClientManagerWeb.ViewModelProviders;
using ClientManagerWeb.ViewModels.Client;
using EmitMapper;

namespace ClientManagerWeb.Controllers
{
    /// <summary>
    /// The client controller.
    /// </summary>
    public partial class ClientController : Controller
    {
        private const string SelectItemAll = "ALL";
        private const string CookieMyAllFilter = "MyAllSelecton";
        private const string CookieClientTypeFilter = "ClientTypeSelecton";
        private const string CookieClientTagsFilter = "ClientTagsFilter";
        private readonly IDataRepository _db;
        private readonly SelectCollectionsProvider _collection;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientController"/> class.
        /// </summary>
        /// <param name="repository">The database framework.</param>
        public ClientController(IDataRepository repository)
        {
            _db = repository;
            _collection = new SelectCollectionsProvider(repository);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientController"/> class.
        /// </summary>
        public ClientController()
            : this(new DatabaseRepository(new DatabaseContext()))
        {
        }

        /// <summary>
        /// View all clients.
        /// </summary>
        /// <returns>The view.</returns>
        [Authorize]
        public ViewResult Index()
        {
            var dto = new IndexViewModel();

            // Fill filters data
            dto.MyAll = GetCookie(CookieMyAllFilter, MyAllFilter.My);
            dto.ClientTags = GetCookie(CookieClientTagsFilter, string.Empty);
            try { dto.ClientTypeId = Convert.ToInt32(GetCookie(CookieClientTypeFilter, "0")); }
            catch
            {
                dto.ClientTypeId = 0;
            }

            // Get details
            int userId = dto.MyAll == MyAllFilter.All ? 0 : _db.Single<User>(c => c.Name == User.Identity.Name).Id;
            dto.ShortDetails = GetClientShortDetails(dto.ClientTags, userId, dto.ClientTypeId);

            // Additional view data
            ViewBag.ClientTags = _collection.GetCollectionOfTags(string.Empty, string.Empty);
            ViewBag.ClientTypes = _collection.GetCollectionOfClientTypes(0, SelectItemAll, 0);
            ViewBag.Users = _collection.GetCollectionOfMyAllFilter();

            return View(dto);
        }

        /// <summary>
        /// Indexes the specified filters.
        /// </summary>
        /// <param name="filters">The filters.</param>
        /// <returns>The view.</returns>
        [HttpPost]
        [Authorize]
        public ViewResult Index(IndexViewModel filters)
        {
            // Save cookie
            Response.Cookies.Add(new HttpCookie(CookieMyAllFilter, filters.MyAll));
            Response.Cookies.Add(new HttpCookie(CookieClientTypeFilter, filters.ClientTypeId.ToString()));
            Response.Cookies.Add(new HttpCookie(CookieClientTagsFilter, filters.ClientTags));

            // Fill filters data
            var dto = new IndexViewModel();
            dto.ClientTags = filters.ClientTags;
            dto.ClientTypeId = filters.ClientTypeId;
            dto.MyAll = filters.MyAll;

            // Get details
            int userId = dto.MyAll == MyAllFilter.All ? 0 : _db.Single<User>(c => c.Name == User.Identity.Name).Id;
            dto.ShortDetails = GetClientShortDetails(dto.ClientTags, userId, dto.ClientTypeId);

            // Additional view data
            ViewBag.ClientTags = _collection.GetCollectionOfTags(string.Empty, string.Empty);
            ViewBag.ClientTypes = _collection.GetCollectionOfClientTypes(0, SelectItemAll, 0);
            ViewBag.Users = _collection.GetCollectionOfMyAllFilter();
            return View(dto);
        }

        /// <summary>
        /// Details for specified client.
        /// </summary>
        /// <param name="id">The client ID.</param>
        /// <returns>The view</returns>
        [Authorize]
        public ActionResult Details(int id)
        {
            var res = GetDetailsFromClient(id);
            if (res == null) return RedirectToAction("Index");
            return View(res);
        }

        /// <summary>
        /// Create client record view.
        /// </summary>
        /// <returns>The view.</returns>
        [Authorize]
        public ActionResult Create()
        {
            var dto = new CreateUpdateViewModel();
            dto.CreationDate = DateTime.Now;

            // Without it client validation fails
            dto.Emails = new[] { new Email { Id = 0, Value = string.Empty } };
            dto.Addresses = new[] { new Address { Id = 0, Value = string.Empty } };
            dto.PhoneNumbers = new[] { new PhoneNumber { Id = 0, Value = string.Empty } };
            dto.Websites = new[] { new Website { Id = 0, Value = string.Empty } };
            dto.TagList = string.Empty;

            ViewBag.Users = _collection.GetCollectionOfUsers();
            ViewBag.Types = _collection.GetCollectionOfClientTypes();
            ViewBag.TagLists = _collection.GetCollectionOfTags(string.Empty, string.Empty);

            return View(dto);
        }

        /// <summary>
        /// Creates the specified client.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <returns>The view.</returns>
        [HttpPost]
        [Authorize]
        public ActionResult Create(CreateUpdateViewModel client)
        {
            if (ModelState.IsValid)
            {
                int id = CreateClient(client);

                return RedirectToAction("Details", new { Id = id });
            }

            ViewBag.Users = _collection.GetCollectionOfUsers();
            ViewBag.Types = _collection.GetCollectionOfClientTypes();
            ViewBag.TagLists = _collection.GetCollectionOfTags(string.Empty, string.Empty);

            return View(client);
        }
        
        /// <summary>
        /// Edits the specified client.
        /// </summary>
        /// <param name="id">The ID of specified client.</param>
        /// <returns>The view.</returns>
        [Authorize]
        public ActionResult Edit(int id)
        {
            var src =
                _db.All<Client>().Include(c => c.Emails)
                    .Include(c => c.Addresses)
                    .Include(c => c.Websites)
                    .Include(c => c.PhoneNumbers)
                    .Include(c => c.Tags)
                    .FirstOrDefault(c => c.Id == id);

            if (src == null) return RedirectToAction("Index");

            var dto = ObjectMapperManager.DefaultInstance
                .GetMapper<Client, CreateUpdateViewModel>()
                .Map(src);
            dto.TagList = string.Join(", ", src.Tags.Select(c => c.Value));

            ViewBag.Users = _collection.GetCollectionOfUsers();
            ViewBag.Types = _collection.GetCollectionOfClientTypes();
            ViewBag.TagLists = _collection.GetCollectionOfTags(string.Empty, string.Empty);

            return View(dto);
        }
        
        /// <summary>
        /// Request from the specified client editor.
        /// </summary>
        /// <param name="client">The client view model.</param>
        /// <returns>The view.</returns>
        [HttpPost]
        [Authorize]
        public ActionResult Edit(CreateUpdateViewModel client)
        {
            if (ModelState.IsValid)
            {
                int id = UpdateClient(client);
                return RedirectToAction("Details", new { Id = id });
            }

            ViewBag.Users = _collection.GetCollectionOfUsers();
            ViewBag.Types = _collection.GetCollectionOfClientTypes();
            ViewBag.TagLists = _collection.GetCollectionOfTags(string.Empty);

            return View(client);
        }

        /// <summary>
        /// Get's some partial view as item.
        /// </summary>
        /// <param name="id">The item ID.</param>
        /// <param name="field">The field name.</param>
        /// <returns>The partial view.</returns>
        public PartialViewResult AddItem(string id, string field)
        {
            var i = Convert.ToInt32(id);
            CreateUpdateViewModel m;
            switch (field)
            {
                case "Emails":
                    m = new CreateUpdateViewModel { Emails = new Email[i + 1] };
                    m.Emails[i] = new Email(string.Empty);
                    m.PartialViewParameterIndex = i;
                    return PartialView("_EmailPartialView", m);

                case "PhoneNumbers":
                    m = new CreateUpdateViewModel { PhoneNumbers = new PhoneNumber[i + 1] };
                    m.PhoneNumbers[i] = new PhoneNumber(string.Empty);
                    m.PartialViewParameterIndex = i;
                    return PartialView("_PhoneNumberPartialView", m);

                case "Addresses":
                    m = new CreateUpdateViewModel { Addresses = new Address[i + 1] };
                    m.Addresses[i] = new Address(string.Empty);
                    m.PartialViewParameterIndex = i;
                    return PartialView("_AddressPartialView", m);

                case "Websites":
                    m = new CreateUpdateViewModel { Websites = new Website[i + 1] };
                    m.Websites[i] = new Website(string.Empty);
                    m.PartialViewParameterIndex = i;
                    return PartialView("_WebSitePartialView", m);
            }

            return new PartialViewResult();
        }

        /// <summary>
        /// Deletes the specified client.
        /// </summary>
        /// <param name="id">The client ID.</param>
        /// <returns>The view.</returns>
        [Authorize]
        public ActionResult Delete(int id)
        {
            return View(GetDetailsFromClient(id));
        }

        /// <summary>
        /// Deletes the confirmed.
        /// </summary>
        /// <param name="id">The client ID.</param>
        /// <returns>The view.</returns>
        [HttpPost, ActionName("Delete")]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            _db.Delete<Client>(c => c.Id == id);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Releases unmanaged resources and optionally releases managed resources.
        /// </summary>
        /// <param name="disposing">True to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }

        /// <summary>
        /// Gets the cookie.
        /// </summary>
        /// <param name="cookieName">Name of the cookie.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The cookie value</returns>
        private string GetCookie(string cookieName, string defaultValue)
        {
            var httpCookie = Request.Cookies[cookieName];
            return httpCookie == null ? defaultValue : Server.HtmlEncode(httpCookie.Value).Replace("amp;", string.Empty);
        }
    }
}