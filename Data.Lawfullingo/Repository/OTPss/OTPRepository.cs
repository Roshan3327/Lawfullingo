using Entity.Lawfullingo;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Data.Lawfullingo.Repository.OTPs
{
    public class OTPRepository : IOTPRepository
    {
        private readonly ApplicationDbContext _context;
        public OTPRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<OTP> CreateOtp(int userId)
        {
            int otp = GenerateUniqueOTP();

            DateTime creationTime = DateTime.Now;

            DateTime expiryTime = creationTime.AddDays(1);
            var otpEntity = new OTP
            {
                UserId = userId,
                Otp = otp,
                ForOtp = "Email",
                CreatedDate = creationTime,
                ExpirationTime = expiryTime,
                IsChecked=false,
                UserType="user"
            };
        await _context.OTPs.AddAsync(otpEntity);
          await  _context.SaveChangesAsync();
            return otpEntity;
        }
        public async Task<OTP> GetLastOtpByUserId(int userId)
        {
            var data = await _context.OTPs
                .Where(x => x.UserId == userId && x.ExpirationTime > DateTime.Now)
                .FirstOrDefaultAsync();
            return data;
        }
        public async Task<OTP> GetOtpByUserId(int userid)
        {
            var data = await _context.OTPs.Include(x => x.User)
               .Where(x => x.UserId == userid && !x.IsChecked).OrderBy(o => o.CreatedDate)
               .FirstOrDefaultAsync();
            return data;
        }

      

        public async Task<OTP> Update(int id,OTP otp)
        {
            var otps = await _context.OTPs.FindAsync(id);
            if (otps == null)
            {
                throw new Exception("Otp not found!");
            }
         var changedata=   _context.OTPs.Update(otp);
             await _context.SaveChangesAsync();
            return otps;
        }

        public async Task<OTP> VerifyOtp(int userId, int otp)
        {
            var data = await _context.OTPs.Include(x => x.User)
               .Where(x => x.Otp == otp && !x.IsChecked && x.UserId == userId)
               .FirstOrDefaultAsync();
            if (data == null)
            {
                throw new Exception("Otp already checked!");
            }
            if (data.ExpirationTime < DateTime.Now)
            {
               throw new Exception("Otp Expired");
            }


            return data;
        }




        private int GenerateUniqueOTP()
        {
            Random random = new Random();
            int otpValue = random.Next(10000, 99999);

            while (_context.OTPs.Any(o => o.Otp == otpValue))
            {
                otpValue = random.Next(10000, 99999);
            }
            return otpValue;
        }

    }
}
