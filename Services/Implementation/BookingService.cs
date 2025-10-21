using AutoMapper;
using Models.Dtos;
using Models.Entities;
using Models.Enums;
using Repositories.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace Services.Implementation
{
    public class BookingService(IBookingRepository _bookingRepository, ITripRepository _tripRepository, IMapper mapper) : IBookingService
    {
        public async Task<AddBookingDto> Create(AddBookingDto model)
        {
            var trip = await _tripRepository.FindOne(t => t.Id == model.TripId);

            if (trip == null)
            {
                throw new CustomValidationException("Trip couldn't be found.");
            }

            if(trip.StartDate < DateTime.UtcNow.Date)
            {
                throw new CustomValidationException("Trip has already started.");
            }

            var booking = await _bookingRepository.FindOne(b => b.TripId == model.TripId && b.UserId == model.UserId && (b.Status == BookingStatus.Pending || b.Status == BookingStatus.Confirmed));

            if(booking != null)
            {
                throw new CustomValidationException("You already have an active reservation for this trip.");
            }

            model.Status = BookingStatus.Pending;
            model.TotalPrice = model.NumberOfPeople * trip.Price;
            model.BookingDate = DateTime.UtcNow.Date;

            var result = _bookingRepository.Add(mapper.Map<Booking>(model));
            await _bookingRepository.SaveChanges();

            return mapper.Map<AddBookingDto>(result);
        }

        public async Task Update(long id, BookingStatus status)
        {
            var booking = await _bookingRepository.FindOne(b => b.Id == id);
            if (booking == null)
            {
                throw new CustomValidationException("Booking couldn't be found.");
            }

            booking.Status = status;
            _bookingRepository.Update(booking);
            await _bookingRepository.SaveChanges();
        }

        public async Task<List<BookingDetailsDto>> GetUserBookings(long userId)
        {
            return await _bookingRepository.GetBookingDetails(userId);
        }
    }
}
