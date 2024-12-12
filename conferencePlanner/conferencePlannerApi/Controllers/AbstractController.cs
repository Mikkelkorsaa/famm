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

        public AbstractController(IAbstractRepo repo)
        {
            _repo = repo;
        }

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
                return CreatedAtAction(nameof(GetAbstractById), 
                    new { id = newAbstract.Id }, 
                    newAbstract);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

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

        [HttpDelete]
        [Route("DeleteAbstract/{id}")]
        public async Task<ActionResult> DeleteAbstract(int id)
        {
            try
            {
                var result = await _repo.DeleteAsync(id);
                return result ? NoContent() : NotFound($"Abstract with ID {id} not found");
            }
            catch
            {
                return StatusCode(500, "An error occurred while deleting the abstract");
            }
        }
    }
}