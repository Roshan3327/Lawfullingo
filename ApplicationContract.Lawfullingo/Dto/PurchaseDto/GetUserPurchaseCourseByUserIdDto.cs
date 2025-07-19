using ApplicationContract.Lawfullingo.Dto.CourseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationContract.Lawfullingo.Dto.PurchaseDto
{
    public class GetUserPurchaseCourseByUserIdDto
    {
        public int id { get; set; }

        public DateTime PurchaseDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalDiscount { get; set; }
        public DateTime created_at { get; set; }

        public List<CourseGetDto> Courses { get; set; }
    }

}
