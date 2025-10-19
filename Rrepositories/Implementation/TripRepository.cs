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
    public class TripRepository(AppDbContext dbContext, IMapper mapper) : BaseRepository<Trip>(dbContext), ITripRepository
    {
        public async Task<PagedResult<TripDto>> GetAllActive(int pageNumber, int pageSize)
        {
            var query = Query()
                        .Include(t => t.Employee)
                        .Include(t => t.Destination)
                        .Where(t => t.StartDate >= DateTime.UtcNow);

            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var trips = await query
                .OrderBy(t => t.StartDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var mappedTrips = mapper.Map<List<TripDto>>(trips);

            return new PagedResult<TripDto>
            {
                Items = mappedTrips,
                TotalPages = totalPages,
                PageNumber = pageNumber
            };
        }

        public async Task<TripDto> GetWithDetails(long tripId, long userId)
        {
            var trip = await Query()
                .Include(t => t.Employee)
                .Include(t => t.Destination)
                .Include(t => t.Reviews)
                .Include(t => t.Bookings.Where(b => b.UserId == userId && b.TripId == tripId))
                .Where(t => t.Id == tripId)
                .FirstOrDefaultAsync();

            return mapper.Map<TripDto>(trip);
        }
    }
}
