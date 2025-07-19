using Entity.Lawfullingo;

namespace Data.Lawfullingo.Repository.Purchases;

public interface IPurchaseRepository
{
    Task<IEnumerable<Purchase>> GetAllAsync();
    Task<Purchase> GetByIdAsync(int id);
    Task<Purchase> AddAsync(Purchase purchase);
    Task<string> UpdateAsync(Purchase purchase);
    Task<int> DeleteAsync(int id);
    Task<List<Purchase>> GetCoursesByUserIdAsync(int userId);
    Task<List<Purchase>> GetCoursesByUserIdCourseIdAsync(int userId ,int courseId);


}
