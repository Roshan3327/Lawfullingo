﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationContract.Lawfullingo.Dto.UsersDto
{
    public class UpdateProfileImageDto
    {


        public IFormFile? ProfileImage { get; set; }
    }
}
