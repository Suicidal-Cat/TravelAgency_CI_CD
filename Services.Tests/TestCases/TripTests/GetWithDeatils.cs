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
    public class GetWithDeatils
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
        public async Task GivenData_TripIsMissing_ThrowsException()
        {
            _tripRep.Setup(x => x.FindOne(It.IsAny<Expression<Func<Trip, bool>>>())).ReturnsAsync((Trip)null);

            await FluentActions.Awaiting(() => _sut.GetWithDetails(1,1))
            .Should()
            .ThrowAsync<CustomValidationException>()
            .WithMessage("Trip couldn't be found.");
        }

        [Test]
        public async Task GivenData_WhenValid_ReturnsExpected()
        {
            var trip = UnitData.GetTripDto(1, DateTime.UtcNow.AddDays(3), DateTime.UtcNow.AddDays(5));
            trip.Should().NotBeNull();
            trip.Reviews.Should().NotBeNull();
            trip.Reviews.Add(UnitData.GetReviewDto(trip.Id, 1));

            _tripRep.Setup(x => x.GetWithDetails(trip.Id, 1)).ReturnsAsync(trip);

            var result = await _sut.GetWithDetails(trip.Id, 1);
           
            result.Should().NotBeNull();
            result.Reviews.Should().NotBeNull();
            result.Reviews.Should().HaveCount(trip.Reviews.Count);
            result.Description.Should().NotBeNull();
        }
    }
}
