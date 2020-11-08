using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace PaymentGateway.Contract
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class PaymentDetails
    {
        public int Id { get; set; }
        public string MaskedCardNumber { get; set; }
        public string CardholderName { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
    }
}
