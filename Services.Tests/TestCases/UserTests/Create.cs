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

namespace Services.Tests.TestCases.UserTests
{
    public class Create
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
        public async Task GivenData_VerifyCreateCall()
        {
            var dto = new RegisterDto()
            {
                Email = "test@test.rs",
                Password = "password",
                Username = "username",
            };

            await _sut.Create(dto);

            _userRep.Verify(x => x.Add(It.Is<User>(u => u.Username == u.Username && u.Password == dto.Password)), Times.Once);
            _userRep.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
