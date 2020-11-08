using System;
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
            var random = new Random();
            var possibleEnumValues = Enum.GetValues(typeof(PaymentStatus));

            return
                new PaymentResult
                {
                    PaymentId = random.Next(),
                    PaymentStatus = (PaymentStatus) possibleEnumValues.GetValue(random.Next(possibleEnumValues.Length))
                };
        }
    }
}
