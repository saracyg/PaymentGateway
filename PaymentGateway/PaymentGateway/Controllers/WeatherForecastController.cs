using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentGateway.Contract;

namespace PaymentGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
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
