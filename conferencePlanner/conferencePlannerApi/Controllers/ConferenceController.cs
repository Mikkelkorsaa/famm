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

		[HttpGet]
		[Route("GetAllConferences")]
		public async Task<ActionResult<IEnumerable<Conference>>> GetAllConferences()
		{
			var conferences = await _conferenceRepo.GetAllAsync();
			return Ok(conferences);
		}
		[HttpGet]
		[Route("GetActiveConferences")]
		public async Task<ActionResult<IEnumerable<Conference>>> GetActiveConferences()
		{
			var conferences = await _conferenceRepo.GetAllActiveAsync();
			return Ok(conferences);
		}

		[HttpGet]
		[Route("GetConferenceById/{id}")]
		public async Task<ActionResult<Conference>> GetConferenceById(int id)
		{
			var conference = await _conferenceRepo.GetByIdAsync(id);
			return conference == null ? NotFound($"Conference with ID {id} not found") : Ok(conference);
		}

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