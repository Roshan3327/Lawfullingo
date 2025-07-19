using ApplicationContract.Lawfullingo.Dto.CourseDto;
using ApplicationContract.Lawfullingo.IApplicationService;
using AutoMapper;
using Data.Lawfullingo.Repository.Courses;
using Entity.Lawfullingo;

namespace Application.Lawfullingo;

public class CourseAppService : ICourseAppService
{
    private readonly ICourseRepository _repository;
    private readonly IMapper _mapper;

    public CourseAppService(ICourseRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CourseGetDto>> GetAllAsync()
    {
        var courses = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<CourseGetDto>>(courses);
    }
    public async Task<CourseGetDto> GetByIdAsync(int id)
    {
        var course = await _repository.GetByIdAsync(id);
        return _mapper.Map<CourseGetDto>(course);
    }

    public async Task AddAsync(CourseCreateDto dto, string imagePathRoot)
    {
        var course = _mapper.Map<CourseCreateDto, Course>(dto);

        // Save image
        if (dto.Course_Image != null && dto.Course_Image.Length > 0)
        {
            var fileName = $"{Guid.NewGuid()}_{dto.Course_Image.FileName}";
            var fullPath = Path.Combine(imagePathRoot, "CourseImages", fileName);
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await dto.Course_Image.CopyToAsync(stream);
            }

            course.course_image = $"/CourseImages/{fileName}";
        }
        course.created_at = DateTime.UtcNow;
        course.discount_amount = (dto.Course_Price * dto.Discount_Percentage) / 100;
        course.purchase_amount = dto.Course_Price - course.discount_amount + dto.Charges_Amount - dto.Coupon_Amount;

        await _repository.AddAsync(course);
    }
    public async Task UpdateAsync(CourseUpdateDto dto, string imagePathRoot)
    {
        var course = await _repository.GetByIdAsync(dto.Id);
        if (course == null) return;

        _mapper.Map(dto, course);

        // Update image if provided
        if (dto.Course_Image != null && dto.Course_Image.Length > 0)
        {
            var fileName = $"{Guid.NewGuid()}_{dto.Course_Image.FileName}";
            var fullPath = Path.Combine(imagePathRoot, "CourseImages", fileName);
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await dto.Course_Image.CopyToAsync(stream);
            }

            course.course_image = $"/CourseImages/{fileName}";
        }

        course.discount_amount = (dto.Course_Price * dto.Discount_Percentage) / 100;
        course.purchase_amount = dto.Course_Price - course.discount_amount + dto.Charges_Amount - dto.Coupon_Amount;

        await _repository.UpdateAsync(course);
    }
    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task<IEnumerable<CourseGetDto>> GetByStatusAsync(bool status)
    {
        var data = await _repository.GetByStatusAsync(status);
        return _mapper.Map<IEnumerable<CourseGetDto>>(data);
    }

    public async Task<IEnumerable<CourseGetDto>> GetByLanguageAsync(string language)
    {
        var data = await _repository.GetByLanguageAsync(language);
        return _mapper.Map<IEnumerable<CourseGetDto>>(data);
    }

    public async Task<IEnumerable<CourseGetDto>> GetByTeacherIdAsync(int teacherId)
    {
        var data = await _repository.GetByTeacherIdAsync(teacherId);
        return _mapper.Map<IEnumerable<CourseGetDto>>(data);
    }

    public async Task<IEnumerable<CourseGetDto>> GetByCategoryIdAsync(int categoryId)
    {
        var data = await _repository.GetByCategoryIdAsync(categoryId);
        return _mapper.Map<IEnumerable<CourseGetDto>>(data);
    }
    public async Task<IEnumerable<CourseGetDto>> GetByFlagAsync(CourseFlag flag)
    {
        var courses = await _repository.GetByFlagAsync(flag);
        return _mapper.Map<IEnumerable<CourseGetDto>>(courses);
    }
}


