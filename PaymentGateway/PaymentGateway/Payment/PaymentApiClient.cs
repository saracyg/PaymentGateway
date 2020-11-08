using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
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
        private readonly string _uri;

        private readonly string _apiAddressKey = "PaymentApiAddress";

        public PaymentApiClient(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            _uri = config.GetValue<string>(_apiAddressKey);
        }

        public async Task<PaymentResult> SendPayment(Contract.Payment payment)
        {
            var content = new StringContent(
                JsonConvert.SerializeObject(payment),
                Encoding.UTF8,
                "application/json");

            using var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsync(_uri, content);

            var jsonContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<PaymentResult>(jsonContent);

            return result;
        }
    }
}
