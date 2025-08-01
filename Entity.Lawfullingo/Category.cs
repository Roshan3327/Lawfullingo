﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Lawfullingo;

public class Category
{
    [Key]
    public int id { get; set; }
    public string name { get; set; }
    public DateTime created_at { get; set; }
    public ICollection<Course> courses { get; set; } = new List<Course>();
} 
