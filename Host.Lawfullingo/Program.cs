using Application.Lawfullingo;
using Application.Lawfullingo.Services;
using Application.Lawfullingo.Services.SMTPS;
using ApplicationContract.Lawfullingo.IApplicationService;
using ApplicationContract.Lawfullingo.IApplicationService.Services;
using Data.Lawfullingo;
using Data.Lawfullingo.Repository.Categories;
using Data.Lawfullingo.Repository.ClassVideos;
using Data.Lawfullingo.Repository.Courses;
using Data.Lawfullingo.Repository.OTPs;
using Data.Lawfullingo.Repository.Purchases;
using Data.Lawfullingo.Repository.Teacheres;
using Data.Lawfullingo.Repository.userses;
using Host.Lawfullingo.Profiles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;


namespace Host.Lawfullingo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            builder.Services.AddHttpContextAccessor();

            // CORS configuration
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin", policy =>
                    policy.AllowAnyHeader()
                          .AllowAnyMethod()
                          .SetIsOriginAllowed(_ => true)
                          .AllowCredentials());
            });

            // JWT Authentication
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "https://localhost:5225",
                        ValidAudience = "https://localhost:5225",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"))
                    };
                });

            // Swagger configuration
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Lawfullingo API", Version = "v1" });

                c.CustomOperationIds(apiDesc =>
                {
                    return apiDesc.TryGetMethodInfo(out MethodInfo methodInfo) ? methodInfo.Name : null;
                });

                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Enter 'Bearer {token}'",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                c.AddSecurityDefinition("Bearer", securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
            });

            // Connection string
            var connection = builder.Configuration.GetConnectionString("Default");
            builder.Services.AddDbContext<ApplicationDbContext>(
               x => x.UseSqlServer(connection));
            builder.Services.AddHttpContextAccessor();

            // Dependency Injection

            builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));

            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICategoryAppService, CategoryAppService>();
            builder.Services.AddScoped<IUsersRepository, UsersRepository>();
            builder.Services.AddScoped<IUsersAppService, UsersAppService>();
            builder.Services.AddScoped<IClassVideosRepository, ClassVideosRepository>();
            builder.Services.AddScoped<IClassVideoAppService, ClassVideoAppService>();
            builder.Services.AddScoped<ICourseRepository, CourseRepository>();
            builder.Services.AddScoped<ICourseAppService, CourseAppService>();
            builder.Services.AddScoped<IPurchaseAppService, PurchaseAppService>();
            builder.Services.AddScoped<IPurchaseRepository, PurchaseRepository>();
            builder.Services.AddScoped<ITeachersAppService, TeachersAppService>();
            builder.Services.AddScoped<ITeachersRepository, TeachersRepository>();
            builder.Services.AddScoped<IEmailAppService, EmailAppService>();
            builder.Services.AddScoped<IOTPRepository, OTPRepository>();

            builder.Services.AddHttpClient();

            // AutoMapper
            builder.Services.AddAutoMapper(typeof(CommonProfile).Assembly);

            // Controllers
            builder.Services.AddControllers();

            // Swagger support
            builder.Services.AddEndpointsApiExplorer();

            var app = builder.Build();

            // Swagger middleware
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lawfullingo API v1");
                    c.RoutePrefix = "swagger";   
                });
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();          // For serving wwwroot content like images
            //app.UseHsts();

            app.UseCors("AllowOrigin");

            app.UseAuthentication();       // Use Authentication BEFORE Authorization
            app.UseAuthorization();

            app.MapControllers();          // Register API controllers

            app.Run();
        }
    }
}
