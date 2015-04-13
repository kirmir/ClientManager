using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using ClientManagerBase.Context;
using ClientManagerBase.Models;
using ClientManagerBase.Models.Letters;
using ClientManagerBase.Repository;
using ClientManagerBase.Services;
using ClientManagerWeb.Models.Helpers;
using ClientManagerWeb.ViewModels.Letters;
using EmitMapper;

namespace ClientManagerWeb.Controllers
{
    /// <summary>
    /// Controller to work with letters awaiting sending.
    /// </summary>
    public class LettersController : Controller
    {
        private readonly IDataRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="LettersController"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public LettersController(IDataRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LettersController"/> class.
        /// </summary>
        public LettersController() : this(new DatabaseRepository(new DatabaseContext()))
        {
        }

        /// <summary>
        /// GET: /Letters/
        /// </summary>
        /// <returns>List of letters ready to send.</returns>
        [Authorize]
        public ViewResult Index()
        {
            // Get current user.
            var userService = new UserService(_repository);
            var user = userService.GetUserByName(User.Identity.Name);

            // Generate new letters.
            var letterService = new LetterService(_repository);
            letterService.GenerateNewLettersForToday();

            // Retrieve all letters to show.
            var unsent = letterService.GetAllUnsentLetters(user).ToList();
            var vm = new LettersListViewModel
                     {
                         Letters = (from letter in unsent
                                    select new LettersListRecordViewModel
                                           {
                                               Id = letter.Id,
                                               SendTo = string.Format("{0} {1} ({2}) <{3}>", letter.Client.SecondName,
                                                                                             letter.Client.FirstName,
                                                                                             letter.Client.Title,
                                                                                             letter.Client.MainEmail),
                                               Subject = letter.Subject,
                                               ExpectedSendDate = letter.ExpectedSendDate
                                           }).ToArray()
                     };

            return View(vm);
        }

        /// <summary>
        /// POST: /Letters/
        /// </summary>
        /// <param name="vm">View model that contains list of letters to send and to cancel.</param>
        /// <returns>Summary of sending letters.</returns>
        [HttpPost]
        [Authorize]
        public ActionResult Index(LettersListViewModel vm)
        {
            if (vm.Letters == null)
                return RedirectToAction("Index");

            var sender = new EmailSender(_repository);

            // Send selected letters.
            var successfull = new List<LettersListRecordViewModel>();
            var failed = new List<LettersListRecordViewModel>();

            if (vm.SelectedLetters != null)
            {
                foreach (var selectedId in vm.SelectedLetters)
                {
                    var id = selectedId;
                    var letter = _repository.All<Letter>(l => l.Id == id).Include(l => l.Client).FirstOrDefault();

                    // Letter exist.
                    if (letter != null && letter.ActualSentDate == null && !letter.IsCanceled)
                    {
                        var user = _repository.Single<User>(u => u.Id == letter.Client.UserId);

                        var letterInfo = new LettersListRecordViewModel
                                         {
                                             SendTo = string.Format("{0} {1} ({2}) <{3}>", letter.Client.SecondName,
                                                                                           letter.Client.FirstName,
                                                                                           letter.Client.Title,
                                                                                           letter.Client.MainEmail),
                                             Subject = letter.Subject
                                         };

                        if (sender.SendLetter(letter, user))
                            successfull.Add(letterInfo);
                        else
                            failed.Add(letterInfo);
                    }
                }
            }

            // Cancel all unselected letters.
            var toCancel = vm.SelectedLetters == null ?
                vm.Letters : vm.Letters.Where(l => !vm.SelectedLetters.Contains(l.Id));

            foreach (var letterVm in toCancel)
            {
                var info = letterVm;
                var letter = _repository.All<Letter>(l => l.Id == info.Id).Include(l => l.Client).FirstOrDefault();

                if (letter != null && letter.ActualSentDate == null)
                    letter.IsCanceled = true;
            }

            _repository.SaveChanges();

            if (successfull.Any())
                ViewBag.Successfull = successfull;
            if (failed.Any())
                ViewBag.Failed = failed;

            return View("SendResults");
        }

        /// <summary>
        /// GET: /Letters/Details/5
        /// </summary>
        /// <param name="id">The letter ID.</param>
        /// <returns>Letter details if ID exists; otherwise - letters list.</returns>
        [Authorize]
        public ActionResult Details(int id)
        {
            var letter = _repository.All<Letter>(l => l.Id == id)
                                    .Include(l => l.Client).Include(l => l.Template)
                                    .FirstOrDefault();

            // Letter doesn't exist.
            if (letter == null)
                return RedirectToAction("Index");

            // Letter exists. Create view model.
            var vm = ObjectMapperManager.DefaultInstance.GetMapper<Letter, LetterDetailsViewModel>().Map(letter);

            var service = new UserService(_repository);
            var user = service.GetUserByName(User.Identity.Name);

            vm.Content = letter.Content.Replace("\r\n", "<br />");
            vm.FromName = user.Representation;
            vm.FromEmail = user.Email;
            vm.TemplateName = letter.Template.Name;
            vm.ToSecondFirstName = string.Format("{0} {1} ({2})", letter.Client.SecondName, letter.Client.FirstName,
                letter.Client.Title);
            vm.ToEmail = letter.Client.MainEmail;

            return View(vm);
        }
        
        /// <summary>
        /// GET: /Letters/Edit/5
        /// </summary>
        /// <param name="id">The letter ID.</param>
        /// <returns>Letter edit form if ID exists; otherwise - letters list.</returns>
        [Authorize]
        public ActionResult Edit(int id)
        {
            var letter = _repository.All<Letter>(l => l.Id == id).Include(l => l.Client).FirstOrDefault();

            // Letter doesn't exist.
            if (letter == null)
                return RedirectToAction("Index");

            // Letter exists. Create view model.
            var vm = new LetterEditViewModel
                     {
                         Id = letter.Id,
                         To = string.Format("{0} {1} ({2}) <{3}>", letter.Client.SecondName, letter.Client.FirstName,
                                                                   letter.Client.Title, letter.Client.MainEmail),
                         Subject = letter.Subject,
                         Content = letter.Content
                     };

            return View(vm);
        }

        /// <summary>
        /// POST: /Letters/Edit/5
        /// </summary>
        /// <param name="vm">The view model with changed letter.</param>
        /// <returns>List of letter if edited successfully; otherwise - validation errors.</returns>
        [HttpPost]
        [Authorize]
        public ActionResult Edit(LetterEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var letter = _repository.Single<Letter>(l => l.Id == vm.Id);
                
                // Letter exists.
                if (letter != null)
                {
                    letter.Subject = vm.Subject;
                    letter.Content = vm.Content;
                    _repository.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            return View(vm);
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