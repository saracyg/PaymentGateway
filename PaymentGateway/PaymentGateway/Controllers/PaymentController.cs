using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentGateway.Contract;

namespace PaymentGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly IPaymentProcessor _paymentProcessor;

        public PaymentController(ILogger<PaymentController> logger, IPaymentProcessor paymentProcessor)
        {
            _logger = logger;
            _paymentProcessor = paymentProcessor;
        }

        [HttpPost]
        public PaymentResult ProcessPayment(Payment payment)
        {
            return _paymentProcessor.ProcessNewPayment(payment);
        }

        [HttpGet]
        public PaymentDetails Get(int id)
        {
            return _paymentProcessor.GetPayment(id);
        }
    }
}
