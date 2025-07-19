using Entity.Lawfullingo;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationContract.Lawfullingo.Dto.CourseDto;

public class CourseCreateDto
{

    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Start_Date { get; set; }
    public DateTime End_Date { get; set; }

    public IFormFile Course_Image { get; set; } // Image file upload
    public string Language { get; set; }
    public int Contact_No { get; set; }
    public int Whatsapp_No { get; set; }

    public int CategoryId { get; set; }
    public bool Status { get; set; }

    public decimal Course_Price { get; set; }
    public decimal Discount_Percentage { get; set; }
    public decimal Charges_Amount { get; set; }
    public decimal Coupon_Amount { get; set; }

    public DateTime Validity { get; set; }
    public int teachersid { get; set; } 

    public CourseFlag Flag { get; set; }
}




