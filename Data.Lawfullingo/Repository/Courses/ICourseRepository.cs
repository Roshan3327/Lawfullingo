using Entity.Lawfullingo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Lawfullingo.Repository.Courses
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllAsync();
        Task<Course> GetByIdAsync(int id);
        Task AddAsync(Course course);
        Task UpdateAsync(Course course);
        Task DeleteAsync(int id);
        Task<IEnumerable<Course>> GetByLanguageAsync(string language);
        Task<IEnumerable<Course>> GetByFlagAsync(CourseFlag flag);
        Task<IEnumerable<Course>> GetByTeacherIdAsync(int teacherId);
        Task<IEnumerable<Course>> GetByStatusAsync(bool status);

        Task<IEnumerable<Course>> GetByCategoryIdAsync(int categoryId);
    }
}
