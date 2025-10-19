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
    public class Update
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
            _bookRep.Setup(x => x.FindOne(It.IsAny<Expression<Func<Booking, bool>>>())).ReturnsAsync((Booking)null);

            await FluentActions.Awaiting(() => _sut.Update(1, BookingStatus.Confirmed))
            .Should()
            .ThrowAsync<CustomValidationException>()
            .WithMessage("Booking couldn't be found.");
        }

        [Test]
        public async Task GivenData_WhenValid_VerifyUpdateCall()
        {
            var booking = UnitData.GetBooking(1, 1, 1, 1.0m, BookingStatus.Confirmed);

            _bookRep.Setup(x => x.FindOne(It.IsAny<Expression<Func<Booking, bool>>>())).ReturnsAsync(booking);

            await _sut.Update(booking.Id,booking.Status);

            _bookRep.Verify(x => x.Update(It.Is<Booking>(b => b.TotalPrice == booking.NumberOfPeople * 1.0m && b.Status == booking.Status)), Times.Once);
            _bookRep.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
