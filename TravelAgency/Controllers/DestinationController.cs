using Microsoft.AspNetCore.Mvc;
using Models.Dtos;
using Services.Interfaces;

namespace TravelAgency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DestinationController(IDestinationService destinationService) : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Create(DestinationDto destination)
        {
            await destinationService.Create(destination);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(DestinationDto destination)
        {
            await destinationService.Update(destination);
            return Ok();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            await destinationService.Delete(id);
            return Ok();
        }
    }
}
