using GigHub.Core.Repositories;
using GigHub.Persistance.Repositories;

namespace GigHub.Core
{
    public interface IUnitOfWork
    {
        IAttendanceRepository Attendances { get; }
        IFollowingRepository Followings { get; }
        IGenreRepository Genres { get; }
        IGigsRepository Gigs { get; }
        IApplicationUserRepository ApplicationUsers { get; }
        INotificationRepository Notifications { get; }
        IUserNotificationRepository UserNotifications { get; }

        void Complete();
    }
}