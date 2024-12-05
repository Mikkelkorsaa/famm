using conferencePlannerApi.Repositories.Interfaces;
using conferencePlannerCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace conferencePlannerApi.Controllers
{
	public class UploadImageController : Controller
	{

		private readonly IAbstractRepo _abstractRepository;

        public UploadImageController(IAbstractRepo abstractRepository)
        {
			_abstractRepository = abstractRepository;
		}

        [HttpPost]
		public async Task<IActionResult> UploadImage(IFormFile file, Abstract _abstract)
		{
			var allowedTypes = new[] { "image/jpeg", "image/png", "image/gif" };

			if (file == null || file.Length == 0) return BadRequest("Tom fil");
			
			if (!allowedTypes.Contains(file.ContentType.ToLower())) return BadRequest("Filtypen er ikke gyldig");
			
			if(file.Length > 20000000) return BadRequest("Filen er større end 20MB");
			
			using var ms = new MemoryStream();
			await file.CopyToAsync(ms);
			byte[] imageBytes = ms.ToArray();

			await _abstractRepository.UploadImage(imageBytes, _abstract);

			return Ok(imageBytes);
		}
	}
}
