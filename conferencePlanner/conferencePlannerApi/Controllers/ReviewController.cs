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
            try
            {
                var review = await _repo.GetReviewByIdAsync(id);
                return review == null 
                    ? NotFound($"Review with ID {id} not found") 
                    : Ok(review);
            }
            catch
            {
                return StatusCode(500, "An error occurred while retrieving the review");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Review>> UpdateAbstract(int id, [FromBody] Review review)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != review.Id)
                {
                    return BadRequest("ID in URL does not match ID in review object");
                }

                var updatedReview = await _repo.UpdateAbstract(id, review);
                return updatedReview == null 
                    ? NotFound($"Review with ID {id} not found") 
                    : Ok(updatedReview);
            }
            catch
            {
                return StatusCode(500, "An error occurred while updating the review");
            }
        }
    }
}