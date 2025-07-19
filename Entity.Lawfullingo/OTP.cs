using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Lawfullingo
{
    public class OTP
    {
        [Key]
        public int Id { get; set; }
        public int Otp { get; set; }
        public DateTime ExpirationTime { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool IsChecked { get; set; }
        public string? ForOtp { get; set; }
        public string? NewMobile { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public Users User { get; set; }
        public string UserType { get; set; } 
    }
}
