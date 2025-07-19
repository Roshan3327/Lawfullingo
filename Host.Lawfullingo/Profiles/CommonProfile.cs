using ApplicationContract.Lawfullingo.Dto.CategoryDto;
using ApplicationContract.Lawfullingo.Dto.ClassVideoDto;
using ApplicationContract.Lawfullingo.Dto.CourseDto;
using ApplicationContract.Lawfullingo.Dto.PurchaseDto;
using ApplicationContract.Lawfullingo.Dto.UsersDto;
using AutoMapper;
using Entity.Lawfullingo;

namespace Host.Lawfullingo.Profiles
{
    public class CommonProfile : Profile
    {
        public CommonProfile()
        {
            // ========== Category ==========
            CreateMap<Category, CategoryGetDto>();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));


            CreateMap<Users, UsersGetDto>();
            CreateMap<UsersCreateDto, Users>();
            CreateMap<UsersUpdateDto, Users>();
            CreateMap<UpdateProfileImageDto, Users>()
    .ForMember(dest => dest.profile_image, opt => opt.Ignore());


            CreateMap<Purchase, PurchaseGetDto>();
            CreateMap<PurchaseCreateDto, Purchase>();
            CreateMap<PurchaseUpdateDto, Purchase>();

            // ========== Class Videos ==========
            CreateMap<Class_Videos, ClassVideoGetDto>();
            CreateMap<ClassVideoCreateDto, Class_Videos>();
            CreateMap<ClassVideoUpdateDto, Class_Videos>();

            // ========== Course ==========
            // CreateDto → Course
            CreateMap<CourseCreateDto, Course>()
                .ForMember(dest => dest.course_image, opt => opt.Ignore()) // set manually after image upload
                .ForMember(dest => dest.teachers, opt => opt.Ignore())     // prevent assigning full navigation
                .ForMember(dest => dest.category, opt => opt.Ignore())     // prevent assigning full navigation
                .ForMember(dest => dest.created_at, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.discount_amount, opt => opt.MapFrom(src =>
                    (src.Course_Price * src.Discount_Percentage) / 100))
                .ForMember(dest => dest.purchase_amount, opt => opt.MapFrom(src =>
                    src.Course_Price - ((src.Course_Price * src.Discount_Percentage) / 100) + src.Charges_Amount - src.Coupon_Amount));

            // UpdateDto → Course
            CreateMap<CourseUpdateDto, Course>()
                .ForMember(dest => dest.course_image, opt => opt.Ignore()) // set manually if needed
                .ForMember(dest => dest.teachers, opt => opt.Ignore())
                .ForMember(dest => dest.category, opt => opt.Ignore())
                .ForMember(dest => dest.discount_amount, opt => opt.MapFrom(src =>
                    (src.Course_Price * src.Discount_Percentage) / 100))
                .ForMember(dest => dest.purchase_amount, opt => opt.MapFrom(src =>
                    src.Course_Price - ((src.Course_Price * src.Discount_Percentage) / 100) + src.Charges_Amount - src.Coupon_Amount));

            // Course → GetDto
            CreateMap<Course, CourseGetDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.category.name))
                .ForMember(dest => dest.TeacherName, opt => opt.MapFrom(src => src.teachers.teacher_name))
                .ForMember(dest => dest.Flag, opt => opt.MapFrom(src => src.flag.ToString()));


            CreateMap<Course, CourseGetDto>();
            CreateMap<Purchase, GetUserPurchaseCourseByUserIdDto>();
        }
    }
}
