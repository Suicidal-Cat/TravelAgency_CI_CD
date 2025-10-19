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
using System.Text;
using System.Threading.Tasks;

namespace Services.Tests.TestCases.TripTests
{
    public class GetAllActive
    {
        private ITripService _sut;
        private Mock<ITripRepository> _tripRep;

        [SetUp]
        public void Setup()
        {
            _tripRep = new Mock<ITripRepository>();
            _sut = new TripService(_tripRep.Object, (IEmployeeRepository)null, AutoMapperConfig.GetMapper());
        }

        [Test]
        public async Task GivenData_ReturnsExpected()
        {
            var trips = new List<TripDto>() {
                UnitData.GetTripDto(1,DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(3)),
                UnitData.GetTripDto(2,DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(3))
            };

            _tripRep.Setup(x => x.GetAllActive(1, 1)).ReturnsAsync(new PagedResult<TripDto>
            {
                Items = trips,
                PageNumber = 1,
                TotalPages = 1
            });

            var result = await _sut.GetAllActiveTrips(1, 1);

            result.Should().NotBeNull();
            result.Items.Should().NotBeNullOrEmpty();
            result.Items.Should().HaveCount(trips.Count);
            result.PageNumber = 1;
            result.TotalPages = 1;
        }
    }
}
