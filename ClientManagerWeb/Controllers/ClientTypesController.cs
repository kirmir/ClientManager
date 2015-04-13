using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ClientManagerBase.Context;
using ClientManagerBase.Models.Clients;
using ClientManagerBase.Repository;
using ClientManagerWeb.ViewModels.ClientTypes;
using EmitMapper;

namespace ClientManagerWeb.Controllers
{ 
    /// <summary>
    /// Controller for CRUD operations on client types.
    /// </summary>
    public class ClientTypesController : Controller
    {
        private readonly IDataRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientTypesController"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public ClientTypesController(IDataRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientTypesController"/> class.
        /// </summary>
        public ClientTypesController() : this(new DatabaseRepository(new DatabaseContext()))
        {
        }

        /// <summary>
        /// GET: /ClientTypes/
        /// </summary>
        /// <returns>List of client types.</returns>
        [Authorize]
        public ViewResult Index()
        {
            var types = ObjectMapperManager.DefaultInstance
                                           .GetMapper<List<ClientType>, List<ClientTypeViewModel>>()
                                           .Map(_repository.All<ClientType>().ToList());

            return View(types);
        }

        /// <summary>
        /// GET: /ClientTypes/Create
        /// </summary>
        /// <returns>Form for creating new client type.</returns>
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// POST: /ClientTypes/Create
        /// </summary>
        /// <param name="vm">Client type view model.</param>
        /// <returns>List of client types if creation succeed; otherwise - validation errors.</returns>
        [HttpPost]
        [Authorize]
        public ActionResult Create(ClientTypeViewModel vm)
        {
            if (ModelState.IsValid)
            {
                // Check if client type with the same name is already exists.
                if (_repository.Single<ClientType>(t => t.Value == vm.Value) == null)
                {
                    var clientType =
                        ObjectMapperManager.DefaultInstance.GetMapper<ClientTypeViewModel, ClientType>().Map(vm);
                    _repository.Add(clientType);
                    _repository.SaveChanges();

                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, "Client type with this name is already exists.");
            }

            return View(vm);
        }

        /// <summary>
        /// GET: /ClientTypes/Edit/5
        /// </summary>
        /// <param name="id">The client type ID.</param>
        /// <returns>Form for editing client type if its ID exists; otherwise - list of client types.</returns>
        [Authorize]
        public ActionResult Edit(int id)
        {
            var clientType = _repository.Single<ClientType>(t => t.Id == id);

            // ID doesn't exist.
            if (clientType == null)
                return RedirectToAction("Index");

            var vm = ObjectMapperManager.DefaultInstance.GetMapper<ClientType, ClientTypeViewModel>().Map(clientType);

            return View(vm);
        }

        /// <summary>
        /// POST: /ClientTypes/Edit/5
        /// </summary>
        /// <param name="vm">The client type view model.</param>
        /// <returns>Returns client types list if edited successfully; otherwise - validation errors.</returns>
        [HttpPost]
        [Authorize]
        public ActionResult Edit(ClientTypeViewModel vm)
        {
            if (ModelState.IsValid)
            {
                // Check if client type with the same name is already exists.
                if (_repository.Single<ClientType>(t => t.Value == vm.Value && t.Id != vm.Id) == null)
                {
                    var clientType = new ClientType { Id = vm.Id };
                    _repository.Attach(clientType);
                    clientType.Value = vm.Value;
                    _repository.SaveChanges();

                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, "Client type with this name is already exists.");
            }

            return View(vm);
        }

        /// <summary>
        /// GET: /ClientTypes/Delete/5
        /// </summary>
        /// <param name="id">The client type ID.</param>
        /// <returns>Delete confirmation if ID exists; otherwise - list of client types.</returns>
        [Authorize]
        public ActionResult Delete(int id)
        {
            var clientType = _repository.Single<ClientType>(t => t.Id == id);

            // ID doesn't exist.
            if (clientType == null)
                return RedirectToAction("Index");

            var vm = ObjectMapperManager.DefaultInstance.GetMapper<ClientType, ClientTypeViewModel>().Map(clientType);

            // Check if type is used in any client.
            ViewBag.TypeIsUsed = _repository.All<Client>(c => c.TypeId == id).Any();

            return View(vm);
        }

        /// <summary>
        /// POST: /ClientTypes/Delete/5
        /// </summary>
        /// <param name="id">The client type ID.</param>
        /// <returns>List of client types.</returns>
        [HttpPost, ActionName("Delete")]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            // Check if type is used in any client.
            if (_repository.All<Client>(c => c.TypeId == id).Any())
                return RedirectToAction("Index");

            // It is not used, so can be deleted.
            var clientType = _repository.Single<ClientType>(t => t.Id == id);

            // ID exists.
            if (clientType != null)
            {
                _repository.Delete(clientType);
                _repository.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Releases unmanaged resources and optionally releases managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources;
        /// <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            _repository.Dispose();
            base.Dispose(disposing);
        }
    }
}