using Models.Dtos;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ITripService
    {
        public Task Create(AddTripDto model);
        public Task Update(long id, DateTime startDate, DateTime endDate);
        public Task<PagedResult<TripDto>> GetAllActiveTrips(int pageNumber, int pageSize);
        public Task<TripDto> GetWithDetails(long tripId, long userId);
    }
}
