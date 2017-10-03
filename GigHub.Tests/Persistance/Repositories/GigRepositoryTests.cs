using System;
using System.Data.Entity;
using FluentAssertions;
using GigHub.Core.Models;
using GigHub.Persistance.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using GigHub.Persistance;
using GigHub.Tests.Extensions;

namespace GigHub.Tests.Persistance.Repositories
{
    [TestClass]
    public class GigRepositoryTests
    {

        private GigsRepository _repository;
        private Mock<DbSet<Gig>> _mockGigs;
        private Mock<DbSet<Attendance>> _mockAttendances;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockGigs = new Mock<DbSet<Gig>>();
            _mockAttendances = new Mock<DbSet<Attendance>>();

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.SetupGet(m => m.Gigs).Returns(_mockGigs.Object);
            mockContext.SetupGet(m => m.Attendances).Returns(_mockAttendances.Object);

            _repository = new GigsRepository(mockContext.Object);
        }

        [TestMethod]
        public void GetUpComingGigsByArtist_GigsInthePast_ShouldNotBeReturned()
        {
            var gig = new Gig() { ArtistId = "1", DateTime = DateTime.Now.AddDays(-1) };

            _mockGigs.SetSource(new[] { gig });
            var gigs = _repository.GetUpComingGigsByArtists("1");

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpComingGigsByArtist_GigIsCancelled_ShouldNotBeReturned()
        {
            var gig = new Gig() { ArtistId = "1", DateTime = DateTime.Now.AddDays(1) };
            gig.Cancel();

            _mockGigs.SetSource(new[] { gig });
            var gigs = _repository.GetUpComingGigsByArtists("1");

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpComingGigsByArtist_GigIsForADifferentArtist_ShouldNotBeReturned()
        {
            var gig = new Gig() { ArtistId = "1", DateTime = DateTime.Now.AddDays(1) };

            _mockGigs.SetSource(new[] { gig });
            var gigs = _repository.GetUpComingGigsByArtists(gig.ArtistId + "-");

            gigs.Should().BeEmpty();
        }


        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsForTheGivenArtistAndIsInTheFuture_ShouldBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };

            _mockGigs.SetSource(new[] { gig });

            var gigs = _repository.GetUpComingGigsByArtists(gig.ArtistId);

            gigs.Should().Contain(gig);

        }

        // This test helped me catch a bug in GetGigsUserAttending() method. 
        // It used to return gigs from the past. 
        [TestMethod]
        public void GetGigsUserAttending_GigIsInThePast_ShouldNotBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(-1) };
            var attendance = new Attendance { Gig = gig, AttendeeId = "1" };

            _mockAttendances.SetSource(new[] { attendance });

            var gigs = _repository.GetGigsUserAttending(attendance.AttendeeId);

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetGigsUserAttending_AttendanceForADifferentUser_ShouldNotBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(1) };
            var attendance = new Attendance { Gig = gig, AttendeeId = "1" };

            _mockAttendances.SetSource(new[] { attendance });

            var gigs = _repository.GetGigsUserAttending(attendance.AttendeeId + "-");

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetGigsUserAttending_UpcomingGigUserAttending_ShouldBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(1) };
            var attendance = new Attendance { Gig = gig, AttendeeId = "1" };

            _mockAttendances.SetSource(new[] { attendance });

            var gigs = _repository.GetGigsUserAttending(attendance.AttendeeId);

            gigs.Should().Contain(gig);
        }

    }
}
