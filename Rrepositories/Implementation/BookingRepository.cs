using AutoMapper;
using DataBase;
using Microsoft.EntityFrameworkCore;
using Models.Dtos;
using Models.Entities;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementation
{
    public class BookingRepository(AppDbContext dbContext, IMapper mapper) : BaseRepository<Booking>(dbContext), IBookingRepository
    {
        public async Task<List<BookingDetailsDto>> GetBookingDetails(long userId)
        {
            var bookings = await Query().Include(b => b.Trip).Where(b => b.UserId == userId).ToListAsync();

            return mapper.Map<List<BookingDetailsDto>>(bookings);
        }
    }
}
