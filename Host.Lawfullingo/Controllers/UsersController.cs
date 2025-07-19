using ApplicationContract.Lawfullingo.Dto.UsersDto;
using ApplicationContract.Lawfullingo.IApplicationService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Host.Lawfullingo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersAppService _usersAppService;
        private readonly IWebHostEnvironment _env;


        public UsersController(IUsersAppService usersAppService, IWebHostEnvironment env)
        {
            _usersAppService = usersAppService;
            _env = env;
        }

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
                return NotFound("User not found with this id ");
            return Ok(user);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromForm] UsersCreateDto dto)
        {
            var existingbyMobile = await _usersAppService.GetUserByMobileAsync(dto.Mobile);
            var existingByEmail = await _usersAppService.GetUserByEmailAsync(dto.user_email);

            if (existingbyMobile != null)
            {
                return BadRequest("This Mobile Number already exists!.");
            }

            if (existingByEmail != null)
            {
                return BadRequest("This Email Address already exists!.");
            }

            string imageUrl = string.Empty;
            if (dto.ProfileImage != null && dto.ProfileImage.Length > 0)
            {
                var imageFile = dto.ProfileImage;

                if (imageFile.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                    var folderPath = Path.Combine(_env.WebRootPath, "Profileimage");

                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    var filePath = Path.Combine(folderPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                    var baseUrl = "https://formapi.codedonor.in/";
                    imageUrl = Path.Combine(baseUrl, "LogoPics", fileName);
                }
            }
            dto.ProfileImageUrl = imageUrl;
            await _usersAppService.AddAsync(dto);
            return Ok(new { message = "User created successfully" });
        }

        

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int userId, [FromBody] UsersUpdateDto dto)
        {
            await _usersAppService.UpdateAsync(userId, dto);
            return Ok(new { message = "User updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _usersAppService.DeleteAsync(id);
            return Ok(new { message = "User deleted successfully" });
        }
        [HttpPost("update-profile-image")]
        public async Task<IActionResult> UpdateProfileImage([FromForm] int userId, UpdateProfileImageDto dto)
        {
            var user = await _usersAppService.GetByIdAsync(userId);

            if (user == null)
                return NotFound("User not found");

            if (dto.ProfileImage != null && dto.ProfileImage.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.ProfileImage.FileName);
                var folderPath = Path.Combine("wwwroot", "Profileimage");

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.ProfileImage.CopyToAsync(stream);
                }

                var baseUrl = "https://formapi.codedonor.in/";
                user.ProfileImage = Path.Combine(baseUrl, "Profileimage", fileName);

                await _usersAppService.UploadProfileImageAsync(userId, user.ProfileImage    );

                return Ok(new { message = "Profile image updated successfully." });
            }

            return BadRequest("No image uploaded.");
        }



    }
}
