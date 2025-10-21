using AutoMapper;
using Models.Dtos;
using Models.Entities;
using Repositories.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace Services.Implementation
{
    public class ReviewService(IReviewRepository _reviewRepository, IMapper mapper) : IReviewService
    {
        public async Task<ReviewDto> Create(ReviewDto model)
        {
            model.ReviewDate = DateTime.UtcNow;
            var review = await _reviewRepository.FindOne(r => r.UserId == model.UserId && r.TripId == model.TripId);
            ReviewDto result = null;

            if (review == null)
            {
                result = mapper.Map<ReviewDto>(_reviewRepository.Add(mapper.Map<Review>(model)));
            }
            else
            {
                model.Id = review.Id;
                mapper.Map(model,review);
                result = mapper.Map<ReviewDto>(_reviewRepository.Update(review));
            }
            await _reviewRepository.SaveChanges();

            return result;
        }

        public async Task Delete(long userId, long reviewId)
        {
            var review = await _reviewRepository.FindOne(r => r.UserId == userId && r.Id == reviewId);
            if(review == null)
            {
                throw new CustomValidationException("Couldn't find a review.");
            }

            _reviewRepository.Delete(review);
            await _reviewRepository.SaveChanges();
        }
    }
}
