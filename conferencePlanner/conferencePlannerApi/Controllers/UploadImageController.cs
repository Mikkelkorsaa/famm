﻿using conferencePlannerApi.Repositories.Interfaces;
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

			if (file == null || file.Length == 0) return BadRequest("File is null or empty");
			
			if (!allowedTypes.Contains(file.ContentType.ToLower())) return BadRequest("Invalid file type");
			
			if(file.Length > 5 * 1024 * 1024) return BadRequest("File size exceeds 5MB");
			
			using var ms = new MemoryStream();
			await file.CopyToAsync(ms);
			var imageBytes = ms.ToArray();

			await _abstractRepository.UploadImage(imageBytes, _abstract);

			return Ok(imageBytes);
		}
	}
}
