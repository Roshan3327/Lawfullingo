using ApplicationContract.Lawfullingo.Dto.CategoryDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationContract.Lawfullingo.IApplicationService;

public interface ICategoryAppService
{
    Task<IEnumerable<CategoryGetDto>> GetAllAsync();
    Task<CategoryGetDto> GetByIdAsync(int id);
    Task AddAsync(CategoryCreateDto dto);
    Task UpdateAsync(CategoryUpdateDto dto);
    Task DeleteAsync(int id);
}
