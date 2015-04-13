using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using ClientManagerBase.Context;
using ClientManagerBase.Models.Clients;
using ClientManagerBase.Models.Letters;
using ClientManagerBase.Repository;
using ClientManagerBase.Services;
using ClientManagerWeb.ViewModels.Templates;
using EmitMapper;

namespace ClientManagerWeb.Controllers
{
    /// <summary>
    /// Controller for CRUD operations on letters templates.
    /// </summary>
    public class TemplatesController : Controller
    {
        private readonly IDataRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplatesController"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public TemplatesController(IDataRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplatesController"/> class.
        /// </summary>
        public TemplatesController() : this(new DatabaseRepository(new DatabaseContext()))
        {
        }

        /// <summary>
        /// GET: /Templates/
        /// </summary>
        /// <returns>List of templates.</returns>
        [Authorize]
        public ViewResult Index()
        {
            var vmList = ObjectMapperManager.DefaultInstance
                                            .GetMapper<List<Template>, List<TemplatesListRecordViewModel>>()
                                            .Map(_repository.All<Template>().ToList());
            return View(vmList);
        }

        /// <summary>
        /// GET: /Templates/Details/5
        /// </summary>
        /// <param name="id">The template ID.</param>
        /// <returns>Template details view if ID exists; otherwise - list of templates.</returns>
        [Authorize]
        public ActionResult Details(int id)
        {
            var template = _repository.All<Template>(t => t.Id == id).Include(t => t.Tags).FirstOrDefault();

            // Template doesn't exist.
            if (template == null)
                return RedirectToAction("Index");

            // Template exists.
            var vm = ObjectMapperManager.DefaultInstance.GetMapper<Template, TemplateDetailsViewModel>().Map(template);
            vm.LetterContent = vm.LetterContent.Replace("\r\n", "<br />");
            vm.SelectedTags = string.Join(", ", template.Tags.Select(t => t.Value));

            return View(vm);
        }

        /// <summary>
        /// GET: /Templates/Create
        /// </summary>
        /// <returns>Form for creating new template.</returns>
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Tags = GetAllTags();
            return View();
        } 

        /// <summary>
        /// POST: /Templates/Create
        /// </summary>
        /// <param name="vm">The template.</param>
        /// <returns>List of templates if creation succeed; otherwise - validation errors.</returns>
        [HttpPost]
        [Authorize]
        public ActionResult Create(TemplateCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                // Check if template with the same name is already exists.
                if (_repository.Single<Template>(t => t.Name == vm.Name) == null)
                {
                    var template = ObjectMapperManager.DefaultInstance
                                                      .GetMapper<TemplateCreateEditViewModel, Template>()
                                                      .Map(vm);

                    // Add new tags from entered names.
                    var service = new TagService(_repository);
                    template.Tags = service.GetTagsFromString(vm.NewTags) ?? new List<Tag>();

                    // Add existent selected tags.
                    if (vm.SelectedTags != null)
                    {
                        foreach (var tagId in vm.SelectedTags)
                        {
                            var id = tagId;

                            // Check if this tag id already added.
                            if (template.Tags.Any(t => t.Id == id))
                                continue;

                            // Add tag from database.
                            var tag = _repository.Single<Tag>(t => t.Id == id);
                            if (tag != null)
                                template.Tags.Add(tag);
                        }
                    }

                    _repository.Add(template);
                    _repository.SaveChanges();
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, "Template with this name is already exists.");
            }

            ViewBag.Tags = GetAllTags();
            return View(vm);
        }

        /// <summary>
        /// GET: /Templates/Edit/5
        /// </summary>
        /// <param name="id">The template ID.</param>
        /// <returns>Edit form if ID exists; otherwise - list of templates.</returns>
        [Authorize]
        public ActionResult Edit(int id)
        {
            var template = _repository.All<Template>(t => t.Id == id).Include(t => t.Tags).FirstOrDefault();

            // Template doesn't exist.
            if (template == null)
                return RedirectToAction("Index");

            // Template exists.
            var vm = ObjectMapperManager.DefaultInstance.GetMapper<Template, TemplateCreateEditViewModel>().Map(template);
            vm.SelectedTags = template.Tags.Select(t => t.Id).ToArray();

            ViewBag.Tags = GetAllTags();
            return View(vm);
        }

        /// <summary>
        /// POST: /Templates/Edit/5
        /// </summary>
        /// <param name="vm">The template view model.</param>
        /// <returns>List of templates if successfully edited; otherwise - validation errors.</returns>
        [HttpPost]
        [Authorize]
        public ActionResult Edit(TemplateCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                // Check if template with the same name is already exists.
                if (_repository.Single<Template>(t => t.Name == vm.Name && t.Id != vm.Id) == null)
                {
                    var template = _repository.All<Template>(t => t.Id == vm.Id).Include(t => t.Tags).FirstOrDefault();

                    ObjectMapperManager.DefaultInstance
                                       .GetMapper<TemplateCreateEditViewModel, Template>().Map(vm, template);

                    // Clear previous tags.
                    template.Tags.Clear();

                    // Add new tags from entered names.
                    var service = new TagService(_repository);
                    template.Tags = service.GetTagsFromString(vm.NewTags) ?? new List<Tag>();

                    // Add existent selected tags.
                    if (vm.SelectedTags != null)
                    {
                        foreach (var tagId in vm.SelectedTags)
                        {
                            var id = tagId;

                            // Check if this tag id already added.
                            if (template.Tags.Any(t => t.Id == id))
                                continue;

                            // Add tag from database.
                            var tag = _repository.Single<Tag>(t => t.Id == id);
                            if (tag != null)
                                template.Tags.Add(tag);
                        }
                    }

                    _repository.SaveChanges();
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, "Template with this name is already exists.");
            }

            ViewBag.Tags = GetAllTags();
            return View(vm);
        }

        /// <summary>
        /// GET: /Templates/Delete/5
        /// </summary>
        /// <param name="id">The template ID.</param>
        /// <returns>Delete confirmation if ID exists; otherwise - list of templates.</returns>
        [Authorize]
        public ActionResult Delete(int id)
        {
            var template = _repository.All<Template>(t => t.Id == id).Include(t => t.Tags).FirstOrDefault();

            // Template doesn't exist.
            if (template == null)
                return RedirectToAction("Index");

            // Template exists.
            var vm = ObjectMapperManager.DefaultInstance.GetMapper<Template, TemplateDetailsViewModel>().Map(template);
            vm.LetterContent = vm.LetterContent.Replace("\r\n", "<br />");
            vm.SelectedTags = string.Join(", ", template.Tags.Select(t => t.Value));

            return View(vm);
        }

        /// <summary>
        /// POST: /Templates/Delete/5
        /// </summary>
        /// <param name="id">The template ID.</param>
        /// <returns>List of templates.</returns>
        [HttpPost, ActionName("Delete")]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            var template = _repository.Single<Template>(t => t.Id == id);

            // Template exists.
            if (template != null)
            {
                _repository.Delete(template);
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
        /// Gets all tags view models from repository.
        /// </summary>
        /// <returns>Tags view models.</returns>
        private List<TemplateTagViewModel> GetAllTags()
        {
            var tags = _repository.All<Tag>().ToList();

            return ObjectMapperManager.DefaultInstance
                                      .GetMapper<List<Tag>, List<TemplateTagViewModel>>()
                                      .Map(tags);
        }
    }
}