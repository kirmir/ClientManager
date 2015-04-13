using System.Web.Mvc;
using System.Web.Security;
using ClientManagerBase.Context;
using ClientManagerBase.Repository;
using ClientManagerBase.Services;
using ClientManagerWeb.ViewModels.Account;

namespace ClientManagerWeb.Controllers
{
    /// <summary>
    /// Controller for users accounts and authentication.
    /// </summary>
    public class AccountController : Controller
    {
        private readonly IDataRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public AccountController(IDataRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        public AccountController()
            : this(new DatabaseRepository(new DatabaseContext()))
        {
        }

        /// <summary>
        /// GET: /Account/LogOn
        /// </summary>
        /// <returns>Login form.</returns>
        public ActionResult LogOn()
        {
            return View();
        }

        /// <summary>
        /// POST: /Account/LogOn
        /// </summary>
        /// <param name="model">User log on details.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>Returns to last URL if loginning was successful; otherwise - shows error.</returns>
        [HttpPost]
        public ActionResult LogOn(LogOnUserViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var userService = new UserService(_repository);
                if (userService.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.KeepLoggedIn);
                    
                    // Return to previous URL.
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                        return Redirect(returnUrl);
                    
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "The user name or password provided is incorrect.");
            }

            // If we got this far, something failed, redisplay form.
            return View(model);
        }

        /// <summary>
        /// GET: /Account/LogOff
        /// </summary>
        /// <returns>Home page.</returns>
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// GET: /Account/Register
        /// </summary>
        /// <returns>Register user form.</returns>
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// POST: /Account/Register
        /// </summary>
        /// <param name="model">The new user details.</param>
        /// <returns>Home page if registering was successful; otherwise - show error..</returns>
        [HttpPost]
        public ActionResult Register(RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user.
                var userService = new UserService(_repository);
                if (userService.CreateUser(model.UserName, model.Password,
                    model.Email.Trim(), model.Representation.Trim()) != null)
                {
                    // Successful user creation.
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    return RedirectToAction("Index", "Home");
                }

                // Can't create user.
                ModelState.AddModelError(string.Empty, "User with such name is already exists.");
            }

            // If we got this far, something failed, redisplay form.
            return View(model);
        }

        /// <summary>
        /// GET: /Account/EditInfo
        /// </summary>
        /// <returns>Form for changing user account information.</returns>
        [Authorize]
        public ActionResult EditInfo()
        {
            var userService = new UserService(_repository);
            var user = userService.GetUserByName(User.Identity.Name);

            // Check if user exists.
            if (user != null)
            {
                var vm = new EditUserInfoViewModel { Email = user.Email, Representation = user.Representation };
                return View(vm);
            }

            return RedirectToAction("LogOff");
        }

        /// <summary>
        /// POST: /Account/EditInfo
        /// </summary>
        /// <param name="model">User data to change.</param>
        /// <returns>Success message if data was changed; otherwise - shows errors.</returns>
        [Authorize]
        [HttpPost]
        public ActionResult EditInfo(EditUserInfoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userService = new UserService(_repository);
                if (userService.EditUserInfo(User.Identity.Name, model.OldPassword,
                    model.NewPassword, model.Email.Trim(), model.Representation.Trim()))
                    return RedirectToAction("EditInfoSuccess");

                ModelState.AddModelError(string.Empty, "The user doesn't exist or password is invalid.");
            }

            // If we got this far, something failed, redisplay form.
            return View(model);
        }

        /// <summary>
        /// GET: /Account/EditInfoSuccess
        /// </summary>
        /// <returns>Message about successful account editing.</returns>
        public ActionResult EditInfoSuccess()
        {
            return View();
        }

        /// <summary>
        /// Releases unmanaged resources and optionally releases managed
        /// resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged
        /// resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            _repository.Dispose();
            base.Dispose(disposing);
        }
    }
}
