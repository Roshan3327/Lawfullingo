using Entity.Lawfullingo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Lawfullingo.Repository.ClassVideos
{
    public  interface IClassVideosRepository
    {
        Task<IEnumerable<Class_Videos>> GetAllAsync();
        Task<Class_Videos> GetByIdAsync(int id);
        Task AddAsync(Class_Videos video);
        Task UpdateAsync(Class_Videos video);
        Task DeleteAsync(int id);
    }
}
