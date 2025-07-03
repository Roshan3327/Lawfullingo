using Entity.Lawfullingo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Lawfullingo.Repository.ClassVideos
{
    public class ClassVideosRepository : IClassVideosRepository
    {
        private readonly ApplicationDbContext _context;

        public ClassVideosRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Class_Videos>> GetAllAsync()
        {
            return await _context.Class_Videos.ToListAsync();
        }

        public async Task<Class_Videos> GetByIdAsync(int id)
        {
            return await _context.Class_Videos.FirstOrDefaultAsync(v => v.id == id);
        }

        public async Task AddAsync(Class_Videos video)
        {
            _context.Class_Videos.Add(video);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Class_Videos video)
        {
            _context.Class_Videos.Update(video);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var video = await _context.Class_Videos.FindAsync(id);
            if (video != null)
            {
                _context.Class_Videos.Remove(video);
                await _context.SaveChangesAsync();
            }
        }
    }
}