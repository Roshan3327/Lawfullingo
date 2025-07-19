using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Lawfullingo;

public class Class_Videos
{
    [Key]
    public int id { get; set; }
    public string video_url { get; set; }
    public DateTime created_at { get; set; }
}
