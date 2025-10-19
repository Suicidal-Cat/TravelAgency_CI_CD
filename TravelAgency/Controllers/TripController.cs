using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos;
using Services.Interfaces;
using TravelAgency.Helper;

namespace TravelAgency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripController(ITripService tripService, IJwtUserInfo jwtUserInfo) : Controller
    {
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create(AddTripDto model)
        {
           await tripService.Create(model);
           return Ok("Succesfully create a trip.");
        }

        [HttpPost("{id:long}/change-time")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update([FromRoute] long id,DateTime startDate, DateTime endDate)
        {
            await tripService.Update(id, startDate, endDate);
            return Ok("You have succesfully updated the trip.");
        }

        [HttpGet("active")]
        [Authorize]
        public async Task<IActionResult> GetAllActive([FromQuery] int pageNumber = 1)
        {
            const int pageSize = 15;

            var result = await tripService.GetAllActiveTrips(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id:long}")]
        [Authorize]
        public async Task<IActionResult> GetWithDetails([FromRoute] long id)
        {
            return Ok(await tripService.GetWithDetails(id, jwtUserInfo.GetUserId()));
        }
    }
}
