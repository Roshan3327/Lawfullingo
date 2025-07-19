using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationContract.Lawfullingo.Dto.UsersDto
{
    public class UsersUpdateDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public int Mobile { get; set; }
        public string? ProfileImage { get; set; }
        public string Gender { get; set; }
        public DateTime UserDob { get; set; }
        public bool Status { get; set; }
        public string Education { get; set; }
        public bool IsVerified { get; set; }
    }
}
