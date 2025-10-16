using FluentAssertions;
using Microsoft.EntityFrameworkCore;
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

namespace Services.Tests.TestCases.DestinationTests
{
    public class Update
    {
        private IDestinationService _sut;
        private Mock<IDestinationRepository> _destRepo;

        [SetUp]
        public void Setup()
        {
            _destRepo = new Mock<IDestinationRepository>();
            _destRepo.Setup(x => x.SaveChanges()).Returns(Task.CompletedTask);
            _sut = new DestinationService(_destRepo.Object, AutoMapperConfig.GetMapper());
        }

        [Test]
        public async Task GivenData_WhenIdIsMissing_ThrowsException()
        {
            var dto = UnitData.GetDestinationDto();
            _destRepo.Setup(x => x.FindOne(It.IsAny<Expression<Func<Destination,bool>>>())).ReturnsAsync((Destination)null);

            await FluentActions.Awaiting(() => _sut.Update(dto))
            .Should()
            .ThrowAsync<CustomValidationException>()
            .WithMessage("Destination can't be found.");
        }

        [Test]
        public async Task GivenData_WhenValid_ReturnExpected()
        {
            var dto = UnitData.GetDestinationDto();
            _destRepo.Setup(x => x.FindOne(It.IsAny<Expression<Func<Destination, bool>>>())).ReturnsAsync(UnitData.GetDestination());

            var result = await _sut.Update(dto);

            result.Should().NotBeNull();
            result.Id.Should().NotBe(0);
            result.Description.Should().Be(result.Description);
            result.Name.Should().Be(result.Name);
        }
    }
}
