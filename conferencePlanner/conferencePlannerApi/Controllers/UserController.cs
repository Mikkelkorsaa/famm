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
    public async Task<IEnumerable<User>> GetAllUsers()
        => await _repo.GetAllAsync();

    [HttpGet]
    [Route("GetUserById/{id}")]
    public async Task<ActionResult<User>> GetUserById(int id)
    {
      var user = await _repo.GetByIdAsync(id);
      return user == null ? NotFound() : user;
    }

    [HttpPost]
    [Route("CreateUser")]
    public async Task<ActionResult<User>> CreateUser(User user)
    {
      var newUser = await _repo.CreateAsync(user);
      return CreatedAtAction(nameof(GetUserById), new { id = newUser!.Id }, newUser);
    }

    [HttpPut]
    [Route("UpdateUser")]
    public async Task<ActionResult<User>> UpdateUser(User user)
    {
      var updatedUser = await _repo.UpdateAsync(user);
      return updatedUser == null ? NotFound() : updatedUser;
    }

    [HttpDelete]
    [Route("DeleteUser/{id}")]
    public async Task<ActionResult> DeleteUser(int id)
        => await _repo.DeleteAsync(id) ? NoContent() : NotFound();

    [HttpPost]
    [Route("Login")]
    public async Task<ActionResult<User>> Login(LoginModel model)
    {
      var user = await _repo.GetByEmailAsync(model.Email);
      if (user == null || !ValidatePassword(user, model.Password))
      {
        return Unauthorized();
      }
      else if (!user.IsActive)
      {
        return Forbid();
      }
      return user;
    }

    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register(RegisterModel request)
    {
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

    private bool ValidatePassword(User user, string password)
    {
      if (user.Password != password)
      {
        return false;
      }
      return true;
    }

    [HttpGet]
    [Route("GetUserByEmail/{email}")]
    public async Task<ActionResult<User>> GetUserByEmail(string email)
    {
      var user = await _repo.GetByEmailAsync(email);
      return user == null ? NotFound() : user;
    }

    [HttpPost]
    public async Task<IActionResult> SendEmail()
    {
        await _emailService.SendEmailAsync(
            "mikkelkorsaa@gmail.com",
            "Test Subject",
            "<h1>Hello</h1><p>This is a test email.</p>"
        );
        return Ok();
    }
  }
}