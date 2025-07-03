using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationContract.Lawfullingo.Dto.PurchaseDto
{
    public class PurchaseCreateDto
    {
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal CourseAmount { get; set; }
        public decimal CourseDiscount { get; set; }
    }
}
