using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationContract.Lawfullingo.Dto.ClassVideoDto
{
    public class ClassVideoCreateDto
    {
        public string video_url { get; set; }
        public IFormFile VideoFile { get; set; }
    }
}
