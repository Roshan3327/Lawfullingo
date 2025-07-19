using ApplicationContract.Lawfullingo.Common.Response;
using ApplicationContract.Lawfullingo.Dto.DTOss;
using ApplicationContract.Lawfullingo.IApplicationService;
using ApplicationContract.Lawfullingo.IApplicationService.Services;
using Azure;
using Data.Lawfullingo.Repository.Logins;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Host.Lawfullingo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OtpController : ControllerBase
    {
        private readonly IOTPRepository _otpRepository;
        private readonly IUsersAppService _usersAppService;
        private readonly IEmailAppService _emailService;

        public OtpController(IOTPRepository otpRepository, IUsersAppService usersAppService, IEmailAppService emailService)
        {
            _otpRepository = otpRepository;
            _usersAppService = usersAppService;
            _emailService = emailService;
        }
        [HttpPost("email-otp-verify")]
        public async Task<IActionResult> EmailOtpVerify([FromBody] OtpVerficationDto dto)
        {
            if (dto.EmailOtp == 0|| dto.EmailOtp==null)
                return BadRequest(ApiResponse<string>.FailResponse("Email and OTP are required."));

            var user = await _usersAppService.GetByIdAsync(dto.UserId);
            if (user == null)
                return NotFound(ApiResponse<string>.FailResponse("User not found."));

            var otp = await _otpRepository.GetOtpByUserId(user.Id);
            if (otp == null)
                return BadRequest(ApiResponse<string>.FailResponse("Invalid or already used OTP."));

            if (otp.ExpirationTime < DateTime.Now)
                return BadRequest(ApiResponse<string>.FailResponse("OTP has expired."));

            otp.IsChecked = true;
          await  _otpRepository.Update(otp.Id, otp);   
            return Ok(ApiResponse<string>.SuccessResponse(user.UserEmail, $"Hello {user.UserName},Your Email Otp Verify successfully!"));

        }



        [HttpPost("send-email-otp")]
        public async Task<IActionResult> SendEmailOtp([FromBody] string email)
        {
            var user = await _usersAppService.GetUserByEmailAsync(email);
            if (string.IsNullOrWhiteSpace(email))
                return BadRequest(ApiResponse<string>.FailResponse("Email is required."));

            if (user == null)
                return NotFound(ApiResponse<string>.FailResponse("User not found."));

            if (user.isVerified==false)
                return BadRequest(ApiResponse<string>.FailResponse("User has been Not Verified!."));

            var otp = await _otpRepository.CreateOtp(user.id);

            string userName = $"{user.user_name} ";
            string subject = "Your Email OTP for Verification";

            string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Mails", "otpemail.html");
            if (!System.IO.File.Exists(templatePath))
                return StatusCode(500, "Email template not found.");

            string html = System.IO.File.ReadAllText(templatePath)
                            .Replace("{{user_name}}", userName)
                            .Replace("{{otp}}", $"Your Email OTP is: {otp.Otp}");

            await _emailService.SendEmailAsync(email, subject, html);

        return Ok(ApiResponse<string>.SuccessResponse(email, $"Otp send successfully on You Email { email}"));
        }


}
}
