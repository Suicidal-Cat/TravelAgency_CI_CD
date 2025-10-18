using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos;
using Services.Interfaces;

namespace TravelAgency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class DestinationController(IDestinationService destinationService) : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Create(DestinationDto destination)
        {
            await destinationService.Create(destination);
            return Ok("Succesfully created a destination.");
        }

        [HttpPut]
        public async Task<IActionResult> Update(DestinationDto destination)
        {
            var result = await destinationService.Update(destination);
            return Ok(result);
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            await destinationService.Delete(id);
            return Ok("Succesfully deleted a destination.");
        }
    }
}
