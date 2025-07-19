using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationContract.Lawfullingo.Dto.TeachersDto;

public class TeachersCreateDto
{
    public string TeacherName { get; set; }
    public string TeacherEmail { get; set; }
    public string Password { get; set; }
    public int Mobile { get; set; }
    public IFormFile? ProfileImage { get; set; }

    public string? ProfileImageUrl { get; set; }
    public string Gender { get; set; }
    public string Education { get; set; }
    public bool Status { get; set; }
    public bool IsVerified { get; set; }
}
