using ApplicationContract.Lawfullingo.Dto.CategoryDto;
using ApplicationContract.Lawfullingo.Dto.UsersDto;
using AutoMapper;
using Entity.Lawfullingo;

namespace Host.Lawfullingo.Profiles
{
    public class CommonProfile : Profile
    {
        public CommonProfile()
        {
            CreateMap<Category, CategoryGetDto>();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>()

           .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Users, UsersGetDto>();
            CreateMap<UsersCreateDto, Users>();
            CreateMap<UsersUpdateDto, Users>();
        }

    }
}
