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


        public async Task DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Users> GetUserByEmailAsync(string user_email)
        {
            return await _context.Users
       .FirstOrDefaultAsync(u => u.user_email == user_email);
        }

        public async Task<Users> GetUserByMobileAsync(long? mobile)
        {
            return await _context.Users
             .FirstOrDefaultAsync(u => u.mobile == mobile);
        }
        public async Task UpdateAsync(int id, Users user)
        {
            var existingUser = await _context.Users.FindAsync(id);

            if (existingUser == null)
                throw new Exception("User not found");
            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();
        }



    }                              
}


