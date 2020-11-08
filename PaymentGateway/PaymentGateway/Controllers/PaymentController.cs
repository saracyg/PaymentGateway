using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentGateway.Contract;
using PaymentGateway.Payment;

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
        public async Task<PaymentResult> ProcessPayment(Contract.Payment payment)
        {
            return await _paymentProcessor.ProcessNewPayment(payment);
        }

        [HttpGet]
        public async Task<PaymentDetails> Get(int id)
        {
            return await _paymentProcessor.GetPayment(id);
        }
    }
}
