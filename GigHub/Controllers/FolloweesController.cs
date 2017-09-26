using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using GigHub.Core;

namespace GigHub.Controllers
{
    public class FolloweesController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public FolloweesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            var artists = _unitOfWork.ApplicationUsers.GetFollowersByUserId(userId);

            return View(artists);
        }
    }
}