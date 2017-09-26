using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using GigHub.Persistance;
using GigHub.Core;

namespace GigHub.Controllers.Api
{

    [Authorize]
    public class AttendancesController : ApiController
    {

        private readonly IUnitOfWork _unitOfWork;

        public AttendancesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            var currentUserId = User.Identity.GetUserId();

            var attendance = _unitOfWork.Attendances.GetAttendance(dto.GigId, currentUserId);

            if(attendance != null)
                return BadRequest("Attendance already exists");

            attendance = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = currentUserId
            };

            _unitOfWork.Attendances.Add(attendance);

            _unitOfWork.Complete();

            return Ok();

        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var currentUserId = User.Identity.GetUserId();

            var attendance = _unitOfWork.Attendances.GetAttendance(id, currentUserId);

            if (attendance == null)
                return NotFound();

            _unitOfWork.Attendances.Remove(attendance);
            _unitOfWork.Complete();

            return Ok(id);
        }

    }
}
