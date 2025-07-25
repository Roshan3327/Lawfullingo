﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Lawfullingo;

public class Teachers
{
    [Key]
    public int id { get; set; }
    public string teacher_name { get; set; } = null;
    public string teacher_email { get; set; }
    public string password { get; set; }
    public long? mobile { get; set; }
    public string profile_image { get; set; } = null;
    public string gender { get; set; }
    public bool status { get; set; }
    public string education { get; set; }
    public bool isVerified { get; set; }
    public int teacherTypeId { get; set; }
    public TeacherType teacherType { get; set; }
    public DateTime deleted_at { get; set; }
    public DateTime created_at { get; set; }

    public ICollection<Course> courses { get; set; } = new List<Course>();
    public ICollection<Course_Class> course_Classes { get; set; } = new List<Course_Class>();

}