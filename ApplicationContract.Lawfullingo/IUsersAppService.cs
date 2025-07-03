using ApplicationContract.Lawfullingo.Dto.UsersDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationContract.Lawfullingo
{
    public interface IUsersAppService
    {
        Task<IEnumerable<UsersGetDto>> GetAllAsync();
        Task<UsersGetDto> GetByIdAsync(int id);
        Task AddAsync(UsersCreateDto dto);
        Task UpdateAsync(UsersUpdateDto dto);
        Task DeleteAsync(int id);
          
    }
}
