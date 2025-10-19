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
    public class Create
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
            _sut = new TripService(_tripRep.Object,_empRep.Object, AutoMapperConfig.GetMapper());
        }

        [Test]
        public async Task GivenData_InvalidStartDate_ThrowsException()
        {
            var model = new AddTripDto()
            {
                StartDate = DateTime.Now.AddDays(-1)
            };

            await FluentActions.Awaiting(() => _sut.Create(model))
            .Should()
            .ThrowAsync<CustomValidationException>()
            .WithMessage("Invalid start date.");
        }

        [Test]
        public async Task GivenData_InvalidEndDate_ThrowsException()
        {
            var model = new AddTripDto()
            {
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(-1),
            };

            await FluentActions.Awaiting(() => _sut.Create(model))
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

            await FluentActions.Awaiting(() => _sut.Create(model))
            .Should()
            .ThrowAsync<CustomValidationException>()
            .WithMessage("End date must be later than the start date.");
        }

        [Test]
        public async Task GivenData_EmployeeIsMissing_ThrowsException()
        {
            var model = new AddTripDto()
            {
                StartDate = DateTime.Now.AddDays(3),
                EndDate = DateTime.Now.AddDays(10),
                Employee = new EmployeeDto()
                {
                    Id = 1
                }
            };

            _empRep.Setup(x => x.FindOne(It.IsAny<Expression<Func<Employee, bool>>>())).ReturnsAsync((Employee)null);

            await FluentActions.Awaiting(() => _sut.Create(model))
            .Should()
            .ThrowAsync<CustomValidationException>()
            .WithMessage("Employee couldn't be found.");
        }

        [Test]
        public async Task GivenData_WhenValid_VerifyAddCall()
        {
            var model = new AddTripDto()
            {
                StartDate = DateTime.Now.AddDays(3),
                EndDate = DateTime.Now.AddDays(10),
                Employee = new EmployeeDto()
                {
                    Id = 1,
                    FirstName = "Test",
                },
                Description = "Test",
                DestinationId = 1,
                Price = 2.0m
            };

            var emp = UnitData.GetEmployee();
            _empRep.Setup(x => x.FindOne(It.IsAny<Expression<Func<Employee, bool>>>())).ReturnsAsync(emp);

            await _sut.Create(model);

            _tripRep.Verify(x => x.Add(It.Is<Trip>(d => d.Price == model.Price && d.Description == model.Description && d.Employee.Id == emp.Id)), Times.Once);
            _tripRep.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
