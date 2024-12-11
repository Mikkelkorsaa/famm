using Microsoft.AspNetCore.Mvc;
using conferencePlannerApi.Repositories.Interfaces;
using conferencePlannerCore.Models;

namespace conferencePlannerApi.Controllers
{
   [ApiController]
   [Route("api/[controller]")]
   public class ConferenceController : ControllerBase
   {
       private readonly IConferenceRepo _repo;

       public ConferenceController(IConferenceRepo repo)
       {
           _repo = repo;
       }

       [HttpGet]
       [Route("GetAllConferences")]
       public async Task<ActionResult<IEnumerable<Conference>>> GetAllConferences()
       {
           try
           {
               var conferences = await _repo.GetAllAsync();
               return Ok(conferences);
           }
           catch
           {
               return StatusCode(500, "An error occurred while retrieving conferences");
           }
       }

       [HttpGet]
       [Route("GetConferenceById/{id}")]
       public async Task<ActionResult<Conference>> GetConferenceById(int id)
       {
           try
           {
               var conference = await _repo.GetByIdAsync(id);
               return conference == null ? NotFound($"Conference with ID {id} not found") : Ok(conference);
           }
           catch
           {
               return StatusCode(500, "An error occurred while retrieving the conference");
           }
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

               var newConference = await _repo.CreateAsync(conference);
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

               var updatedConference = await _repo.UpdateAsync(conference);
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
               var result = await _repo.DeleteAsync(id);
               return result ? NoContent() : NotFound($"Conference with ID {id} not found");
           }
           catch
           {
               return StatusCode(500, "An error occurred while deleting the conference");
           }
       }
   }
}