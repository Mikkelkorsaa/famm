using conferencePlannerApi.Repositories.Interfaces;
using conferencePlannerCore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace conferencePlannerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AbstractController : ControllerBase
    {
        private readonly IAbstractRepo _repo;
        private readonly IConferenceRepo _conferenceRepo;

        public AbstractController(IAbstractRepo repo, IConferenceRepo conferenceRepo)
        {
            _repo = repo;
            _conferenceRepo = conferenceRepo;
        }

        /// <summary>
        /// Retrieves all Abstract objects from the database
        /// </summary>
        /// <returns>
        /// 200 OK with IEnumerable of Abstract objects
        /// 500 Internal Server Error if an exception occurs
        /// </returns>
        [HttpGet]
        [Route("GetAllAbstracts")]
        public async Task<ActionResult<IEnumerable<Abstract>>> GetAllAbstracts()
        {
            try
            {
                var abstracts = await _repo.GetAllAsync();
                return Ok(abstracts);
            }
            catch
            {
                return StatusCode(500, "An error occurred while retrieving abstracts");
            }
        }


        /// <summary>
        /// Retrieves an abstract by its unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the abstract to retrieve</param>
        /// <returns>
        /// 200 OK with the abstract if found
        /// 404 Not Found if the abstract doesn't exist
        /// 500 Internal Server Error if an error occurs during retrieval
        [HttpGet]
        [Route("GetAbstractById/{id}")]
        public async Task<ActionResult<Abstract>> GetAbstractById(int id)
        {
            try
            {
                var @abstract = await _repo.GetByIdAsync(id);
                return @abstract == null ? NotFound($"Abstract with ID {id} not found") : Ok(@abstract);
            }
            catch
            {
                return StatusCode(500, "An error occurred while retrieving the abstract");
            }
        }

        /// <summary>
        /// Creates a new abstract in the system
        /// </summary>
        /// <param name="abstract">The abstract object to create</param>
        /// <returns>
        /// 200 OK with the created abstract
        /// 400 Bad Request if the model state is invalid
        /// 500 Internal Server Error if an error occurs during creation
        /// </returns>
        [HttpPost]
        [Route("CreateAbstract")]
        public async Task<ActionResult<Abstract>> CreateAbstract(Abstract @abstract)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var newAbstract = await _repo.CreateAsync(@abstract);
                return Ok(newAbstract);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Updates an existing abstract in the system
        /// </summary>
        /// <param name="abstract">The abstract object with updated values</param>
        /// <returns>
        /// 200 OK with the updated abstract
        /// 400 Bad Request if the model state is invalid
        /// 404 Not Found if the abstract doesn't exist
        /// 500 Internal Server Error if an error occurs during update
        /// </returns>
        [HttpPut]
        [Route("UpdateAbstract")]
        public async Task<ActionResult<Abstract>> UpdateAbstract(Abstract @abstract)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var updatedAbstract = await _repo.UpdateAsync(@abstract);

                return updatedAbstract == null
                    ? NotFound($"Abstract with ID {@abstract.Id} not found")
                    : Ok(updatedAbstract);
            }
            catch
            {
                return StatusCode(500, "An error occurred while updating the abstract");
            }
        }

        /// <summary>
        /// Deletes an abstract from the system by its ID
        /// </summary>
        /// <param name="id">The unique identifier of the abstract to delete</param>
        /// <returns>
        /// 200 OK if deletion is successful
        /// 500 Internal Server Error if an error occurs during deletion
        /// </returns>
        [HttpDelete]
        [Route("DeleteAbstract/{id}")]
        public async Task<ActionResult> DeleteAbstract(int id)
        {
            try
            {
                await _repo.DeleteAsync(id);
                return Ok();
            }
            catch
            {
                return StatusCode(500, "An error occurred while deleting the abstract");
            }
        }
    }
}