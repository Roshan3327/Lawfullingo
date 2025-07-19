using ApplicationContract.Lawfullingo.Dto.PurchaseDto;
using ApplicationContract.Lawfullingo.IApplicationService;
using Azure;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Razorpay.Api;
using System.Runtime.Intrinsics.X86;
using static System.Net.Mime.MediaTypeNames;

namespace Host.Lawfullingo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseAppService _purchaseAppService;

        public PurchaseController(IPurchaseAppService purchaseAppService)
        {
            _purchaseAppService = purchaseAppService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePurchase([FromBody] PurchaseCreateDto dto)
        {
            try
            {
                // ✅ Razorpay test keys (replace with your real keys)
                string key = "rzp_test_gyMniVEV0gjzEZ";
                string secret = "s59A4UonGO5mu7qiZlHhdT71";
             
                // ✅ Initialize Razorpay Client
                RazorpayClient client = new RazorpayClient(key, secret);

                // ✅ Create order options
                var options = new Dictionary<string, object>
                {
                    { "amount", (int)(dto.CourseAmount * 100) }, // Amount in paise
                    { "currency", "INR" },
                    { "receipt", "receipt_" + Guid.NewGuid().ToString("N") },
                    { "payment_capture", 1 }
                };

                // ✅ Create order on Razorpay
                Order order = client.Order.Create(options);

                // ✅ (Optional) Save purchase locally
                await _purchaseAppService.AddAsync(dto);

                // ✅ Return order details to frontend
                return Ok(new
                {
                    OrderId = order["id"].ToString(),
                    Amount = order["amount"],
                    Currency = order["currency"],
                    Status = order["status"]
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Razorpay Error: {ex.Message}");
            }
        }
        [HttpGet("details/user-course/{userId}")]
        public async Task<IActionResult> GetPurchaseDetails(int userId)
        {
            var result = await _purchaseAppService.GetCoursesByUserIdAsync(userId);
            return Ok(result);
        }
    }
}





