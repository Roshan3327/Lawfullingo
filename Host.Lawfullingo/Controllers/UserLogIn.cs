using ApplicationContract.Lawfullingo.Common.Response;
using Azure;
using Data.Lawfullingo;
using Data.Lawfullingo.Repository.Logins;
using Entity.Lawfullingo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Host.Lawfullingo.Logins
{
    public class UserLogIn : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public UserLogIn(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [Route("api/checklogin")]
        [HttpPost]
        public async Task<ActionResult<ApiResponse<AuthenticationResponse>>> Login([FromBody] LoginDto login)
        {
            try
            {
                object user ;
                 user = await _dbContext.Users
                    .FirstOrDefaultAsync(u => u.user_email == login.Email && u.password == login.Password);
                string role = "";

                if (user == null)
                {
                    user = await _dbContext.Teachers
                        .Include(x=>x.teacherType)
           .FirstOrDefaultAsync(u => u.teacher_email == login.Email && u.password == login.Password);
                    if (user == null)
                    {
                        return Unauthorized(ApiResponse<AuthenticationResponse>.FailResponse("Invalid email or password"));
                    }
                    role = "Teacher";


                }
                else
                {
                    role = "User";
                }
                int userId = 0;
                string name = "", email = "";
                long? mobile = null;

                if (role == "User" && user is Users u)
                {
                    userId = u.id;
                    name = u.user_name;
                    email = u.user_email;
                    mobile = u.mobile;
                }
                else if (role == "Teacher" && user is Teachers t)
                {
                    userId = t.id;
                    name = t.teacher_name;
                    email = t.teacher_email;
                    mobile = t.mobile;
                    role = t.teacherType?.name; 
                }

                var claims = new List<Claim>
            {
            new Claim("Id", userId.ToString()),
            new Claim(ClaimTypes.Name, name),
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, role)

             };

                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsASuperLongSecretKeyWithAtLeast32Bytes"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    issuer: "https://localhost:5226",
                    audience: "https://localhost:5226",
                    claims: claims,
                    expires: DateTime.Now.AddDays(5),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                var response = new AuthenticationResponse
                {
                    Token = tokenString,
                    Profile = new UserProfileDto
                    {
                        UserId = userId,
                        UserName = name,
                        Email = email,
                        Mobile = mobile,
                    }
                }; return Ok(ApiResponse<AuthenticationResponse>.SuccessResponse(response, "Login successful"));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, ApiResponse<AuthenticationResponse>.FailResponse("Internal server error"));
            }
        }
    }
}