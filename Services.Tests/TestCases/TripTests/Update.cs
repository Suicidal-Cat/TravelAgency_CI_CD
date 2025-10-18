using FluentAssertions;
using Models.Dtos;
using Models.Entities;
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

namespace Services.Tests.TestCases.TripTests
{
    public class Update
    {
        private ITripService _sut;
        private Mock<ITripRepository> _tripRep;
        private Mock<IEmployeeRepository> _empRep;

        [SetUp]
        public void Setup()
        {
            _tripRep = new Mock<ITripRepository>();
            _empRep = new Mock<IEmployeeRepository>();
            _tripRep.Setup(x => x.SaveChanges()).Returns(Task.CompletedTask);
            _sut = new TripService(_tripRep.Object, _empRep.Object, AutoMapperConfig.GetMapper());
        }

        [Test]
        public async Task GivenData_InvalidStartDate_ThrowsException()
        {
            await FluentActions.Awaiting(() => _sut.Update(1,DateTime.Now.AddDays(-1),DateTime.Now))
            .Should()
            .ThrowAsync<CustomValidationException>()
            .WithMessage("Invalid start date.");
        }

        [Test]
        public async Task GivenData_InvalidEndDate_ThrowsException()
        {
            await FluentActions.Awaiting(() => _sut.Update(1, DateTime.Now.AddDays(1), DateTime.Now.AddDays(-1)))
            .Should()
            .ThrowAsync<CustomValidationException>()
            .WithMessage("Invalid end date.");
        }

        [Test]
        public async Task GivenData_InvalidTripDate_ThrowsException()
        {
            var model = new AddTripDto()
            {
                StartDate = DateTime.Now.AddDays(3),
                EndDate = DateTime.Now.AddDays(2)
            };

            await FluentActions.Awaiting(() => _sut.Update(1, DateTime.Now.AddDays(3), DateTime.Now.AddDays(2)))
            .Should()
            .ThrowAsync<CustomValidationException>()
            .WithMessage("End date must be later than the start date.");
        }

        [Test]
        public async Task GivenData_TripIsMissing_ThrowsException()
        {
            _tripRep.Setup(x => x.FindOne(It.IsAny<Expression<Func<Trip, bool>>>())).ReturnsAsync((Trip)null);

            await FluentActions.Awaiting(() => _sut.Update(1, DateTime.Now.AddDays(3), DateTime.Now.AddDays(5)))
            .Should()
            .ThrowAsync<CustomValidationException>()
            .WithMessage("Trip couldn't be found.");
        }

        [Test]
        public async Task GivenData_WhenValid_VerifyUpdateCall()
        {

            var trip = UnitData.GetTrip();
            _tripRep.Setup(x => x.FindOne(It.IsAny<Expression<Func<Trip, bool>>>())).ReturnsAsync(trip);

            await _sut.Update(trip.Id, trip.StartDate, trip.EndDate);

            _tripRep.Verify(x => x.Update(It.Is<Trip>(t => t.StartDate == trip.StartDate && t.EndDate == t.EndDate)), Times.Once);
            _tripRep.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
