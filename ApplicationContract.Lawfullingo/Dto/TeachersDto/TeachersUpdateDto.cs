using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationContract.Lawfullingo.Dto.TeachersDto
{
    public class TeachersUpdateDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public int Mobile { get; set; }
        public string ProfileImage { get; set; }
        public string Gender { get; set; }
        public string Education { get; set; }
        public bool Status { get; set; }
        public bool IsVerified { get; set; }
    }
}
