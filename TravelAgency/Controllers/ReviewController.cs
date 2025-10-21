using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos;
using Services.Interfaces;
using TravelAgency.Helper;

namespace TravelAgency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReviewController(IReviewService reviewService, IJwtUserInfo jwtUserInfo) : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Create(ReviewDto model)
        {
            model.UserId = jwtUserInfo.GetUserId();
            return Ok(await reviewService.Create(model));
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            await reviewService.Delete(jwtUserInfo.GetUserId(), id);
            return Ok("Succesfully delete a review.");
        }
    }
}
