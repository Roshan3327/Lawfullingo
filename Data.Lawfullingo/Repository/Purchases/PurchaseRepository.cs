using Entity.Lawfullingo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Lawfullingo.Repository.Purchases;

public class PurchaseRepository : IPurchaseRepository
{
   private readonly ApplicationDbContext _context;

    public PurchaseRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<List<Purchase>> GetCoursesByUserIdAsync(int userId)
    {
        return await _context.Purchase
            .Include(p => p.course)
            .Where(p => p.user_id == userId)
            .ToListAsync();
    }
    public async Task<IEnumerable<Purchase>> GetAllAsync()
    {
        return await _context.Purchase
                       .Include(p => p.users)
                       .Include(p => p.course)
                       .ToListAsync();
    }

    public async Task<Purchase> GetByIdAsync(int id)
    {
        return await _context.Purchase
                       .Include(p => p.users)
                       .Include(p => p.course)
                       .FirstOrDefaultAsync(p => p.id == id);
    }

    public async Task<Purchase> AddAsync(Purchase purchase)
    {
       await _context.Purchase.AddAsync(purchase);
       await _context.SaveChangesAsync();
        return purchase;
    }

    public async Task<string> UpdateAsync(Purchase purchase)
    {
        _context.Purchase.Update(purchase);
      await  _context.SaveChangesAsync();
        return "Purchase updated successfully";
    }

    public async Task<int> DeleteAsync(int id)
    {
        var purchase = await _context.Purchase.FindAsync(id);
        if (purchase != null)
        {
            _context.Purchase.Remove(purchase);
            await _context.SaveChangesAsync();
            return id ;
        }
        return 0;
    }

  

    public async Task<List<Purchase>> GetCoursesByUserIdCourseIdAsync(int userId, int courseId)
    {
        var purchases = await _context.Purchase
                         .Where(p => p.user_id == userId && p.course_id == courseId)
                         .Include(p => p.course)
                         .ThenInclude(cv => cv.course_Classes)
                         .ThenInclude(cc => cc.Class_Videos)
                         .ToListAsync();

        return purchases;
    }
}


     

