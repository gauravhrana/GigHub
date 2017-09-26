using GigHub.Core.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GigHub.Core.Repositories;

namespace GigHub.Persistance.Repositories
{
    public class NotificationRepository : INotificationRepository
    {

        private readonly ApplicationDbContext _context;

        public NotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Notification> GetNewNotificationsFor(string userId)
        {
            return _context.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .Select(un => un.Notification)
                .Include(n => n.Gig.Artist)
                .ToList();
        }
    }
}