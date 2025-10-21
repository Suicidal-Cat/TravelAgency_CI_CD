using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos;
using Models.Enums;
using Services.Interfaces;
using TravelAgency.Helper;

namespace TravelAgency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookingController(IBookingService bookingService, IJwtUserInfo jwtUserInfo) : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Create(AddBookingDto model)
        {
            model.UserId = jwtUserInfo.GetUserId();
            return Ok(await bookingService.Create(model));
        }

        [HttpPost("update-status/{id:long}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update(long id, BookingStatus status)
        {
            await bookingService.Update(id, status);
            return Ok("You have succesfully updated the booking.");
        }

        [HttpGet("user-bookings")]
        public async Task<IActionResult> GetUserBookings()
        {
            return Ok(await bookingService.GetUserBookings(jwtUserInfo.GetUserId()));
        }
    }
}
