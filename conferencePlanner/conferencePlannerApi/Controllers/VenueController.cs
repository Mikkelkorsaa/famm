using conferencePlannerApi.Repositories.Interfaces;
using conferencePlannerCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace conferencePlannerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VenueController : ControllerBase
    {
       private readonly IVenueRepo _repo;
            
          public VenueController(IVenueRepo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        [Route("GetConferenceById/{id}")]
        public async Task<ActionResult<Conference>> GetVenueById(int id)
        {
            try
            {
                var venue = await _repo.GetByIdAsync(id);
                return venue == null ? NotFound($"Venue with ID {id} not found") : Ok(venue);
            }
            catch
            {
                return StatusCode(500, "An error occurred while retrieving the conference");
            }
        }


    }
}
