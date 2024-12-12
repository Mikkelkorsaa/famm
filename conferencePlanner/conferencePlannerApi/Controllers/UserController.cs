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

       [HttpGet]
       [Route("GetAllUsers")]
       public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
       {
           try
           {
               var users = await _repo.GetAllAsync();
               return Ok(users);
           }
           catch(Exception ex)
           {
               return StatusCode(500, ex.Message);
           }
       }

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

       [HttpPost]
       [Route("CreateUser")]
       public async Task<ActionResult<User>> CreateUser(User user)
       {
           try
           {
               var newUser = await _repo.CreateAsync(user);
               return CreatedAtAction(nameof(GetUserById), new { id = newUser!.Id }, newUser);
           }
           catch
           {
               return StatusCode(500, "An error occurred while creating the user");
           }
       }

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

               var user = await _repo.GetByEmailAsync(model.Email);
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

               if (await _repo.GetByEmailAsync(request.Email) != null)
               {
                   return BadRequest("Email already registered");
               }

               var user = new User
               {
                   Name = request.Name,
                   Email = request.Email,
                   Password = request.Password
               };

               await _repo.CreateAsync(user);
               return Ok();
           }
           catch
           {
               return StatusCode(500, "An error occurred during registration");
           }
       }

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

       private bool ValidatePassword(User user, string password)
       {
           return user.Password == password;
       }
   }
}