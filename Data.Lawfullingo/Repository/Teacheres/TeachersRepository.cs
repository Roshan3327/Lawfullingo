using Entity.Lawfullingo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Lawfullingo.Repository.Teacheres
{
    public class TeachersRepository:ITeachersRepository
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

        // Assuming _context is your ApplicationDbContext and it has DbSet<Teachers> Teachers

        public async Task<Teachers> GetTeacherByEmailAsync(string teacher_email)
        {
            return await _context.Teachers
                .Include(t => t.teacherType)
                .FirstOrDefaultAsync(t => t.teacher_email == teacher_email);
        }

        public async Task<Teachers> GetTeacherByMobileAsync(long? mobile)
        {
            return await _context.Teachers
                .FirstOrDefaultAsync(t => t.mobile == mobile);
        }

        public async Task UpdateAsync(int id, Teachers teacher)
        {
            var existingTeacher = await _context.Teachers.FindAsync(id);

            if (existingTeacher == null)
                throw new Exception("Teacher not found");

            _context.Teachers.Update(existingTeacher);
            await _context.SaveChangesAsync();
        }

    }
}


