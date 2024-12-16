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
        [Route("GetVenueById/{id}")]
        public async Task<ActionResult<Venue>> GetVenueById(int id)
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

        [HttpPost]
        [Route("CreateVenue")]
        public async Task<ActionResult<Venue>> CreateVenue(Venue venue)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newVenue = await _repo.CreateAsync(venue);
                return CreatedAtAction(nameof(GetVenueById),
                    new { id = venue!.Id },
                    newVenue);
            }
            catch
            {
                return StatusCode(500, "An error occurred while creating the venue");
            }
        }

        [HttpPut]
        [Route("UpdateVenue")]
        public async Task<ActionResult<Venue>> UpdateVenue(Venue venue)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var updatedVenue = await _repo.UpdateAsync(venue);
                return updatedVenue == null
                    ? NotFound($"Venue with ID {venue.Id} not found")
                    : Ok(updatedVenue);
            }
            catch
            {
                return StatusCode(500, "An error occurred while updating the venue");
            }
        }

        [HttpDelete]
        [Route("DeleteVenue/{id}")]
        public async Task<ActionResult> DeleteVenue(int id)
        {
            try
            {
                var result = await _repo.DeleteAsync(id);
                return result ? NoContent() : NotFound($"Venue with ID {id} not found");
            }
            catch
            {
                return StatusCode(500, "An error occurred while deleting the venue");
            }
        }

    }
}
