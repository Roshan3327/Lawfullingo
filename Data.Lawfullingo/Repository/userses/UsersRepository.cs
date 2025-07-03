using Entity.Lawfullingo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Lawfullingo.Repository.userses
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDbContext _context;

        public UsersRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Users>> GetAllAsync()
        {
            return await _context.Users
                .Include(u => u.purchases)
                .ToListAsync();
        }

        public async Task<Users> GetByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.purchases)
                .FirstOrDefaultAsync(u => u.id == id);
        }

        public async Task AddAsync(Users user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Users user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

      
    }
}

