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

        /// <summary>
        /// Gets a venue by its ID
        /// </summary>
        /// <param name="id">The ID of the venue to retrieve</param>
        /// <returns>
        /// 200 OK with venue data
        /// 404 Not Found if venue doesn't exist
        /// 500 Internal Server Error if processing fails
        /// </returns>
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

        /// <summary>
        /// Creates a new venue
        /// </summary>
        /// <param name="venue">The venue data to create</param>
        /// <returns>
        /// 201 Created with the new venue data and location header
        /// 400 Bad Request if validation fails
        /// 500 Internal Server Error if processing fails
        /// </returns>
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

        /// <summary>
        /// Updates an existing venue
        /// </summary>
        /// <param name="venue">The venue data to update</param>
        /// <returns>
        /// 200 OK with updated venue data
        /// 400 Bad Request if validation fails
        /// 404 Not Found if venue doesn't exist
        /// 500 Internal Server Error if processing fails
        /// </returns>
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

        /// <summary>
        /// Deletes a venue by its ID
        /// </summary>
        /// <param name="id">The ID of the venue to delete</param>
        /// <returns>
        /// 204 No Content if successful
        /// 404 Not Found if venue doesn't exist
        /// 500 Internal Server Error if processing fails
        /// </returns>
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
