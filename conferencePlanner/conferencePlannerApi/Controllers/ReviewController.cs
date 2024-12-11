using conferencePlannerApi.Repositories.Interfaces;
using conferencePlannerCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace conferencePlannerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewRepo _repo;

        public ReviewsController(IReviewRepo repo)
        {
            _repo = repo;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Review>> GetReviewById(int id)
        {
            var review = await _repo.GetReviewByIdAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            return Ok(review);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Review>> UpdateAbstract(int id, [FromBody] Review review)
        {
            if (id != review.Id)
            {
                return BadRequest();
            }

            var updatedReview = await _repo.UpdateAbstract(id, review);
            if (updatedReview == null)
            {
                return NotFound();
            }

            return Ok(updatedReview);
        }
    }
}
