using ApplicationContract.Lawfullingo.Dto.UsersDto;
using ApplicationContract.Lawfullingo.IApplicationService;
using AutoMapper;
using Data.Lawfullingo.Repository.userses;
using Entity.Lawfullingo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Lawfullingo;

public class UsersAppService : IUsersAppService
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

        var user = _mapper.Map<UsersCreateDto, Users>(dto);
        user.status = true;
        user.profile_image = dto.ProfileImageUrl;
        user.created_at = DateTime.UtcNow;
        user.deleted_at = DateTime.MinValue;

        await _repository.AddAsync(user);

    }

    public async Task UpdateAsync(int id, UsersUpdateDto dto)
    {
        var user = await _repository.GetByIdAsync(id);
        if (user != null)
        {
            _mapper.Map(dto, user);
            await _repository.UpdateAsync(id, user);
        }
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task<Users> GetUserByEmailAsync(string user_email)
    {
        return await _repository.GetUserByEmailAsync(user_email);
    }

    public async Task<Users> GetUserByMobileAsync(long? mobile)
    {
        return await _repository.GetUserByMobileAsync(mobile);
    }

    public async Task<string> UploadProfileImageAsync(int id, string imageUrl)
    {
        var user = await _repository.GetByIdAsync(id);
        if (user == null)
            throw new Exception("User not found");
        user.profile_image = imageUrl;
        await _repository.UpdateAsync(id, user);

        return user.profile_image;
    }

}

