﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationContract.Lawfullingo.Dto.TeachersDto
{
    public class UpdateTeacherProfileImageDto
    {


        public IFormFile? ProfileImage { get; set; }
    }
}
