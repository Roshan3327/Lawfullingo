using Entity.Lawfullingo;
using Microsoft.EntityFrameworkCore;
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

    public async Task AddAsync(Purchase purchase)
    {
       await _context.Purchase.AddAsync(purchase);
       await _context.SaveChangesAsync();
    }

    public void UpdateAsync(Purchase purchase)
    {
        _context.Purchase.Update(purchase);
        _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var purchase = await _context.Purchase.FindAsync(id);
        if (purchase != null)
        {
            _context.Purchase.Remove(purchase);
            await _context.SaveChangesAsync();
        }
    }

    Task IPurchaseRepository.UpdateAsync(Purchase purchase)
    {
        throw new NotImplementedException();
    }
}


     

