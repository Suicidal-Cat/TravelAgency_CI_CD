using Models.Dtos;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IDestinationService
    {
        public Task<Destination> Create(DestinationDto destination);
        public Task<Destination> Update(DestinationDto destination);
        public Task Delete(long id);
    }
}
