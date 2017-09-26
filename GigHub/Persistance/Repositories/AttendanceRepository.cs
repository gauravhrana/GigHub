﻿using System;
using System.Collections.Generic;
using System.Linq;
using GigHub.Core.Models;
using GigHub.Core.Repositories;

namespace GigHub.Persistance.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {

        private readonly ApplicationDbContext _context;

        public AttendanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Attendance> GetFutureAttendances(string currentUserId)
        {
            return _context.Attendances
                            .Where(a => a.AttendeeId == currentUserId && a.Gig.DateTime > DateTime.Now)
                            .ToList();
        }

        public Attendance GetAttendance(int gigId, string userId)
        {
            return _context.Attendances.SingleOrDefault(
                a => a.GigId == gigId && a.AttendeeId == userId);

        }

        public void Add(Attendance attendance)
        {
            _context.Attendances.Add(attendance);
        }

        public void Remove(Attendance attendance)
        {
            _context.Attendances.Remove(attendance);
        }
    }
}