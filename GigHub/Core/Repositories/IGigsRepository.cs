using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IGigsRepository
    {
        void Add(Gig gig);
        Gig GetGig(int gigId);
        IEnumerable<Gig> GetGigsUserAttending(string userId);
        Gig GetGigWithAttendess(int gigId);
        IEnumerable<Gig> GetUpComingGigsByArtists(string userId);
        IEnumerable<Gig> GetUpComingGigs(string query);
    }
}