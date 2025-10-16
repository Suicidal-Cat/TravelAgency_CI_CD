using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    public class DestinationService(IDestinationRepository _destinationRep, IMapper mapper) : IDestinationService
    {
        public async Task<Destination> Create(DestinationDto destination)
        {
            var dest = mapper.Map<Destination>(destination);
            dest = _destinationRep.Add(dest);
            await _destinationRep.SaveChanges();

            return dest;
        }

        public async Task<Destination> Update(DestinationDto destination)
        {
            var dest = await _destinationRep.FindOne(d => d.Id == destination.Id);

            if (dest == null)
            {
                throw new CustomValidationException("Destination can't be found.");
            }
            mapper.Map(destination, dest);
            _destinationRep.Update(dest);
            await _destinationRep.SaveChanges();

            return dest;
        }

        public async Task Delete(long id)
        {
            var dest = await _destinationRep.FindOne(d => d.Id == id);

            if (dest == null)
            {
                throw new CustomValidationException("Destination can't be found.");
            }

            _destinationRep.Delete(dest);
            await _destinationRep.SaveChanges();
        }
    }
}
