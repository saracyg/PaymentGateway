using Microsoft.AspNetCore.Mvc;
using PaymentGateway.Contract;

namespace BankApiMock.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        [HttpPost]
        public PaymentResult ProcessPayment(Payment payment)
        {
            return
                new PaymentResult
                {
                    PaymentId = 23,
                    PaymentStatus = PaymentStatus.Succeeded
                };
        }
    }
}
