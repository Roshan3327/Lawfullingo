using ApplicationContract.Lawfullingo.Dto.CategoryDto;
using ApplicationContract.Lawfullingo.IApplicationService;
using AutoMapper;
using Data.Lawfullingo.Repository.Categories;
using Entity.Lawfullingo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Lawfullingo;

public class CategoryAppService : ICategoryAppService
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;

    public CategoryAppService(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryGetDto>> GetAllAsync()
    {
        var categories = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<CategoryGetDto>>(categories);
    }

    public async Task<CategoryGetDto> GetByIdAsync(int id)
    {
        var category = await _repository.GetByIdAsync(id);
        return _mapper.Map<CategoryGetDto>(category);
    }

    public async Task AddAsync(CategoryCreateDto dto)
    {
        var category = _mapper.Map<Category>(dto);
        category.created_at = DateTime.UtcNow;

        await _repository.AddAsync(category);
    }

    public async Task UpdateAsync(CategoryUpdateDto dto)
    {
        var category = await _repository.GetByIdAsync(dto.Id);
        if (category != null)
        {
            _mapper.Map(dto, category); // update only changed fields
            await _repository.UpdateAsync(category);
        }
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}

