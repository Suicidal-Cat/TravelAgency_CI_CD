using FluentAssertions;
using Models.Dtos;
using Models.Entities;
using Models.Enums;
using Moq;
using NUnit.Framework;
using Repositories.Interfaces;
using Services.Implementation;
using Services.Interfaces;
using Services.Tests.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace Services.Tests.TestCases.BookingTests
{
    public class Create
    {
        private IBookingService _sut;
        private Mock<IBookingRepository> _bookRep;
        private Mock<ITripRepository> _tripRep;

        [SetUp]
        public void Setup()
        {
            _bookRep = new Mock<IBookingRepository>();
            _tripRep = new Mock<ITripRepository>();
            _bookRep.Setup(x => x.SaveChanges()).Returns(Task.CompletedTask);
            _sut = new BookingService(_bookRep.Object, _tripRep.Object, AutoMapperConfig.GetMapper());
        }

        [Test]
        public async Task GivenData_WhenTripIsMissing_ThrowsException()
        {
            var model = new AddBookingDto()
            {
                TripId = 0
            };

            _tripRep.Setup(x => x.FindOne(It.IsAny<Expression<Func<Trip, bool>>>())).ReturnsAsync((Trip)null);

            await FluentActions.Awaiting(() => _sut.Create(model))
            .Should()
            .ThrowAsync<CustomValidationException>()
            .WithMessage("Trip couldn't be found.");
        }

        [Test]
        public async Task GivenData_WhenTripStarted_ThrowsException()
        {
            var model = new AddBookingDto()
            {
                TripId = 1,
            };

            var trip = UnitData.GetTrip(DateTime.UtcNow.AddDays(-1), DateTime.UtcNow);

            _tripRep.Setup(x => x.FindOne(It.IsAny<Expression<Func<Trip, bool>>>())).ReturnsAsync(trip);

            await FluentActions.Awaiting(() => _sut.Create(model))
            .Should()
            .ThrowAsync<CustomValidationException>()
            .WithMessage("Trip has already started.");
        }

        [Test]
        public async Task GivenData_WhenReservationIsActive_ThrowsException()
        {
            var trip = UnitData.GetTrip(DateTime.UtcNow.AddDays(3), DateTime.UtcNow.AddDays(5));
            var booking = UnitData.GetBooking(1, trip.Id, 1, trip.Price, BookingStatus.Confirmed);

            var model = new AddBookingDto()
            {
                TripId = trip.Id,
                UserId = 1,
                NumberOfPeople = 1,
            };

            _tripRep.Setup(x => x.FindOne(It.IsAny<Expression<Func<Trip, bool>>>())).ReturnsAsync(trip);

            _bookRep.Setup(x => x.FindOne(It.IsAny<Expression<Func<Booking, bool>>>())).ReturnsAsync(booking);

            await FluentActions.Awaiting(() => _sut.Create(model))
            .Should()
            .ThrowAsync<CustomValidationException>()
            .WithMessage("You already have an active reservation for this trip.");
        }

        [Test]
        public async Task GivenData_WhenValid_VerifyAddCall()
        {
            var trip = UnitData.GetTrip(DateTime.UtcNow.AddDays(3), DateTime.UtcNow.AddDays(5));
            var model = new AddBookingDto()
            {
                TripId = trip.Id,
                UserId = 1,
                NumberOfPeople = 1,
                BookingDate = DateTime.UtcNow,
            };

            _tripRep.Setup(x => x.FindOne(It.IsAny<Expression<Func<Trip, bool>>>())).ReturnsAsync(trip);
            _bookRep.Setup(x => x.FindOne(It.IsAny<Expression<Func<Booking, bool>>>())).ReturnsAsync((Booking)null);

            await _sut.Create(model);

            _bookRep.Verify(x => x.Add(It.Is<Booking>(b => b.TotalPrice == model.NumberOfPeople * trip.Price && b.Status == BookingStatus.Pending)), Times.Once);
            _bookRep.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
