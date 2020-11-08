using System;
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

        public PaymentController(ILogger<PaymentController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public PaymentResult ProcessPayment(Payment payment)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public PaymentDetails Get()
        {
            throw new NotImplementedException();
        }
    }
}
