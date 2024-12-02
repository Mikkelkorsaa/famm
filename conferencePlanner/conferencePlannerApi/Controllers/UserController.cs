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
    [Route("UpdateUser/{id}")]
    public async Task<ActionResult<User>> UpdateUser(int id, User user)
    {
      var updatedUser = await _repository.UpdateAsync(id, user);
      return updatedUser == null ? NotFound() : updatedUser;
    }

    [HttpDelete]
    [Route("DeleteUser/{id}")]
    public async Task<ActionResult> DeleteUser(int id)
        => await _repository.DeleteAsync(id) ? NoContent() : NotFound();
  }
}