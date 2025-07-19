using ApplicationContract.Lawfullingo.Dto.CourseDto;
using Entity.Lawfullingo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationContract.Lawfullingo.IApplicationService
{
    public interface ICourseAppService
    {
        Task<IEnumerable<CourseGetDto>> GetAllAsync();
        Task<CourseGetDto> GetByIdAsync(int id);

        Task AddAsync(CourseCreateDto dto, string imagePathRoot);
        Task UpdateAsync(CourseUpdateDto dto, string imagePathRoot);
        Task DeleteAsync(int id);

        Task<IEnumerable<CourseGetDto>> GetByStatusAsync(bool status);
        Task<IEnumerable<CourseGetDto>> GetByLanguageAsync(string language);
        Task<IEnumerable<CourseGetDto>> GetByTeacherIdAsync(int teacherId);
        Task<IEnumerable<CourseGetDto>> GetByCategoryIdAsync(int categoryId);
        Task<IEnumerable<CourseGetDto>> GetByFlagAsync(CourseFlag flag);

    }
}
