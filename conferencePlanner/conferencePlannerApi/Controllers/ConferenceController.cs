using Microsoft.AspNetCore.Mvc;
using conferencePlannerApi.Repositories.Interfaces;
using conferencePlannerCore.Models;

namespace conferencePlannerApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ConferenceController : ControllerBase
	{
		private readonly IConferenceRepo _conferenceRepo;

		public ConferenceController(IConferenceRepo repo)
		{
			_conferenceRepo = repo;
		}

        /// <summary>
        /// Retrieves all conferences from the system
        /// </summary>
        /// <returns>200 OK with a collection of all conferences</returns>
        [HttpGet]
        [Route("GetAllConferences")]
        public async Task<ActionResult<IEnumerable<Conference>>> GetAllConferences()
        {
            var conferences = await _conferenceRepo.GetAllAsync();
            return Ok(conferences);
        }

        /// <summary>
        /// Retrieves all active conferences from the system
        /// </summary>
        /// <returns>200 OK with a collection of active conferences</returns>
        [HttpGet]
        [Route("GetActiveConferences")]
        public async Task<ActionResult<IEnumerable<Conference>>> GetActiveConferences()
        {
            var conferences = await _conferenceRepo.GetAllActiveAsync();
            return Ok(conferences);
        }

        /// <summary>
        /// Retrieves a specific conference by its unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the conference to retrieve</param>
        /// <returns>
        /// 200 OK with the conference if found
        /// 404 Not Found if the conference doesn't exist
        /// </returns>
        [HttpGet]
        [Route("GetConferenceById/{id}")]
        public async Task<ActionResult<Conference>> GetConferenceById(int id)
        {
            var conference = await _conferenceRepo.GetByIdAsync(id);
            return conference == null ? NotFound($"Conference with ID {id} not found") : Ok(conference);
        }

        /// <summary>
        /// Creates a new conference in the system
        /// </summary>
        /// <param name="conference">The conference object to create</param>
        /// <returns>
        /// 201 Created with the new conference and location header
        /// 400 Bad Request if the model state is invalid
        /// 500 Internal Server Error if an error occurs during creation
        /// </returns>
        [HttpPost]
        [Route("CreateConference")]
        public async Task<ActionResult<Conference>> CreateConference(Conference conference)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var newConference = await _conferenceRepo.CreateAsync(conference);
                return CreatedAtAction(nameof(GetConferenceById),
                    new { id = newConference!.Id },
                    newConference);
            }
            catch
            {
                return StatusCode(500, "An error occurred while creating the conference");
            }
        }

        /// <summary>
        /// Updates an existing conference in the system
        /// </summary>
        /// <param name="conference">The conference object with updated values</param>
        /// <returns>
        /// 200 OK with the updated conference
        /// 400 Bad Request if the model state is invalid
        /// 404 Not Found if the conference doesn't exist
        /// 500 Internal Server Error if an error occurs during update
        /// </returns>
        [HttpPut]
        [Route("UpdateConference")]
        public async Task<ActionResult<Conference>> UpdateConference(Conference conference)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var updatedConference = await _conferenceRepo.UpdateAsync(conference);
                return updatedConference == null
                    ? NotFound($"Conference with ID {conference.Id} not found")
                    : Ok(updatedConference);
            }
            catch
            {
                return StatusCode(500, "An error occurred while updating the conference");
            }
        }

        /// <summary>
        /// Deletes a conference from the system by its ID
        /// </summary>
        /// <param name="id">The unique identifier of the conference to delete</param>
        /// <returns>
        /// 204 No Content if deletion is successful
        /// 404 Not Found if the conference doesn't exist
        /// 500 Internal Server Error if an error occurs during deletion
        /// </returns>
        [HttpDelete]
        [Route("DeleteConference/{id}")]
        public async Task<ActionResult> DeleteConference(int id)
        {
            try
            {
                var result = await _conferenceRepo.DeleteAsync(id);
                return result ? NoContent() : NotFound($"Conference with ID {id} not found");
            }
            catch
            {
                return StatusCode(500, "An error occurred while deleting the conference");
            }
        }

        /// <summary>
        /// Retrieves all criteria associated with a specific conference
        /// </summary>
        /// <param name="id">The unique identifier of the conference</param>
        /// <returns>
        /// 200 OK with the list of criteria
        /// 204 No Content if no criteria are found
        /// 500 Internal Server Error if an error occurs during retrieval
        /// </returns>
        [HttpGet]
        [Route("AllCriteria/{id}")]
        public async Task<ActionResult<IEnumerable<string>>> GetConferenceCriteria(int id)
        {
            try
            {
                List<string> result = await _conferenceRepo.ListAllCriteria(id);
                return result != null ? Ok(result) : NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Retrieves all categories associated with a specific conference
        /// </summary>
        /// <param name="id">The unique identifier of the conference</param>
        /// <returns>
        /// 200 OK with the list of categories
        /// 204 No Content if no categories are found
        /// 500 Internal Server Error if an error occurs during retrieval
        /// </returns>
        [HttpGet]
        [Route("AllCategories/{id}")]
        public async Task<ActionResult<IEnumerable<string>>> GetConferenceCategories(int id)
        {
            try
            {
                List<string> result = await _conferenceRepo.ListAllCategories(id);
                return result != null ? Ok(result) : NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}