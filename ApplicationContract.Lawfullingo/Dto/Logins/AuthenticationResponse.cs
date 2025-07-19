using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Lawfullingo.Repository.Logins
{
    public class AuthenticationResponse
    {
        public string Token { get; set; }

        public UserProfileDto Profile { get; set; }
    }

    public class UserProfileDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public long? Mobile { get; set; }
        public int UserId { get; set; }
    }
}
