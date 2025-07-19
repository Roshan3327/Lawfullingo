using Entity.Lawfullingo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Lawfullingo.Repository.Teacheres
{
    public interface ITeachersRepository
    {
        Task<IEnumerable<Teachers>> GetAllAsync();
        Task<Teachers> GetByIdAsync(int id);
        Task AddAsync(Teachers teacher);
        Task UpdateAsync(int id, Teachers teacher);
        Task DeleteAsync(int id);
        Task<Teachers> GetTeacherByEmailAsync(string user_email);
        Task<Teachers> GetTeacherByMobileAsync(long? mobile);
    }
}
