using Models.Dtos;
using Models.Entities;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Tests.TestData
{
    public static class UnitData
    {
        public static Destination GetDestination()
        {
            return new Destination()
            {
                Id = 1,
                Country = "Serbia",
                Description = "Description2",
                Name = "Name2",
            };
        }

        public static DestinationDto GetDestinationDto()
        {
            return new DestinationDto()
            {
                Id = 1,
                Country = "Serbia",
                Description = "Description1",
                Name = "Name1",
            };
        }

        public static Employee GetEmployee()
        {
            return new Employee()
            {
                Id = 1,
                FirstName = "Emp1",
                LastName = "Em2",
                PhoneNumber = "1234567890",
            };
        }

        public static Trip GetTrip(DateTime? startDate = null, DateTime? endDate = null)
        {
            return new Trip()
            {
                Id = 1,
                Description = "test",
                Destination = GetDestination(),
                Employee = GetEmployee(),
                Reviews = new List<Review>(),
                StartDate = startDate != null ? (DateTime)startDate : DateTime.Now.AddDays(3),
                EndDate = endDate != null ? (DateTime) endDate : DateTime.Now.AddDays(5),
                Price = 20.0M,
            };
        }

        public static TripDto GetTripDto(long id, DateTime startDate, DateTime endDate)
        {
            return new TripDto()
            {
                Id = id,
                Description = "test",
                Destination = GetDestinationDto(),
                Employee = new EmployeeDto
                {
                    Id = 1,
                    FirstName = "emp1"
                },
                Reviews = new List<ReviewDto>(),
                StartDate = startDate,
                EndDate = endDate
            };
        }

        public static Review GetReview(long id, long tripId, long userId)
        {
            return new Review()
            {
                Id = id,
                Comment = "test" + id,
                TripId = tripId,
                UserId = userId,
                Rating = 5,
                ReviewDate = DateTime.Now,
            };
        }

        public static ReviewDto GetReviewDto(long tripId, long userId)
        {
            return new ReviewDto()
            {
                Comment = "test" + tripId,
                TripId = tripId,
                UserId = userId,
                Rating = 5,
                ReviewDate = DateTime.Now,
            };
        }

        public static Booking GetBooking(long id, long tripId, long userId, decimal price, BookingStatus status)
        {
            return new Booking()
            {
                Id = id,
                BookingDate = DateTime.Now,
                NumberOfPeople = 2,
                TotalPrice = 2 * price,
                Status = status,
                TripId = tripId,
                UserId = userId,
            };
        }
    }
}
