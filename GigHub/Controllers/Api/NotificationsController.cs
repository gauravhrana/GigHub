using AutoMapper;
using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class NotificationsController : ApiController
    {

        private ApplicationDbContext _context;

        public NotificationsController()
        {
            _context = new Models.ApplicationDbContext();
        }

        public IEnumerable<NotificationDto> GetNewNotifications()
        {
            var currentUserId = User.Identity.GetUserId();
            var notifications = _context.UserNotifications
                                    //.Where(un => un.UserId == currentUserId && !un.IsRead)
                                    .Where(un => un.UserId == currentUserId)
                                    .Select(un => un.Notification)
                                    .Include(n => n.Gig.Artist)
                                    .Take(15)
                                    .ToList();

            return notifications.Select(Mapper.Map<Notification, NotificationDto>);
        }

        [HttpPost]
        public IHttpActionResult MarkAsRead()
        {
            var userId = User.Identity.GetUserId();
            var notifications = _context.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .ToList();

            notifications.ForEach(n => n.Read());

            _context.SaveChanges();

            return Ok();
        }
    }
}
