using Entity.Lawfullingo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Lawfullingo.Repository.Teacheres
{
    public class TeachersRepository
    {
        private readonly ApplicationDbContext _context;

        public TeachersRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Teachers teacher)
        {
            await _context.Teachers.AddAsync(teacher);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher != null)
            {
                _context.Teachers.Remove(teacher);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Teachers>> GetAllAsync()
        {
            return await _context.Teachers
                .Include(t => t.courses)
                .Include(t => t.course_Classes)
                .ToListAsync();
        }

        public async Task<Teachers> GetByIdAsync(int id)
        {
            return await _context.Teachers
                .Include(t => t.courses)
                .Include(t => t.course_Classes)
                .FirstOrDefaultAsync(t => t.id == id);
        }
        public async Task UpdateAsync(Teachers teacher)
        {
            _context.Teachers.Update(teacher);
            await _context.SaveChangesAsync();
        }
    }
}
