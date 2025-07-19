using ApplicationContract.Lawfullingo.Dto.PurchaseDto;
using ApplicationContract.Lawfullingo.IApplicationService;
using AutoMapper;
using Data.Lawfullingo.Repository.Purchases;
using Entity.Lawfullingo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Lawfullingo
{
    public class PurchaseAppService : IPurchaseAppService
    {
        private readonly IPurchaseRepository _repository;
        private readonly IMapper _mapper;

        public PurchaseAppService(IPurchaseRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetUserPurchaseCourseByUserIdDto>> GetCoursesByUserIdAsync(int userId)
        {
            var purchases = await _repository.GetCoursesByUserIdAsync(userId);

            var result = _mapper.Map<IEnumerable<GetUserPurchaseCourseByUserIdDto>>(purchases);
            return result;
        }
        public async Task<IEnumerable<PurchaseGetDto>> GetAllAsync()
        {
            var purchases = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<PurchaseGetDto>>(purchases);
        }

        public async Task<PurchaseGetDto> GetByIdAsync(int id)
        {
            var purchase = await _repository.GetByIdAsync(id);
            if (purchase == null)
                throw new Exception("Purchase not found");

            return _mapper.Map<PurchaseGetDto>(purchase);
        }

        public async Task AddAsync(PurchaseCreateDto dto)
        {
            var purchase = _mapper.Map<Purchase>(dto);
            purchase.purchase_date = DateTime.Now;
            await _repository.AddAsync(purchase);
        }

        public async Task UpdateAsync(int id, PurchaseUpdateDto dto)
        {
            var existingPurchase = await _repository.GetByIdAsync(id);
            if (existingPurchase == null)
                throw new Exception("Purchase not found");

         var mappedData=   _mapper.Map(dto, existingPurchase);
           

            await _repository.UpdateAsync(existingPurchase);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
