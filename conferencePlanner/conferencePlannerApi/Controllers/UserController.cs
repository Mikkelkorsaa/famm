using Microsoft.AspNetCore.Mvc;
using conferencePlannerApi.Repositories.Interfaces;
using conferencePlannerCore.Models;

namespace conferencePlannerApi.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class UsersController : ControllerBase
  {
    private readonly IUserRepo _repository;

    public UsersController(IUserRepo repository)
    {
      _repository = repository;
    }

    [HttpGet]
    public async Task<IEnumerable<User>> GetAll()
        => await _repository.GetAllAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> Get(int id)
    {
      var user = await _repository.GetByIdAsync(id);
      return user == null ? NotFound() : user;
    }

    [HttpPost]
    public async Task<ActionResult<User>> Create(User user)
    {
      var newUser = await _repository.CreateAsync(user);
      return CreatedAtAction(nameof(Get), new { id = newUser.Id }, newUser);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<User>> Update(int id, User user)
    {
      var updatedUser = await _repository.UpdateAsync(id, user);
      return updatedUser == null ? NotFound() : updatedUser;
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
        => await _repository.DeleteAsync(id) ? NoContent() : NotFound();
  }
}