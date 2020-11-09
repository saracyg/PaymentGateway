using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace PaymentGateway.Contract
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class Payment
    {
        [Required, CreditCard]
        public string CardNumber { get; set; }
        [Required]
        public string CardholderName { get; set; }
        [Required, Range(1,12)]
        public int? ExpiryMonth { get; set; }
        [Required]
        public int? ExpiryYear { get; set; }
        [Required]
        public decimal? Amount { get; set; }
        [Required, MinLength(3), MaxLength(3)]
        public string Currency { get; set; }
        [Required]
        public int? Cvv { get; set; }
    }
}
