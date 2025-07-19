using ApplicationContract.Lawfullingo.Dto.TeachersDto;
using ApplicationContract.Lawfullingo.IApplicationService;
using AutoMapper;
using Data.Lawfullingo.Repository.Teacheres;
using Entity.Lawfullingo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Lawfullingo;

public class TeachersAppService : ITeachersAppService
{
    private readonly ITeachersRepository _repository;
    private readonly IMapper _mapper;

    public TeachersAppService(ITeachersRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TeachersGetDto>> GetAllAsync()
    {
        var Teachers = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<TeachersGetDto>>(Teachers);
    }

    public async Task<TeachersGetDto> GetByIdAsync(int id)
    {
        var Teacher = await _repository.GetByIdAsync(id);
        return _mapper.Map<TeachersGetDto>(Teacher);
    }

    public async Task AddAsync(TeachersCreateDto dto)
    {

        var Teacher = _mapper.Map<TeachersCreateDto, Teachers>(dto);
        Teacher.status = true;
        Teacher.profile_image = dto.ProfileImageUrl;
        Teacher.created_at = DateTime.UtcNow;
        Teacher.deleted_at = DateTime.MinValue;

        await _repository.AddAsync(Teacher);

    }

    public async Task UpdateAsync(int id, TeachersUpdateDto dto)
    {
        var Teacher = await _repository.GetByIdAsync(id);
        if (Teacher != null)
        {
            _mapper.Map(dto, Teacher);
            await _repository.UpdateAsync(id, Teacher);
        }
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task<Teachers> GetTeacherByEmailAsync(string Teacher_email)
    {
        return await _repository.GetTeacherByEmailAsync(Teacher_email);
    }

    public async Task<Teachers> GetTeacherByMobileAsync(long? mobile)
    {
        return await _repository.GetTeacherByMobileAsync(mobile);
    }

    public async Task<string> UploadProfileImageAsync(int id, string imageUrl)
    {
        var Teacher = await _repository.GetByIdAsync(id);
        if (Teacher == null)
            throw new Exception("Teacher not found");
        Teacher.profile_image = imageUrl;
        await _repository.UpdateAsync(id, Teacher);

        return Teacher.profile_image;
    }

}

