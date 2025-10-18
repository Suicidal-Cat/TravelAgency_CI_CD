using Humanizer;
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

namespace Services.Tests.TestCases.ReviewTests
{
    public class Create
    {
        private IReviewService _sut;
        private Mock<IReviewRepository> _revRep;

        [SetUp]
        public void Setup()
        {
            _revRep = new Mock<IReviewRepository>();
            _revRep.Setup(x => x.SaveChanges()).Returns(Task.CompletedTask);
            _sut = new ReviewService(_revRep.Object, AutoMapperConfig.GetMapper());
        }

        [Test]
        public async Task GivenData_VerifyCreateCall()
        {
            var review = UnitData.GetReviewDto(1,1);
            _revRep.Setup(x => x.FindOne(It.IsAny<Expression<Func<Review, bool>>>())).ReturnsAsync((Review)null);

            await _sut.Create(review);

            _revRep.Verify(x => x.Add(It.Is<Review>(r => r.Comment == review.Comment && r.Rating == review.Rating)), Times.Once);
            _revRep.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Test]
        public async Task GivenData_VerifyUpdateCall()
        {
            var review = UnitData.GetReviewDto(1, 1);
            var existingReview = UnitData.GetReview(1, 1, 1);
            _revRep.Setup(x => x.FindOne(It.IsAny<Expression<Func<Review, bool>>>())).ReturnsAsync(existingReview);

            await _sut.Create(review);

            _revRep.Verify(x => x.Update(It.Is<Review>(r => r.Comment == review.Comment && r.Rating == review.Rating && r.Id == existingReview.Id)), Times.Once);
            _revRep.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
