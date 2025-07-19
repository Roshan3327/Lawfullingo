using ApplicationContract.Lawfullingo.Dto.TeachersDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Lawfullingo;

namespace ApplicationContract.Lawfullingo.IApplicationService
{
    public interface ITeachersAppService
    {
        Task<IEnumerable<TeachersGetDto>> GetAllAsync();
        Task<TeachersGetDto> GetByIdAsync(int id);
        Task AddAsync(TeachersCreateDto dto);
        Task UpdateAsync(int id, TeachersUpdateDto dto);
        Task DeleteAsync(int id);
        Task<Teachers> GetTeacherByEmailAsync(string Teacher_email);
        Task<Teachers> GetTeacherByMobileAsync(long? mobile);
        Task<string> UploadProfileImageAsync(int id, string imageUrl);

    }
}
