using System;
using System.Web.Mvc;

namespace ClientManagerWeb.Controllers
{
    /// <summary>
    /// The home controller.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Default action.
        /// </summary>
        /// <returns>The view</returns>
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";
            return View();
        }
    }
}
