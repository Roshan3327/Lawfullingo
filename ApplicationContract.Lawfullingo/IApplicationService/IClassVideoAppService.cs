

using ApplicationContract.Lawfullingo.Dto.ClassVideoDto;
using Entity.Lawfullingo;

namespace ApplicationContract.Lawfullingo.IApplicationService;

public interface IClassVideoAppService
{
    Task<IEnumerable<ClassVideoGetDto>> GetAllAsync();
    Task<ClassVideoGetDto> GetByIdAsync(int id);
    Task AddAsync(ClassVideoCreateDto dto);
    Task UpdateAsync(ClassVideoUpdateDto dto);
    Task DeleteAsync(int id);
    Task<ClassVideoGetDto> UploadAsync(ClassVideoCreateDto dto);
    Task<List<ClassVideoOnlyDto>> GetUserPurchaseClassVideosAsync(int userId);


}
