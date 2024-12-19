using Microsoft.AspNetCore.Mvc;
using conferencePlannerApi.Repositories.Interfaces;
using conferencePlannerCore.Models;
using conferencePlannerApi.Services.Interfaces;

namespace conferencePlannerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo _repo;
        private readonly IEmailService _emailService;

        public UserController(IUserRepo repo, IEmailService emailService)
        {
            _repo = repo;
            _emailService = emailService;
        }

        /// <summary>
        /// Retrieves all users from the system
        /// </summary>
        /// <returns>200 OK with a collection of all users</returns>
        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await _repo.GetAllAsync();
            return Ok(users);
        }

        /// <summary>
        /// Retrieves a specific user by their ID
        /// </summary>
        /// <param name="id">The unique identifier of the user to retrieve</param>
        /// <returns>
        /// 200 OK with the user if found
        /// 404 Not Found if the user doesn't exist
        /// 500 Internal Server Error if an error occurs during retrieval
        /// </returns>
        [HttpGet]
        [Route("GetUserById/{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            try
            {
                var user = await _repo.GetByIdAsync(id);
                return user == null ? NotFound($"User with ID {id} not found") : Ok(user);
            }
            catch
            {
                return StatusCode(500, "An error occurred while retrieving the user");
            }
        }

        /// <summary>
        /// Creates a new user in the system
        /// </summary>
        /// <param name="user">The user object to create</param>
        /// <returns>
        /// 200 OK with the created user
        /// 500 Internal Server Error if an error occurs during creation
        /// </returns>
        [HttpPost]
        [Route("CreateUser")]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            try
            {
                var newUser = await _repo.CreateAsync(user);
                return Ok(newUser);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Updates an existing user's information
        /// </summary>
        /// <param name="user">The user object with updated values</param>
        /// <returns>
        /// 200 OK with the updated user
        /// 400 Bad Request if the model state is invalid
        /// 404 Not Found if the user doesn't exist
        /// 500 Internal Server Error if an error occurs during update
        /// </returns>
        [HttpPut]
        [Route("UpdateUser")]
        public async Task<ActionResult<User>> UpdateUser(User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var updatedUser = await _repo.UpdateAsync(user);
                return updatedUser == null
                    ? NotFound($"User with ID {user.Id} not found")
                    : Ok(updatedUser);
            }
            catch
            {
                return StatusCode(500, "An error occurred while updating the user");
            }
        }


        /// <summary>
        /// Deletes a user from the system
        /// </summary>
        /// <param name="id">The unique identifier of the user to delete</param>
        /// <returns>
        /// 204 No Content if deletion is successful
        /// 404 Not Found if the user doesn't exist
        /// 500 Internal Server Error if an error occurs during deletion
        /// </returns>
        [HttpDelete]
        [Route("DeleteUser/{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
                var result = await _repo.DeleteAsync(id);
                return result ? NoContent() : NotFound($"User with ID {id} not found");
            }
            catch
            {
                return StatusCode(500, "An error occurred while deleting the user");
            }
        }

        /// <summary>
        /// Authenticates a user and logs them into the system
        /// </summary>
        /// <param name="model">The login credentials</param>
        /// <returns>
        /// 200 OK with the user if authentication is successful
        /// 400 Bad Request if the model state is invalid
        /// 401 Unauthorized if credentials are invalid
        /// 403 Forbidden if account is inactive
        /// 500 Internal Server Error if an error occurs during login
        /// </returns>
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<User>> Login(LoginModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var user = await _repo.GetByEmailAsync(model.Email.ToLower());
                if (user == null || !ValidatePassword(user, model.Password))
                {
                    return Unauthorized("Invalid email or password");
                }

                if (!user.IsActive)
                {
                    return Forbid("Account is not active");
                }

                return Ok(user);
            }
            catch
            {
                return StatusCode(500, "An error occurred during login");
            }
        }

        /// <summary>
        /// Registers a new user in the system
        /// </summary>
        /// <param name="request">The registration information</param>
        /// <returns>
        /// 200 OK with the created user
        /// 400 Bad Request if the model state is invalid or user creation fails
        /// 500 Internal Server Error if an error occurs during registration
        /// </returns>
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterModel request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Create the user object
                var user = new User
                {
                    Name = request.Name,
                    Email = request.Email.ToLower(),
                    Password = request.Password,
                    Organization = request.Organization,
                    Role = UserRole.Applicant,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                };

                // Attempt to create the user
                var createdUser = await _repo.CreateAsync(user);

                if (createdUser == null)
                {
                    return BadRequest("Failed to create user");
                }

                // Return success with the created user
                return Ok(createdUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Retrieves a user by their email address
        /// </summary>
        /// <param name="email">The email address of the user to retrieve</param>
        /// <returns>
        /// 200 OK with the user if found
        /// 404 Not Found if the user doesn't exist
        /// 500 Internal Server Error if an error occurs during retrieval
        /// </returns>
        [HttpGet]
        [Route("GetUserByEmail/{email}")]
        public async Task<ActionResult<User>> GetUserByEmail(string email)
        {
            try
            {
                var user = await _repo.GetByEmailAsync(email);
                return user == null ? NotFound($"User with email {email} not found") : Ok(user);
            }
            catch
            {
                return StatusCode(500, "An error occurred while retrieving the user");
            }
        }

        /// <summary>
        /// Sends a test email using the email service
        /// </summary>
        /// <returns>
        /// 200 OK if email is sent successfully
        /// 500 Internal Server Error if an error occurs while sending the email
        /// </returns>
        [HttpPost]
        [Route("SendEmail")]
        public async Task<IActionResult> SendEmail()
        {
            try
            {
                await _emailService.SendEmailAsync(
                    "mikkelkorsaa@gmail.com",
                    "Test Subject",
                    "<h1>Hello</h1><p>This is a test email.</p>"
                );
                return Ok("Email sent successfully");
            }
            catch
            {
                return StatusCode(500, "An error occurred while sending the email");
            }
        }

        /// <summary>
        /// Retrieves filtered users using OR logic between filter criteria
        /// </summary>
        /// <param name="filter">The filter criteria to apply</param>
        /// <returns>
        /// 200 OK with the filtered list of users
        /// Empty list if no matches found
        /// </returns>
        [HttpPost]
        [Route("filter/or")]
        public async Task<ActionResult<IEnumerable<User>>> GetOrFilterUsers(UserFilter filter) {
            var users = await _repo.GetFilterORSearch(filter);
            return users != null ? Ok(users) : new List<User>();
        }
        private bool ValidatePassword(User user, string password)
        {
            return user.Password == password;
        }

        /// <summary>
        /// Gets the total number of users matching the OR filter criteria
        /// </summary>
        /// <param name="filter">The filter criteria to apply</param>
        /// <returns>
        /// 200 OK with the count of matching users
        /// </returns>
        [HttpPost]
        [Route("filter/or/hits")]
        public async Task<ActionResult<int>> GetHitsOrFilter(UserFilter filter){
            var noOfHits = await _repo.GetFilterOrSearchNumberOfHits(filter);
            return Ok(noOfHits);
        }
    }
}