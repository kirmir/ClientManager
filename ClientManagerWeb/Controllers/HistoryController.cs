using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using ClientManagerBase.Context;
using ClientManagerBase.Models.Letters;
using ClientManagerBase.Repository;
using ClientManagerWeb.ViewModels.History;

namespace ClientManagerWeb.Controllers
{
    /// <summary>
    /// The history controller.
    /// </summary>
    public class HistoryController : Controller
    {
        private readonly IDataRepository _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="HistoryController"/> class.
        /// </summary>
        /// <param name="repository">The database framework.</param>
        public HistoryController(IDataRepository repository)
        {
            _db = repository;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HistoryController"/> class.
        /// </summary>
        public HistoryController()
            : this(new DatabaseRepository(new DatabaseContext()))
        {
        }

        /// <summary>
        /// Browse this instance.
        /// </summary>
        /// <returns>The view.</returns>
        [Authorize]
        public ActionResult Index()
        {
            var dto = new FilterHistoryViewModel();
            dto.DateTill = DateTime.Now;
            dto.DateFrom = dto.DateTill.AddMonths(-1);
            dto.History = GetUnread(dto.DateFrom, dto.DateTill);

            return View(dto);
        }

        /// <summary>
        /// Browse this instance.
        /// </summary>
        /// <param name="history">The history.</param>
        /// <returns>
        /// The view.
        /// </returns>
        [HttpPost]
        [Authorize]
        public ActionResult Index(FilterHistoryViewModel history)
        {
            var dto = new FilterHistoryViewModel();
            dto.DateTill = history.DateTill;
            dto.DateFrom = history.DateFrom;
            dto.History = GetUnread(dto.DateFrom, dto.DateTill);

            return View(dto);
        }

        /// <summary>
        /// Details the specified letter.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <returns>The view.</returns>
        [Authorize]
        public ActionResult Details(int id)
        {
            var src = _db.All<Letter>(c => c.Id == id).Include(c => c.Client).FirstOrDefault();
            if (src == null) return RedirectToAction("Index");

            var dto = EmitMapper.ObjectMapperManager.DefaultInstance.GetMapper<Letter, DetailsViewModel>().Map(src);
            dto.ClientTitle = string.Format("{0} ({1} {2})", src.Client.Title, src.Client.FirstName, src.Client.SecondName);

            return View(dto);
        }
        
        /// <summary>
        /// Gets the unread.
        /// </summary>
        /// <param name="dateFrom">The date from.</param>
        /// <param name="dateTill">The date till.</param>
        /// <returns>The unread.</returns>
        private IEnumerable<HistoryViewModel> GetUnread(DateTime dateFrom, DateTime dateTill)
        {
            return _db.All<Letter>(
                c =>
                c.ActualSentDate != null && c.ActualSentDate.Value >= dateFrom && c.ActualSentDate.Value <= dateTill)
                .Include(c => c.Client)
                .Select(c => new
                                 {
                                     c.Client.Title,
                                     c.Client.FirstName,
                                     c.Client.SecondName,
                                     c.Id,
                                     c.FromNameEmail,
                                     c.ActualSentDate.Value,
                                     c.Subject
                                 })
                .ToList()
                .Select(c => new HistoryViewModel
                                 {
                                     To = string.Format("{0} ({1} {2})", c.Title, c.FirstName, c.SecondName),
                                     Id = c.Id,
                                     From = c.FromNameEmail,
                                     SendDate = c.Value,
                                     Subject = c.Subject
                                 });
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
