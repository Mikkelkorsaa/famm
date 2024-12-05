using conferencePlannerApi.Repositories.Interfaces;
using conferencePlannerCore.Models;
using Microsoft.AspNetCore.Mvc;

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
		public async Task<IEnumerable<Abstract>> GetAllAbstracts()
			=> await _repo.GetAllAsync();
		
		[HttpGet]
		[Route("GetAbstractById/{id}")]
		public async Task<ActionResult<Abstract>> GetAbstractById(int id)
		{
			var @abstract = await _repo.GetByIdAsync(id);
			return @abstract == null ? NotFound() : @abstract;
		}

		[HttpPost]
		[Route("CreateAbstract")]
		public async Task<ActionResult<Abstract>> CreateAbstract(Abstract @abstract)
		{
			var newAbstract = await _repo.CreateAsync(@abstract);
			return CreatedAtAction(nameof(GetAbstractById), new { id = newAbstract.Id }, newAbstract);
		}

		[HttpPut]
		[Route("UpdateAbstract")]
		public async Task<ActionResult<Abstract>> UpdateAbstract(Abstract @abstract)
		{
			var updatedAbstract = await _repo.UpdateAsync(@abstract);
			return updatedAbstract == null ? NotFound() : updatedAbstract;
		}

		[HttpDelete]
		[Route("DeleteAbstract/{id}")]
		public async Task<ActionResult> DeleteAbstract(int id)
			=> await _repo.DeleteAsync(id) ? NoContent() : NotFound();
	}
}
