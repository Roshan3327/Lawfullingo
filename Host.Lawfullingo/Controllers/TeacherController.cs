using ApplicationContract.Lawfullingo.Dto.TeachersDto;
using ApplicationContract.Lawfullingo.Dto.UsersDto;
using ApplicationContract.Lawfullingo.IApplicationService;
using Microsoft.AspNetCore.Mvc;

namespace Host.Lawfullingo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeancherController : ControllerBase
    {
        private readonly ITeachersAppService _TeachersAppService;
        private readonly IWebHostEnvironment _env;


        public TeancherController(ITeachersAppService TeachersAppService, IWebHostEnvironment env)
        {
            _TeachersAppService = TeachersAppService;
            _env = env;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeachersGetDto>>> GetAll()
        {
            var Teachers = await _TeachersAppService.GetAllAsync();
            return Ok(Teachers);
        }

        // GET: api/Teachers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TeachersGetDto>> GetById(int id)
        {
            var Teacher = await _TeachersAppService.GetByIdAsync(id);
            if (Teacher == null)
                return NotFound("Teacher not found with this id ");
            return Ok(Teacher);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromForm] TeachersCreateDto dto)
        {
            var existingbyMobile = await _TeachersAppService.GetTeacherByMobileAsync(dto.Mobile);
            var existingByEmail = await _TeachersAppService.GetTeacherByEmailAsync(dto.TeacherEmail);

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
            await _TeachersAppService.AddAsync(dto);
            return Ok(new { message = "Teacher created successfully" });
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int TeacherId, [FromBody] TeachersUpdateDto dto)
        {
            await _TeachersAppService.UpdateAsync(TeacherId, dto);
            return Ok(new { message = "Teacher updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _TeachersAppService.DeleteAsync(id);
            return Ok(new { message = "Teacher deleted successfully" });
        }
        [HttpPost("update-profile-image")]
        public async Task<IActionResult> UpdateProfileImage([FromForm] int TeacherId, UpdateTeacherProfileImageDto dto)
        {
            var Teacher = await _TeachersAppService.GetByIdAsync(TeacherId);

            if (Teacher == null)
                return NotFound("Teacher not found");

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
                Teacher.ProfileImage = Path.Combine(baseUrl, "Profileimage", fileName);

                await _TeachersAppService.UploadProfileImageAsync(TeacherId, Teacher.ProfileImage);

                return Ok(new { message = "Profile image updated successfully." });
            }

            return BadRequest("No image uploaded.");
        }



    }
}
