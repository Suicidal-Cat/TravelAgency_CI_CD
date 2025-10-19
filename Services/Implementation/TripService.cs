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
    public class TripService(ITripRepository _tripRepository,IEmployeeRepository _employeeRepository, IMapper mapper) : ITripService
    {
        public async Task Create(AddTripDto model)
        {
            if(model.StartDate <= DateTime.UtcNow)
            {
                throw new CustomValidationException("Invalid start date.");
            }

            if (model.EndDate <= DateTime.UtcNow)
            {
                throw new CustomValidationException("Invalid end date.");
            }

            if (model.StartDate > model.EndDate)
            {
                throw new CustomValidationException("End date must be later than the start date.");
            }

            var trip = mapper.Map<Trip>(model);
            if (trip.Employee.Id != 0)
            {
                var employee = await _employeeRepository.FindOne(e => e.Id == model.Employee.Id);

                if (employee == null)
                {
                    throw new CustomValidationException("Employee couldn't be found.");
                } else
                {
                    trip.Employee = employee;
                }
            }

            _tripRepository.Add(trip);
            await _tripRepository.SaveChanges();
        }

        public async Task Update(long id, DateTime startDate, DateTime endDate)
        {
            if (startDate <= DateTime.UtcNow)
            {
                throw new CustomValidationException("Invalid start date.");
            }

            if (endDate <= DateTime.UtcNow)
            {
                throw new CustomValidationException("Invalid end date.");
            }

            if (startDate > endDate)
            {
                throw new CustomValidationException("End date must be later than the start date.");
            }

            var trip = await _tripRepository.FindOne(t => t.Id == id);
            if (trip == null)
            {
                throw new CustomValidationException("Trip couldn't be found.");
            }

            trip.StartDate = startDate;
            trip.EndDate = endDate;
            _tripRepository.Update(trip);
            await _tripRepository.SaveChanges();
        }

        public async Task<PagedResult<TripDto>> GetAllActiveTrips(int pageNumber, int pageSize)
        {
            return await _tripRepository.GetAllActive(pageNumber, pageSize);
        }

        public async Task<TripDto> GetWithDetails(long tripId,long userId)
        {
            var trip = await _tripRepository.GetWithDetails(tripId, userId);
            if (trip == null)
            {
                throw new CustomValidationException("Trip couldn't be found.");
            }
            return trip;
        }
    }
}
