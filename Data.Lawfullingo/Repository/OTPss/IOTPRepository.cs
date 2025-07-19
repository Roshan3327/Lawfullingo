
using Entity.Lawfullingo;

    public  interface   IOTPRepository
    {
    Task<OTP> CreateOtp(int userId);
    Task<OTP> GetLastOtpByUserId(int userId);
    Task<OTP> GetOtpByUserId(int userid);
    Task<OTP> VerifyOtp(int userId, int otp);
    Task<OTP> Update(int id,OTP otp);

}
