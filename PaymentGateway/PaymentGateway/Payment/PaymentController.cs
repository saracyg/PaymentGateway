using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentGateway.Contract;

namespace PaymentGateway.Payment
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentProcessor _paymentProcessor;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(IPaymentProcessor paymentProcessor, ILogger<PaymentController> logger)
        {
            _paymentProcessor = paymentProcessor;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<PaymentResult>> ProcessPayment(Contract.Payment payment)
        {
            _logger.Log(LogLevel.Debug, "Incoming POST request");
            var result = await _paymentProcessor.ProcessNewPayment(payment);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return result;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDetails>> Get(int id)
        {
            _logger.Log(LogLevel.Debug, "Incoming GET request", id);
            var result = await _paymentProcessor.GetPayment(id);

            if (result == null)
            {
                return NotFound();
            }

            return result;
        }
    }
}
