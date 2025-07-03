using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Lawfullingo;


public class Purchase
{
    public int id { get; set; }
    public int user_id { get; set; }
    public Users users { get; set; }
    public int course_id { get; set; }
    public Course course { get; set; }
    public DateTime purchase_date { get; set; }
    public decimal course_amount { get; set; }
    public decimal course_discount { get; set; }
    public DateTime created_at { get; set; }
}
