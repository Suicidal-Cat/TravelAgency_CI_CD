using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos;
using Services.Interfaces;

namespace TravelAgency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripController(ITripService tripService) : Controller
    {
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create(AddTripDto model)
        {
           await tripService.Create(model);
           return Ok();
        }

        [HttpPost("{id:long}/change-time")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update([FromRoute] long id,DateTime startDate, DateTime endDate)
        {
            await tripService.Update(id, startDate, endDate);
            return Ok();
        }

        [HttpGet("active")]
        [Authorize]
        public async Task<IActionResult> GetAllActive()
        {
            var result = await tripService.GetAllActiveTrips();
            return Ok(result);
        }
    }
}
