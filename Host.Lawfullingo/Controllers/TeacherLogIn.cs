using Data.Lawfullingo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.EntityFrameworkCore;
using Data.Lawfullingo.Repository.Logins;

namespace Host.Lawfullingo.Logins
{
    public class TeacherLogIn : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public TeacherLogIn(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [Route("api/checkTeacherlogin")]
        [HttpPost]
        public async Task<ActionResult> Login([FromBody] LoginDto login)
        {
            try
            {
                var teachers = await _dbContext.Teachers
                    .FirstOrDefaultAsync(u => u.teacher_email == login.Email && u.password == login.Password);
                if (teachers == null)
                {
                    return Unauthorized("Invalid email or password");
                }

                var claims = new List<Claim>
            {
            new Claim("Id", teachers.id.ToString()),
            new Claim(ClaimTypes.Name, teachers.teacher_name),
            new Claim(ClaimTypes.Email, teachers.teacher_email),
            new Claim(ClaimTypes.Role,teachers.teacherType.name)

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
                return Ok(new AuthenticationResponse { Token = tokenString });

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}