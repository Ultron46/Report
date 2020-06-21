using SourceControlAssignment.Models;
using SourceControlAssignment.Repository;
using SourceControlAssignment.ViewModels;
using System;
using System.Web.Mvc;

namespace SourceControlAssignment.Controllers
{
    public class HomeController : Controller
    {
        #region private variables
        private UserRepository UserRepository;
        #endregion

        #region Constructors
        public HomeController()
        {
            UserRepository = new UserRepository();
        }
        #endregion

        public ActionResult Index()
        {
            Guid id = new Guid(Session["UserGID"].ToString());
            User user = UserRepository.GetUser(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            UserViewModel userViewModel = new UserViewModel();
            userViewModel.Birthdate = user.Birthdate;
            userViewModel.Email = user.Email;
            userViewModel.FirstName = user.FirstName;
            userViewModel.Height = user.Height;
            userViewModel.ImageSrc = string.Format("data:image;base64,{0}", Convert.ToBase64String(user.Photo));
            userViewModel.LastName = user.LastName;
            userViewModel.Phone = user.Phone;
            userViewModel.rowguid = user.rowguid;
            userViewModel.Weight = user.Weight;
            return View(userViewModel);
        }
    }
}