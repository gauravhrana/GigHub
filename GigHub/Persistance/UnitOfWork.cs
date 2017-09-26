using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Persistance.Repositories;

namespace GigHub.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {

        ApplicationDbContext _context;

        public IAttendanceRepository Attendances { get; private set; }

        public IGigsRepository Gigs { get; private set; }

        public IFollowingRepository Followings { get; private set; }

        public IGenreRepository Genres { get; private set; }

        public  IApplicationUserRepository ApplicationUsers { get; private set; }

        public  INotificationRepository Notifications { get; private set; }

        public IUserNotificationRepository UserNotifications { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            Attendances = new AttendanceRepository(_context);
            Gigs = new GigsRepository(_context);
            Followings = new FollowingRepository(_context);
            Genres = new GenreRepository(_context);
            ApplicationUsers = new ApplicationUserRepository(_context);
            Notifications = new NotificationRepository(_context);
            UserNotifications = new UserNotificationRepository(_context);

        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}