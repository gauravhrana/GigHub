using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using GigHub.ViewModels;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {

        private ApplicationDbContext _context;
        public HomeController()
        {
            _context = new Models.ApplicationDbContext();
        }
        public ActionResult Index(string query = null)
        {
            var upComingGigs = _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                //.Where(g => g.DateTime > DateTime.Now && !g.IsCancelled);
                .Where(g => g.DateTime > DateTime.Now);

            if (!string.IsNullOrWhiteSpace(query))
            {
                upComingGigs = upComingGigs.
                    Where(g =>
                            g.Artist.Name.Contains(query) ||
                            g.Venue.Contains(query) ||
                            g.Genre.Name.Contains(query));
            }

            var gigsViewModel = new GigsViewModel
            {
                UpcomingGigs = upComingGigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcoming Gigs",
                SearchTerm = query
            };

            return View("Gigs", gigsViewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}