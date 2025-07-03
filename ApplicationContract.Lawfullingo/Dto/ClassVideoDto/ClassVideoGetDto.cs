using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationContract.Lawfullingo.Dto.ClassVideoDto
{
    public class ClassVideoGetDto
    {
        public int Id { get; set; }
        public string VideoUrl { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
