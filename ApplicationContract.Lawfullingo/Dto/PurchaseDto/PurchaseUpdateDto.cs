 

namespace ApplicationContract.Lawfullingo.Dto.PurchaseDto
{
    public class PurchaseUpdateDto 
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal CourseAmount { get; set; }
        public decimal CourseDiscount { get; set; }

    }
}