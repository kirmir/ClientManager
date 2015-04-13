using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using ClientManagerBase.Context;
using ClientManagerBase.Models.Clients;
using ClientManagerBase.Models.Letters;
using ClientManagerBase.Repository;
using ClientManagerWeb.ViewModels.Tags;
using EmitMapper;

namespace ClientManagerWeb.Controllers
{
    /// <summary>
    /// Controller for CRUD operations on tags.
    /// </summary>
    public class TagsController : Controller
    {
        private readonly IDataRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="TagsController"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public TagsController(IDataRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TagsController"/> class.
        /// </summary>
        public TagsController() : this(new DatabaseRepository(new DatabaseContext()))
        {
        }

        /// <summary>
        /// GET: /Tags/
        /// </summary>
        /// <returns>List of tags.</returns>
        [Authorize]
        public ViewResult Index()
        {
            var records = from t in _repository.All<Tag>()
                          select new TagsListRecordViewModel
                                 {
                                     Id = t.Id,
                                     Value = t.Value,
                                     AttachedTemplates = t.Templates.Count
                                 };

            return View(records.ToList());
        }

        /// <summary>
        /// GET: /Tags/Details/5
        /// </summary>
        /// <param name="id">The tag ID.</param>
        /// <returns>Tag details if ID exists; otherwise - list of tags.</returns>
        [Authorize]
        public ActionResult Details(int id)
        {
            var tag = _repository.All<Tag>(t => t.Id == id).Include(t => t.Templates).FirstOrDefault();

            // Tag with this ID doesn't exist.
            if (tag == null)
                return RedirectToAction("Index");

            // Tag exists.
            var tagVm = ObjectMapperManager.DefaultInstance.GetMapper<Tag, TagDetailsViewModel>().Map(tag);
            tagVm.AttachedTemplates =
                ObjectMapperManager.DefaultInstance.GetMapper<List<Template>, List<TagTemplateViewModel>>().Map(
                    tag.Templates.ToList());

            return View(tagVm);
        }

        /// <summary>
        /// GET: /Tags/Create
        /// </summary>
        /// <returns>Form for creating new tag.</returns>
        [Authorize]
        public ViewResult Create()
        {
            ViewBag.Templates = GetAllTemplates();
            return View();
        } 

        /// <summary>
        /// POST: /Tags/Create
        /// </summary>
        /// <param name="tagVm">The tag view model.</param>
        /// <returns>If creation is successful then returns to tags list; otherwise - shows errors.</returns>
        [HttpPost]
        [Authorize]
        public ActionResult Create(TagCreateEditViewModel tagVm)
        {
            if (ModelState.IsValid)
            {
                // Check if tag with the same name is already exists.
                if (_repository.Single<Tag>(t => t.Value == tagVm.Value) == null)
                {
                    // Create new tag with selected templates.
                    var tag = new Tag(tagVm.Value);

                    if (tagVm.SelectedTemplates != null)
                    {
                        tag.Templates = _repository.All<Template>()
                                                   .Where(t => tagVm.SelectedTemplates.Contains(t.Id))
                                                   .ToList();
                    }

                    _repository.Add(tag);
                    _repository.SaveChanges();

                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, "Tag with this name is already exists.");
            }

            ViewBag.Templates = GetAllTemplates();
            return View(tagVm);
        }
        
        /// <summary>
        /// GET: /Tags/Edit/5
        /// </summary>
        /// <param name="id">The tag ID.</param>
        /// <returns>If tag ID exists then returns form for editing; otherwise - list of all tags.</returns>
        [Authorize]
        public ActionResult Edit(int id)
        {
            var tag = _repository.All<Tag>(t => t.Id == id).Include(t => t.Templates).FirstOrDefault();
            
            // Tag with this ID doesn't exist.
            if (tag == null)
                return RedirectToAction("Index");

            // Tag exists.
            var tagVm = ObjectMapperManager.DefaultInstance.GetMapper<Tag, TagCreateEditViewModel>().Map(tag);
            tagVm.SelectedTemplates = tag.Templates.Select(t => t.Id).ToArray();

            ViewBag.Templates = GetAllTemplates();
            return View(tagVm);
        }

        /// <summary>
        /// POST: /Tags/Edit/5
        /// </summary>
        /// <param name="tagVm">The tag view model.</param>
        /// <returns>Returns tag list if edited successfully; otherwise - validation errors.</returns>
        [HttpPost]
        [Authorize]
        public ActionResult Edit(TagCreateEditViewModel tagVm)
        {
            if (ModelState.IsValid)
            {
                // Check if tag with the same name is already exists.
                if (_repository.Single<Tag>(t => t.Value == tagVm.Value && t.Id != tagVm.Id) == null)
                {
                    // Edit tag.
                    var tag = _repository.All<Tag>(t => t.Id == tagVm.Id).Include(t => t.Templates).FirstOrDefault();
                    
                    tag.Value = tagVm.Value;

                    tag.Templates.Clear();
                    if (tagVm.SelectedTemplates != null)
                    {
                        tag.Templates = _repository.All<Template>()
                                                   .Where(t => tagVm.SelectedTemplates.Contains(t.Id))
                                                   .ToList();
                    }

                    _repository.SaveChanges();

                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, "Tag with this name is already exists.");
            }

            ViewBag.Templates = GetAllTemplates();
            return View(tagVm);
        }

        /// <summary>
        /// GET: /Tags/Delete/5
        /// </summary>
        /// <param name="id">The tag ID.</param>
        /// <returns>Shows tag confirmation if ID exists; otherwise - returns tags list.</returns>
        [Authorize]
        public ActionResult Delete(int id)
        {
            var tag = _repository.All<Tag>(t => t.Id == id).Include(t => t.Templates).FirstOrDefault();

            // Tag with this ID doesn't exist.
            if (tag == null)
                return RedirectToAction("Index");

            // Tag exists.
            var tagVm = ObjectMapperManager.DefaultInstance.GetMapper<Tag, TagDetailsViewModel>().Map(tag);
            tagVm.AttachedTemplates =
                ObjectMapperManager.DefaultInstance.GetMapper<List<Template>, List<TagTemplateViewModel>>().Map(
                    tag.Templates.ToList());

            return View(tagVm);
        }

        /// <summary>
        /// POST: /Tags/Delete/5
        /// </summary>
        /// <param name="id">The tag ID.</param>
        /// <returns>Returns tags list.</returns>
        [HttpPost, ActionName("Delete")]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            var tag = _repository.Single<Tag>(t => t.Id == id);

            // Tag with this ID exists.
            if (tag != null)
            {
                _repository.Delete(tag);
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

        /// <summary>
        /// Gets all templates view models from repository.
        /// </summary>
        /// <returns>Templates view models with short template details.</returns>
        private List<TagTemplateViewModel> GetAllTemplates()
        {
            var templates = _repository.All<Template>().ToList();

            return ObjectMapperManager.DefaultInstance
                                      .GetMapper<List<Template>, List<TagTemplateViewModel>>()
                                      .Map(templates);
        }
    }
}