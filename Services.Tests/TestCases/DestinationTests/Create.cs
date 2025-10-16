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

namespace Services.Tests.TestCases.DestinationTests
{
    public class Create
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
        public async Task GivenData_VerifyCreateCall()
        {
            var dto = UnitData.GetDestinationDto();
            await _sut.Create(dto);

            _destRepo.Verify(x => x.Add(It.Is<Destination>(d => d.Name == dto.Name && d.Description == dto.Description)), Times.Once);
            _destRepo.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
