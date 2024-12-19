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

        /// <summary>
        /// Retrieves a specific review by its ID
        /// </summary>
        /// <param name="id">The unique identifier of the review to retrieve</param>
        /// <returns>
        /// 200 OK with the review if found
        /// 404 Not Found if the review doesn't exist
        /// 500 Internal Server Error if an error occurs during retrieval
        /// </returns>
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

        /// <summary>
        /// Updates an existing review
        /// </summary>
        /// <param name="id">The ID of the review to update</param>
        /// <param name="review">The review object containing updated values</param>
        /// <returns>
        /// 200 OK with the updated review
        /// 400 Bad Request if the model state is invalid or IDs don't match
        /// 404 Not Found if the review doesn't exist
        /// 500 Internal Server Error if an error occurs during update
        /// </returns>
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