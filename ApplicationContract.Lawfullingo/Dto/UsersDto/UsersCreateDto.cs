using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationContract.Lawfullingo.Dto.UsersDto
{
    public class UsersCreateDto
    {
        public string user_name { get; set; }
        public string user_email { get; set; }
        public string Password { get; set; }
        public long? Mobile { get; set; }
        public IFormFile? ProfileImage { get; set; }

        public string? ProfileImageUrl { get; set; }
        public string Gender { get; set; }
        public DateTime UserDob { get; set; }
        public string Education { get; set; }
        public bool IsVerified { get; set; }
    }
}
