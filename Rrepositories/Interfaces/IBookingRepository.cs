using Models.Dtos;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IBookingRepository : IBaseRepository<Booking>
    {
        public Task<List<BookingDetailsDto>> GetBookingDetails(long userId);
    }
}
