using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = new();
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
    }
}
