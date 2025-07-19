using Entity.Lawfullingo;
using Microsoft.AspNetCore.Http.HttpResults;
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

        public async Task<List<Class_Videos>> GetUserPurchaseClassVideosAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            var videos = await _context.Courses
                .Where(c => c.purchase.Any(p => p.user_id == userId))
                .SelectMany(c => c.course_Classes)
                .Where(cc => cc.Class_Videos != null)
                .Select(cc => cc.Class_Videos)
                .ToListAsync();

            return videos;
        }

        public Task<List<Course_Class>> GetUserClassVideosAsync(int CourseId)
        {
            var videosQuery = _context.Courses_Class
            .Include(v => v.Class_Videos)
            .Include(x=>x.course)
            .Where(x => x.course_id == CourseId).ToListAsync();
            return videosQuery;
        }
    }
}