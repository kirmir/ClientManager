using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ClientManagerBase.Context;
using ClientManagerBase.Models.Clients;
using ClientManagerBase.Repository;
using ClientManagerWeb.Models.Helpers;
using ClientManagerWeb.ViewModelProviders;
using ClientManagerWeb.ViewModels.SendLetter;

namespace ClientManagerWeb.Controllers
{
    /// <summary>
    /// Send letter controller.
    /// </summary>
    public class SendLetterController : Controller
    {
        private readonly IDataRepository _db;
        private readonly SelectCollectionsProvider _collection;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientController"/> class.
        /// </summary>
        /// <param name="repository">The database framework.</param>
        public SendLetterController(IDataRepository repository)
        {
            _db = repository;
            _collection = new SelectCollectionsProvider(repository);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientController"/> class.
        /// </summary>
        public SendLetterController()
            : this(new DatabaseRepository(new DatabaseContext()))
        {
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>The view.</returns>
        [Authorize]
        public ActionResult Index()
        {
            var form = new SendLetterFormViewModel { ClientId = 0, ClientTypeId = 0 };

            ViewBag.Clients = _collection.GetCollectionOfClients(0, "ALL", 0);
            ViewBag.ClientTypes = _collection.GetCollectionOfClientTypes(0, "ALL", 0);
            return View(form);
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <returns>
        /// The view.
        /// </returns>
        [HttpPost]
        [Authorize]
        public ActionResult Index(SendLetterFormViewModel form)
        {
            IEnumerable<Client> clients;

            // Send to all posible
            if ((form.ClientId == 0) && (form.ClientTypeId == 0))
                clients = _db.All<Client>();

                // Send to some client
            else if ((form.ClientId != 0) && (form.ClientTypeId == 0))
                clients = _db.All<Client>(c => c.Id == form.ClientId);

                // Send to some client type
            else if ((form.ClientId == 0) && (form.ClientTypeId != 0))
                clients = _db.All<Client>(c => c.TypeId == form.ClientTypeId);

                // Don't select client and type in same time
            else
            {
                ViewBag.Clients = _collection.GetCollectionOfClients(0, "ALL", 0);
                ViewBag.ClientTypes = _collection.GetCollectionOfClientTypes(0, "ALL", 0);
                return View(form);
            }

            var sender = new EmailSender(_db);
            var sendResult = sender.SendLetters(clients, User.Identity.Name, form.Subject, form.LetterBody);

            var result = sendResult.Select(c => c.Email + " " + (c.WasSent ? "success" : "fail"));

            return View("SendResult", result);
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
    }
}
