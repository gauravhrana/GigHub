﻿using GigHub.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class GigsController : ApiController
    {

        private ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var currentUserId = User.Identity.GetUserId();
            var gig = _context.Gigs
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .Single(g => g.Id == id && g.ArtistId == currentUserId);

            if (gig.IsCancelled)
                return NotFound();

            gig.Cancel();

            _context.SaveChanges();

            return Ok();
        }

    }
}