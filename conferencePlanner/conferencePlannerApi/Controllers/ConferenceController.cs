using Microsoft.AspNetCore.Mvc;
using conferencePlannerApi.Repositories.Interfaces;
using conferencePlannerCore.Models;

namespace conferencePlannerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConferenceController : ControllerBase
    {
        private readonly IConferenceRepo _repository;

        public ConferenceController(IConferenceRepo repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("GetAllConferences")]
        public async Task<IEnumerable<Conference>> GetAllConferences()
            => await _repository.GetAllAsync();

        [HttpGet]
        [Route("GetConferenceById/{id}")]
        public async Task<ActionResult<Conference>> GetConferenceById(int id)
        {
            var conference = await _repository.GetByIdAsync(id);
            return conference == null ? NotFound() : conference;
        }

        [HttpPost]
        [Route("CreateConference")]
        public async Task<ActionResult<Conference>> CreateConference(Conference conference)
        {
            var newConference = await _repository.CreateAsync(conference);
            return CreatedAtAction(nameof(GetConferenceById), new { id = newConference.Id }, newConference);
        }

        [HttpPut]
        [Route("UpdateConference")]
        public async Task<ActionResult<Conference>> UpdateConference(Conference conference)
        {
            var updatedConference = await _repository.UpdateAsync(conference);
            return updatedConference == null ? NotFound() : updatedConference;
        }

        [HttpDelete]
        [Route("DeleteConference/{id}")]
        public async Task<ActionResult> DeleteConference(int id)
            => await _repository.DeleteAsync(id) ? NoContent() : NotFound();
    }
}