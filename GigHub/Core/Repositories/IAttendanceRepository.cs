using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IAttendanceRepository
    {
        Attendance GetAttendance(int gigId, string userId);

        IEnumerable<Attendance> GetFutureAttendances(string currentUserId);

        void Add(Attendance attendance);
        void Remove(Attendance attendance);
    }
}