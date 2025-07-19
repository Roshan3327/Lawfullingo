using ApplicationContract.Lawfullingo.Dto.UsersDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Lawfullingo;

namespace ApplicationContract.Lawfullingo.IApplicationService
{
    public interface IUsersAppService
    {
        Task<IEnumerable<UsersGetDto>> GetAllAsync();
        Task<UsersGetDto> GetByIdAsync(int id);
        Task AddAsync(UsersCreateDto dto);
        Task UpdateAsync(int id, UsersUpdateDto dto);
        Task DeleteAsync(int id);
        Task<Users> GetUserByEmailAsync(string user_email);
        Task<Users> GetUserByMobileAsync(long? mobile);
        Task<string> UploadProfileImageAsync(int id, string imageUrl);

    }
}
