using ApplicationContract.Lawfullingo.Common.Response;
using ApplicationContract.Lawfullingo.Dto.ClassVideoDto;
using ApplicationContract.Lawfullingo.IApplicationService;
using Data.Lawfullingo.Repository.ClassVideos;
using Data.Lawfullingo.Repository.Purchases;
using Entity.Lawfullingo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Host.Lawfullingo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClassVideoController : ControllerBase
{

    private readonly IClassVideoAppService _appService;
    private readonly IPurchaseRepository _purchaseRepository;
    private readonly IClassVideosRepository _classVideosRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ClassVideoController(IClassVideoAppService appService, IHttpContextAccessor httpContextAccessor, IPurchaseRepository purchaseRepository, IClassVideosRepository classVideosRepository)
    {
        _appService = appService;
        _httpContextAccessor = httpContextAccessor;
        _purchaseRepository = purchaseRepository;
        _classVideosRepository = classVideosRepository;
    }

    [HttpGet("user-class-videos")]
    [Authorize]
    public async Task<ActionResult> GetUserClassVideos([FromQuery] int UserId)
    {
        try
        {
            var userIdClaim = HttpContext.User.FindFirst("Id")?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized(ApiResponse<List<ClassVideoOnlyDto>>.FailResponse("User is not logged in."));

            int loggedInUserId = int.Parse(userIdClaim);

            if (UserId != loggedInUserId)
            {
                return Unauthorized(ApiResponse<List<ClassVideoOnlyDto>>.FailResponse("Access denied. You can only view your own videos."));
            }

            var result = await _appService.GetUserPurchaseClassVideosAsync(loggedInUserId);

            return Ok(ApiResponse<List<ClassVideoOnlyDto>>.SuccessResponse(result, "User class videos retrieved successfully."));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<List<ClassVideoOnlyDto>>.FailResponse(ex.Message));
        }
    }


    [HttpGet("get-videos-url")]
    public async Task<IActionResult> GetVideos([FromQuery] int courseId)
    {
        try
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var userRole = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Role);

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(userRole))
                return Unauthorized(ApiResponse<string>.FailResponse("User not logged in."));

            var user_Id = Convert.ToInt32(userId);

            if (userRole.ToLower() == "teacher")
            {
                var videosQuery = await _classVideosRepository.GetUserClassVideosAsync(courseId);

                var videoDtos = videosQuery.Select(v => new ClassVideoOnlyDto
                {
                    video_url = v.Class_Videos.video_url
                }).ToList();

                return Ok(ApiResponse<List<ClassVideoOnlyDto>>.SuccessResponse(videoDtos));
            }
            else if (userRole.ToLower() == "user")
            {
                var purchases = await _purchaseRepository.GetCoursesByUserIdCourseIdAsync(user_Id, courseId);

                if (purchases != null && purchases.Any())
                {
                    var classVideos = purchases
                        .SelectMany(p => p.course.course_Classes)
                        .Select(v => new ClassVideoOnlyDto
                        {
                            video_url = v.Class_Videos.video_url
                        }).ToList();

                    return Ok(ApiResponse<List<ClassVideoOnlyDto>>.SuccessResponse(classVideos));
                }
                else
                {
                    return BadRequest(ApiResponse<string>.FailResponse("You are not authorized. Please purchase this course first."));
                }
            }

            return BadRequest(ApiResponse<string>.FailResponse("Invalid role or missing data."));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<List<ClassVideoOnlyDto>>.FailResponse(ex.Message));
        }
    }





    [HttpPost("upload")]
    public async Task<IActionResult> Upload([FromForm] ClassVideoCreateDto dto)
    {
        var result = await _appService.UploadAsync(dto);
        return Ok(result);
    }



    [HttpPost]

    public async Task<ActionResult> Create(ClassVideoCreateDto dto)
    {
        await _appService.AddAsync(dto);
        return Ok(new { message = "Video created successfully" });
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClassVideoGetDto>>> GetAllAsync()
    { 
       var video=  await _appService.GetAllAsync();
        return Ok(video);
    }
    
    [HttpGet("{id}")]

    public async Task<ActionResult<ClassVideoGetDto>> GetById(int id)
    {
      var video=  await _appService.GetByIdAsync(id);
        if (video == null)
        {
            return NotFound();
        }
        return Ok(video);


    }

    [HttpPut]

    public async Task<ActionResult> Update(ClassVideoUpdateDto dto)
    {
        return await _appService.UpdateAsync(dto)
            .ContinueWith(task => 
            {
                if (task.IsCompletedSuccessfully)
                {
                    return Ok(new { message = "Video updated successfully" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error updating video");
                }
            });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _appService.DeleteAsync(id);
        return Ok(new { message = "Video deleted successfully" });
    }

}
