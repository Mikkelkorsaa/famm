using conferencePlannerApi.Repositories.Interfaces;
using conferencePlannerCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace conferencePlannerApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AbstractController : ControllerBase
	{

		private readonly IAbstractRepo _repository;

		public AbstractController(IAbstractRepo repository)
		{
			_repository = repository;
		}

		[HttpGet]
		[Route("GetAllAbstracts")]
		public async Task<IEnumerable<Abstract>> GetAllAbstracts()
			=> await _repository.GetAllAsync();
		
		[HttpGet]
		[Route("GetAbstractById/{id}")]
		public async Task<ActionResult<Abstract>> GetAbstractById(int id)
		{
			var @abstract = await _repository.GetByIdAsync(id);
			return @abstract == null ? NotFound() : @abstract;
		}

		[HttpPost]
		[Route("CreateAbstract")]
		public async Task<ActionResult<Abstract>> CreateAbstract(Abstract @abstract)
		{
			var newAbstract = await _repository.CreateAsync(@abstract);
			return CreatedAtAction(nameof(GetAbstractById), new { id = newAbstract.Id }, newAbstract);
		}

		[HttpPut]
		[Route("UpdateAbstract")]
		public async Task<ActionResult<Abstract>> UpdateAbstract(Abstract @abstract)
		{
			var updatedAbstract = await _repository.UpdateAsync(@abstract);
			return updatedAbstract == null ? NotFound() : updatedAbstract;
		}

		[HttpDelete]
		[Route("DeleteAbstract/{id}")]
		public async Task<ActionResult> DeleteAbstract(int id)
			=> await _repository.DeleteAsync(id) ? NoContent() : NotFound();
	}
}
