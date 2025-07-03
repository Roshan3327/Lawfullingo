using ApplicationContract.Lawfullingo;
using ApplicationContract.Lawfullingo.Dto.UsersDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Host.Lawfullingo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersAppService _usersAppService;

        public UsersController(IUsersAppService usersAppService)
        {
            _usersAppService = usersAppService;
        }

        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsersGetDto>>> GetAll()
        {
            var users = await _usersAppService.GetAllAsync();
            return Ok(users);
        }

        // GET: api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsersGetDto>> GetById(int id)
        {
            var user = await _usersAppService.GetByIdAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // POST: api/users
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UsersCreateDto dto)
        {
            await _usersAppService.AddAsync(dto);
            return Ok(new { message = "User created successfully" });
        }

        // PUT: api/users
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UsersUpdateDto dto)
        {
            await _usersAppService.UpdateAsync(dto);
            return Ok(new { message = "User updated successfully" });
        }

        // DELETE: api/users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _usersAppService.DeleteAsync(id);
            return Ok(new { message = "User deleted successfully" });
        }

         
        
    }
}
    