using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Lawfullingo;

public class Course_Class
{
    [Key]
    public int id { get; set; }
    public string name { get; set; }
    public int course_id { get; set; }
    public Course course { get; set; }
    public string description { get; set; }
    public string image_url { get; set; }

    public int video_id { get; set;}
    public Class_Videos Class_Videos { get; set; }
    public DateTime time { get; set; }
    public bool status { get; set; }
    public DateTime created_at { get; set; }
    public int teacher_id { get; set; }
    public Teachers Teachers { get; set; }
}