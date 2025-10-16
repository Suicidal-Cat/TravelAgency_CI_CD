using Models.Dtos;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Tests.TestData
{
    public static class UnitData
    {
        public static Destination GetDestination()
        {
            return new Destination()
            {
                Id = 1,
                Country = "Serbia",
                Description = "Description2",
                Name = "Name2",
            };
        }

        public static DestinationDto GetDestinationDto()
        {
            return new DestinationDto()
            {
                Id = 1,
                Country = "Serbia",
                Description = "Description1",
                Name = "Name1",
            };
        }
    }
}
