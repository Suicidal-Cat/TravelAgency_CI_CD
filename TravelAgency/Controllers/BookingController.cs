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
            await bookingService.Create(model);
            return Ok("You have succesfully booked a trip.");
        }

        [HttpPost("update-status/{id:long}")]
        public async Task<IActionResult> Create(long id, BookingStatus status)
        {
            await bookingService.Update(id, status);
            return Ok("You have succesfully updated the booking.");
        }
    }
}
