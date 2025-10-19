using FluentAssertions;
using Models.Dtos;
using Models.Entities;
using Moq;
using NUnit.Framework;
using Repositories.Interfaces;
using Services.Implementation;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace Services.Tests.TestCases.BookingTests
{
    public class GetUserBookings
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
        public async Task GivenData_WhenBookingIsMissing_ThrowsException()
        {
            var bookings = new List<BookingDetailsDto>()
            {
                new BookingDetailsDto
                {
                    Id = 1,
                    UserId = 1,
                    TripDescription = "Test",
                },
                new BookingDetailsDto
                {
                    Id = 2,
                    UserId = 2,
                    TripDescription = "Test2",
                },
            };

            _bookRep.Setup(x => x.GetBookingDetails(1)).ReturnsAsync(bookings);
            var result = await _sut.GetUserBookings(1);

            result.Should().NotBeNull();
            result.Should().HaveCount(bookings.Count);
        }
    }
}
