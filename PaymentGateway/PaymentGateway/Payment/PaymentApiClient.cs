using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PaymentGateway.ConfigOptions;
using PaymentGateway.Contract;

namespace PaymentGateway.Payment
{
    public interface IPaymentApiClient
    {
        Task<PaymentResult> SendPayment(Contract.Payment payment);
    }

    public class PaymentApiClient : IPaymentApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<PaymentApiClient> _logger;
        private readonly string _uri;

        public PaymentApiClient(IHttpClientFactory httpClientFactory, IOptions<PaymentApiOptions> config, ILogger<PaymentApiClient> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _uri = config.Value.Address;
        }

        public async Task<PaymentResult> SendPayment(Contract.Payment payment)
        {
            var content = new StringContent(
                JsonConvert.SerializeObject(payment),
                Encoding.UTF8,
                "application/json");

            using var client = _httpClientFactory.CreateClient();

            HttpResponseMessage response;
            try
            {
                _logger.Log(LogLevel.Debug, $"Sending POST to {_uri}");
                response = await client.PostAsync(_uri, content);
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, "Failed to communicate with acquiring bank", e);
                throw;
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                _logger.Log(LogLevel.Error, "Acquiring bank responded with unexpected status code", response);
                return null;
            }

            var jsonContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<PaymentResult>(jsonContent);

            return result;
        }
    }
}
