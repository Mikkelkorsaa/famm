using Microsoft.AspNetCore.Mvc;
using conferencePlannerApi.Repositories.Interfaces;
using conferencePlannerCore.Models;

namespace conferencePlannerApi.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class UserController : ControllerBase
  {
    private readonly IUserRepo _repository;

    public UserController(IUserRepo repository)
    {
      _repository = repository;
    }

    [HttpGet]
    [Route("GetAllUsers")]
    public async Task<IEnumerable<User>> GetAllUsers()
        => await _repository.GetAllAsync();

    [HttpGet]
    [Route("GetUserById/{id}")]
    public async Task<ActionResult<User>> GetUserById(int id)
    {
      var user = await _repository.GetByIdAsync(id);
      return user == null ? NotFound() : user;
    }

    [HttpPost]
    [Route("CreateUser")]
    public async Task<ActionResult<User>> CreateUser(User user)
    {
      var newUser = await _repository.CreateAsync(user);
      return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
    }

    [HttpPut]
    [Route("UpdateUser")]
    public async Task<ActionResult<User>> UpdateUser(User user)
    {
      var updatedUser = await _repository.UpdateAsync(user);
      return updatedUser == null ? NotFound() : updatedUser;
    }

    [HttpDelete]
    [Route("DeleteUser/{id}")]
    public async Task<ActionResult> DeleteUser(int id)
        => await _repository.DeleteAsync(id) ? NoContent() : NotFound();

    [HttpPost]
    [Route("Login")]
    public async Task<ActionResult<User>> Login(LoginModel model)
    {
      var user = await _repository.GetByEmailAsync(model.Email);
      if (user == null || !ValidatePassword(user, model.Password))
      {
        return Unauthorized();
      }
      return user;
    }

    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register(RegisterModel request)
    {
      if (await _repository.GetByEmailAsync(request.Email) != null)
      {
        return BadRequest("Email already registered");
      }

      var user = new User
      {
        Name = request.Name,
        Email = request.Email,
        Password = request.Password
      };

      await _repository.CreateAsync(user);

      return Ok();
    }

    private bool ValidatePassword(User user, string password)
    {
      // Replace with actual password validation logic
      return true; // Temporary for testing
    }

    [HttpGet]
    [Route("GetUserByEmail/{email}")]
    public async Task<ActionResult<User>> GetUserByEmail(string email)
    {
      var user = await _repository.GetByEmailAsync(email);
      return user == null ? NotFound() : user;
    }
  }
}