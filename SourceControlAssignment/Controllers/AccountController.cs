using SourceControlAssignment.Repository;
using SourceControlAssignment.ViewModels;
using System;
using System.Web.Mvc;
using SourceControlAssignment.Models;
using System.IO;

namespace SourceControlAssignment.Controllers
{
    /// <summary>
    /// <c>AccountController</c> manages user's account activity.
    /// </summary>
    public class AccountController : Controller
    {
        #region private variables
        private UserRepository UserRepository;
        #endregion

        #region Constructors
        public AccountController()
        {
            UserRepository = new UserRepository();
        }
        #endregion

        #region Login
        /// <summary>
        /// <c>Login</c> manages user's login view.
        /// </summary>
        /// <param name="returnUrl">URL to redirect after user login</param>
        /// <returns>Login View.</returns>
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (Session["UserID"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            TempData["ReturnUrl"] = returnUrl;
            return View();
        }

        /// <summary>
        /// <c>Login</c> login post method to authorize user credentials.
        /// </summary>
        /// <param name="loginViewModel">User credentials.</param>
        /// <returns>View based on user credentials.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                User user = UserRepository.IsUserAuthenticated(loginViewModel.Email, Helpers.Helpers.Hash(loginViewModel.Password));
                if (user != null)
                {
                        Session["UserID"] = user.UserID;
                        Session["UserGID"] = user.rowguid;
                        if (TempData["ReturnUrl"] != null)
                        {
                            string returnUrl = TempData["ReturnUrl"].ToString();
                            if (!string.IsNullOrEmpty(returnUrl))
                            {
                                return Redirect(returnUrl);
                            }
                        }
                        return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }
            return View(loginViewModel);
        }
        #endregion

        #region Register
        /// <summary>
        /// <c>Register</c> manages registeration view.
        /// </summary>
        /// <returns>Registration View.</returns>
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// <c>Register</c> register post method to register user in database and send confirmation mail.
        /// </summary>
        /// <param name="user">User details.</param>
        /// <returns>Redirects to Confirm method.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Register(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                User user1 = new User();
                user1.Birthdate = user.Birthdate;
                user1.CreatedDate = DateTime.Now;
                user1.Email = user.Email;
                user1.FirstName = user.FirstName;
                user1.Height = user.Height;
                user1.LastName = user.LastName;
                user1.Password = Helpers.Helpers.Hash(user.Password);
                user1.Phone = user.Phone;
                using (var reader = new BinaryReader(user.Photo.InputStream))
                {
                    user1.Photo = reader.ReadBytes(user.Photo.ContentLength);
                }
                user1.rowguid = Guid.NewGuid();
                user1.Weight = user.Weight;
                bool status = UserRepository.InsertUser(user1);
                if (status == true)
                {
                    LoginViewModel loginViewModel = new LoginViewModel() { Email = user.Email, Password = user.Password };
                    return Login(loginViewModel);
                }
            }
            return View(user);
        }
        #endregion

        #region Email Varification
        /// <summary>
        /// <c>IsEmailExist</c> checks if email already exist when user is trying to register.
        /// </summary>
        /// <param name="Email">User email.</param>
        /// <returns>True or False.</returns>
        [HttpPost]
        [AllowAnonymous]
        public JsonResult IsEmailExist(string Email)
        {
            bool status = UserRepository.IsEmailExist(Email);
            return Json(status);
        }
        #endregion

        #region Log Out
        /// <summary>
        /// <c>LogOut</c> Logs out user from application.
        /// </summary>
        /// <returns>Redirects to Login page.</returns>
        public ActionResult LogOut()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }
        #endregion
    }
}