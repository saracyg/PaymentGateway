using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace PaymentGateway.Contract
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class PaymentResult
    {
        public int? PaymentId { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
    }

    public enum PaymentStatus
    {
        Succeeded,
        Failed
    }
}
