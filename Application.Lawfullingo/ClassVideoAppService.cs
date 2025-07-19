using ApplicationContract.Lawfullingo.Dto.ClassVideoDto;
using ApplicationContract.Lawfullingo.IApplicationService;
using AutoMapper;
using Data.Lawfullingo.Repository.ClassVideos;
using Entity.Lawfullingo;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Lawfullingo;

public class ClassVideoAppService : IClassVideoAppService
{
    private readonly IClassVideosRepository _repository;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _env;

    public ClassVideoAppService(IClassVideosRepository repository, IMapper mapper, IWebHostEnvironment env)
    {
        _repository = repository;
        _mapper = mapper;
        _env = env;
    }
    public async Task<IEnumerable<ClassVideoGetDto>> GetAllAsync()
    {
        var videos = await _repository.GetAllAsync();
        return videos.Select(v => new ClassVideoGetDto
        {
            Id = v.id,
            video_url = v.video_url,
            created_at = v.created_at
        });
    }
    

    public async Task<ClassVideoGetDto> GetByIdAsync(int id)
    {
        var video = await _repository.GetByIdAsync(id);
        return _mapper.Map<ClassVideoGetDto>(video);
    }

    public async Task AddAsync(ClassVideoCreateDto dto)
    {
        var video = _mapper.Map<Class_Videos>(dto);
        video.created_at = DateTime.UtcNow;

        await _repository.AddAsync(video);
    }

    public async Task UpdateAsync(ClassVideoUpdateDto dto)
    {
        var existing = await _repository.GetByIdAsync(dto.Id);
        if (existing != null)
        {
            existing.video_url = dto.video_url;
            await _repository.UpdateAsync(existing);
        }
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
    public async Task<ClassVideoGetDto> UploadAsync(ClassVideoCreateDto dto)
    {
        if (dto.VideoFile == null || dto.VideoFile.Length == 0)
            throw new ArgumentException("Invalid video file");

        string fileName = Guid.NewGuid() + Path.GetExtension(dto.VideoFile.FileName);
        string folder = Path.Combine(_env.WebRootPath, "videos");

        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);

        string path = Path.Combine(folder, fileName);

        using (var stream = new FileStream(path, FileMode.Create))
        {
            await dto.VideoFile.CopyToAsync(stream);
        }

        var entity = new Class_Videos
        {
            video_url = "/videos/" + fileName,
            created_at = DateTime.UtcNow
        };

        await _repository.AddAsync(entity);

        return new ClassVideoGetDto
        {
            Id = entity.id,
            video_url = entity.video_url,
            created_at = entity.created_at
        };
    }

    public async Task<List<ClassVideoOnlyDto>> GetUserPurchaseClassVideosAsync(int userId)
    {
      var classEntities = await _repository.GetUserClassVideosAsync(userId);
        var videoList = new List<ClassVideoOnlyDto>();

        foreach (var cc in classEntities)
        {
            if (cc.video_url != null && !string.IsNullOrEmpty(cc.video_url))
            {
                var dto = new ClassVideoOnlyDto
                {
                    video_url = cc.video_url
                };

                videoList.Add(dto);
            }
        }

        return videoList;
    }

}
