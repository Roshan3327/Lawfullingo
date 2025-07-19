using Entity.Lawfullingo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Lawfullingo.Repository.userses
{
    public interface IUsersRepository
    {
        Task<IEnumerable<Users>> GetAllAsync();
        Task<Users> GetByIdAsync(int id);
        Task AddAsync(Users user);
        Task UpdateAsync(int id, Users user);
        Task DeleteAsync(int id);
        Task<Users> GetUserByEmailAsync(string teacher_email);
        Task<Users> GetUserByMobileAsync(long? mobile);
    }
}
