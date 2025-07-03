using Application.Lawfullingo;
using ApplicationContract.Lawfullingo;
using ApplicationContract.Lawfullingo.IApplicationService;
using Data.Lawfullingo;
using Data.Lawfullingo.Repository;
using Data.Lawfullingo.Repository.userses;
using Host.Lawfullingo.Profiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;


namespace Host.Lawfullingo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Connection string
            var connection = builder.Configuration.GetConnectionString("Default");
            builder.Services.AddDbContext<ApplicationDbContext>(
               x => x.UseSqlServer(connection));

            // Dependency Injection
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICategoryAppService, CategoryAppService>();
            builder.Services.AddScoped<IUsersRepository, UsersRepository>();
            builder.Services.AddScoped<IUsersAppService, UsersAppService>();


            // AutoMapper
            builder.Services.AddAutoMapper(typeof(CommonProfile).Assembly);

            // Controllers
            builder.Services.AddControllers();

            // ✅ Add Swagger services properly
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Lawfullingo API", Version = "v1" });
            });

            var app = builder.Build();

            // ✅ Enable Swagger middleware
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lawfullingo API v1");
                    c.RoutePrefix = "swagger"; // so URL is http://localhost:5195/swagger
                });
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
