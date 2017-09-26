using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GigHub.Core.Models;
using GigHub.Core.Repositories;

namespace GigHub.Persistance.Repositories
{
    public class GigsRepository : IGigsRepository
    {

        private readonly ApplicationDbContext _context;

        public GigsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Gig> GetGigsUserAttending(string userId)
        {
            return _context.Attendances
                .Where(x => x.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();
        }
        public IEnumerable<Gig> GetUpComingGigsByArtists(string userId)
        {
            return _context.Gigs
                .Where(g => g.ArtistId == userId
                                && g.DateTime > DateTime.Now
                                && !g.IsCancelled)
                .Include(g => g.Genre)
                .ToList();
        }

        public Gig GetGig(int gigId)
        {
            return _context.Gigs
                    .Include(g => g.Artist)
                    .Include(g => g.Genre)
                    .SingleOrDefault(g => g.Id == gigId);
        }

        public Gig GetGigWithAttendess(int gigId)
        {
            return _context.Gigs
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .SingleOrDefault(g => g.Id == gigId);
        }

        public void Add(Gig gig)
        {
            _context.Gigs.Add(gig);
        }

        public IEnumerable<Gig> GetUpComingGigs(string query)
        {
            var upComingGigs = _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.DateTime > DateTime.Now);

            if (!string.IsNullOrWhiteSpace(query))
            {
                upComingGigs = upComingGigs.
                    Where(g =>
                            g.Artist.Name.Contains(query) ||
                            g.Venue.Contains(query) ||
                            g.Genre.Name.Contains(query));
            }

            return upComingGigs;
        }
    }
}