using FluentAssertions;
using Models.Entities;
using Moq;
using NUnit.Framework;
using Repositories.Interfaces;
using Services.Implementation;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Tests.TestCases.UserTests
{
    public class GetByUsername
    {
        private IUserService _sut;
        private Mock<IUserRepository> _userRep;

        [SetUp]
        public void Setup()
        {
            _userRep = new Mock<IUserRepository>();
            _userRep.Setup(x => x.SaveChanges()).Returns(Task.CompletedTask);
            _sut = new UserService(_userRep.Object, AutoMapperConfig.GetMapper());
        }

        [Test]
        public async Task GivenData_ReturnsExpected()
        {
            var user = new User
            {
                Username = "Test",
                Password = "password"
            };

            _userRep.Setup(x => x.FindOne(It.IsAny<Expression<Func<User,bool>>>())).ReturnsAsync(user);

            var result = await _sut.GetByUsername(user.Username);

            result.Should().NotBeNull();
            result.Username.Should().Be(user.Username);
            result.Password.Should().Be(user.Password);
        }
    }
}
