using FluentAssertions;
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

namespace Services.Tests.TestCases.ReviewTests
{
    public class Delete
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
        public async Task GivenData_WhenReviewIsMissing_ThrowsException()
        {
            var review = UnitData.GetReviewDto(1, 1);
            _revRep.Setup(x => x.FindOne(It.IsAny<Expression<Func<Review, bool>>>())).ReturnsAsync((Review)null);


            await FluentActions.Awaiting(() => _sut.Delete(1, review.Id))
            .Should()
            .ThrowAsync<CustomValidationException>()
            .WithMessage("Couldn't find a review.");
        }

        [Test]
        public async Task GivenData_VerifyDeleteCall()
        {
            var review = UnitData.GetReview(1, 1, 1);
            _revRep.Setup(x => x.FindOne(It.IsAny<Expression<Func<Review, bool>>>())).ReturnsAsync(review);

            await _sut.Delete(review.UserId, review.Id);

            _revRep.Verify(x => x.Delete(It.Is<Review>(r => r.Id == review.Id && r.Comment == review.Comment)), Times.Once);
            _revRep.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
