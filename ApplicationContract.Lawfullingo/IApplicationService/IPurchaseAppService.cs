using ApplicationContract.Lawfullingo.Dto.PurchaseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationContract.Lawfullingo.IApplicationService
{
    public interface IPurchaseAppService
    {
        Task<IEnumerable<PurchaseGetDto>> GetAllAsync();
        Task<PurchaseGetDto> GetByIdAsync(int id);
        Task AddAsync(PurchaseCreateDto dto);
        Task UpdateAsync(int id, PurchaseUpdateDto dto);
        Task DeleteAsync(int id);
        Task<IEnumerable<GetUserPurchaseCourseByUserIdDto>> GetCoursesByUserIdAsync(int userId);
    }
}
