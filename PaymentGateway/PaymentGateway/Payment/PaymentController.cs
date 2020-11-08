using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PaymentGateway.Contract;

namespace PaymentGateway.Payment
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentProcessor _paymentProcessor;

        public PaymentController(IPaymentProcessor paymentProcessor)
        {
            _paymentProcessor = paymentProcessor;
        }

        [HttpPost]
        public async Task<PaymentResult> ProcessPayment(Contract.Payment payment)
        {
            return await _paymentProcessor.ProcessNewPayment(payment);
        }

        [HttpGet("{id}")]
        public async Task<PaymentDetails> Get(int id)
        {
            return await _paymentProcessor.GetPayment(id);
        }
    }
}
