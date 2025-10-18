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
        public async Task<List<TripDto>?> GetAllActive()
        {
            var trips = await Query().Include(t => t.Employee).Include(t => t.Destination).Include(t => t.Reviews).Where(t => t.StartDate >= DateTime.UtcNow).ToListAsync();

            return mapper.Map<List<TripDto>>(trips);
        }
    }
}
