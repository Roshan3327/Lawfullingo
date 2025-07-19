using Entity.Lawfullingo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationContract.Lawfullingo.Dto.CourseDto;

public class CourseGetDto
{
   
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Course_Image { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
        public string Language { get; set; }

        public int Contact_No { get; set; }
        public int Whatsapp_No { get; set; }
        public bool Status { get; set; }

        public decimal Course_Price { get; set; }
        public decimal Purchase_Amount { get; set; }
        public decimal Discount_Amount { get; set; }
        public decimal Discount_Percentage { get; set; }
        public decimal Charges_Amount { get; set; }
        public decimal Coupon_Amount { get; set; }

        public DateTime Validity { get; set; }
        public DateTime Created_At { get; set; }

        public string CategoryName { get; set; }
        public string TeacherName { get; set; }

    public CourseFlag Flag { get; set; }
}





