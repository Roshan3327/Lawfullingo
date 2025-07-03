using ApplicationContract.Lawfullingo;
using ApplicationContract.Lawfullingo.Dto.UsersDto;
using AutoMapper;
using Data.Lawfullingo.Repository.userses;
using Entity.Lawfullingo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Lawfullingo;

public class UsersAppService :IUsersAppService
{
    private readonly IUsersRepository _repository;
    private readonly IMapper _mapper;

    public UsersAppService(IUsersRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UsersGetDto>> GetAllAsync()
    {
        var users = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<UsersGetDto>>(users);
    }

    public async Task<UsersGetDto> GetByIdAsync(int id)
    {
        var user = await _repository.GetByIdAsync(id);
        return _mapper.Map<UsersGetDto>(user);
    }

    public async Task AddAsync(UsersCreateDto dto)
    {

        var user = _mapper.Map<UsersCreateDto,Users>(dto);
        user.profile_image = dto.ProfileImage;
        user.created_at = DateTime.UtcNow;
        user.deleted_at = DateTime.MinValue;

        await _repository.AddAsync(user);

    }

    public async Task UpdateAsync(UsersUpdateDto dto)
    {
        var user = await _repository.GetByIdAsync(dto.Id);
        if (user != null)
        {
            _mapper.Map(dto, user);
            await _repository.UpdateAsync(user);
        }
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }

    

    
}

