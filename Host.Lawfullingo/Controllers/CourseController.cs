using ApplicationContract.Lawfullingo.Dto.CourseDto;
using ApplicationContract.Lawfullingo.IApplicationService;
using Entity.Lawfullingo;
using Microsoft.AspNetCore.Mvc;

namespace Lawfullingo.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourseController : ControllerBase
{
    private readonly ICourseAppService _courseAppService;
    private readonly IWebHostEnvironment _env;

    public CourseController(ICourseAppService courseAppService, IWebHostEnvironment env)
    {
        _courseAppService = courseAppService;
        _env = env;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var courses = await _courseAppService.GetAllAsync();
        return Ok(courses);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var course = await _courseAppService.GetByIdAsync(id);
        return course == null ? NotFound() : Ok(course);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CourseCreateDto dto)
    {
        string rootPath = Path.Combine(_env.WebRootPath);
        await _courseAppService.AddAsync(dto, rootPath);
        return Ok(new { message = "Course created successfully!" });
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromForm] CourseUpdateDto dto)
    {
        string rootPath = Path.Combine(_env.WebRootPath);
        await _courseAppService.UpdateAsync(dto, rootPath);
        return Ok(new { message = "Course updated successfully!" });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _courseAppService.DeleteAsync(id);
        return Ok(new { message = "Course deleted successfully!" });
    }

    [HttpGet("status/{status}")]
    public async Task<IActionResult> GetByStatus(bool status)
    {
        var courses = await _courseAppService.GetByStatusAsync(status);
        return Ok(courses);
    }

    [HttpGet("teacher/{teacherId}")]
    public async Task<IActionResult> GetByTeacherId(int teacherId)
    {
        var courses = await _courseAppService.GetByTeacherIdAsync(teacherId);
        return Ok(courses);
    }

    [HttpGet("language/{lang}")]
    public async Task<IActionResult> GetByLanguage(string lang)
    {
        var courses = await _courseAppService.GetByLanguageAsync(lang);
        return Ok(courses);
    }

    [HttpGet("category/{categoryId}")]
    public async Task<IActionResult> GetByCategoryId(int categoryId)
    {
        var courses = await _courseAppService.GetByCategoryIdAsync(categoryId);
        return Ok(courses);
    }
    [HttpGet("flag/{flag}")]
public async Task<IActionResult> GetByFlag(string flag)
{
    if (!Enum.TryParse<CourseFlag>(flag, true, out var parsedFlag))
    {
        return BadRequest("Invalid flag value. Allowed: New, Upcoming, Trending.");
    }

    var result = await _courseAppService.GetByFlagAsync(parsedFlag);
    return Ok(result);
}
}
