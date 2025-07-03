using ApplicationContract.Lawfullingo.Dto.ClassVideoDto;
using ApplicationContract.Lawfullingo.IApplicationService;
using AutoMapper;
using Data.Lawfullingo.Repository.ClassVideos;
using Entity.Lawfullingo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Lawfullingo
{
    public class ClassVideoAppService : IClassVideoAppService
    {
        private readonly IClassVideosRepository _repository;
        private readonly IMapper _mapper;
        public ClassVideoAppService(IClassVideosRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ClassVideoGetDto>> GetAllAsync()
        {
            var videos = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ClassVideoGetDto>>(videos);
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
                existing.video_url = dto.VideoUrl;
                await _repository.UpdateAsync(existing);
            }
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }

}