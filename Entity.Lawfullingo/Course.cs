using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Lawfullingo;

public class Course
{
    [Key]
    public int id { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public DateTime start_date { get; set; }
    public DateTime end_date { get; set; }
    public string course_image { get; set; }
    public CourseFlag flag { get; set; }
    public string language { get; set; }
    public int contact_no { get; set; }
    public int whatsapp_no { get; set; }
    
    public int categoryId { get; set; }
    public Category category { get; set; }
    public bool status { get; set; }
    public decimal course_price { get; set; }
    public decimal purchase_amount { get; set; }
    public decimal discount_amount { get; set; }
    public decimal discount_percentage { get; set; }
    public decimal charges_amount { get; set; }
    public decimal coupon_amount { get; set; }
    public DateTime validity { get; set; }
    public DateTime created_at { get; set; }
    [ForeignKey("{teachersid}")]
    public int teachersid { get; set; }
    public Teachers teachers { get; set; }

    public ICollection<Purchase> purchase { get; set; } = new List<Purchase>();
    public ICollection<Course_Class> course_Classes { get; set; } = new List<Course_Class>();

}

