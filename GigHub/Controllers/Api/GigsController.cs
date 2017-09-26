using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Persistance;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class GigsController : ApiController
    {

        private readonly IUnitOfWork _unitOfWork;

        public GigsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var currentUserId = User.Identity.GetUserId();

            var gig = _unitOfWork.Gigs.GetGigWithAttendess(id);

            if(gig == null || gig.IsCancelled)
                return NotFound();

            if (gig.ArtistId != currentUserId)
                return Unauthorized();

            gig.Cancel();

            _unitOfWork.Complete();

            return Ok();
        }

    }
}
