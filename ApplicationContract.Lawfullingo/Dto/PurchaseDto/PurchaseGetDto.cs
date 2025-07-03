using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationContract.Lawfullingo.Dto.PurchaseDto
{
    public class PurchaseGetDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }   // from Users
        public string CourseName { get; set; } // from Course
        public DateTime PurchaseDate { get; set; }
        public decimal CourseAmount { get; set; }
        public decimal CourseDiscount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
