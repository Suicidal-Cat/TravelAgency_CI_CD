using Models.Dtos;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IBookingService
    {
        public Task<AddBookingDto> Create(AddBookingDto model);
        public Task Update(long id, BookingStatus status);
        public Task<List<BookingDetailsDto>> GetUserBookings(long userId);
    }
}
