using Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IReviewService
    {
        public Task Create(ReviewDto model);
        public Task Delete(long userId, long reviewId);
    }
}
