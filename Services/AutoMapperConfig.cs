using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class AutoMapperConfig
    {
        public static IMapper GetMapper()
        {
            MapperConfiguration config = new MapperConfiguration(
                   c =>
                   {
                       c.AddProfile<MappingProfile>();
                   });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }
    }
}
