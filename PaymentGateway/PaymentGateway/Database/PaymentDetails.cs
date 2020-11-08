using MongoDB.Bson;
using PaymentGateway.Contract;

namespace PaymentGateway.Database
{
    public class PaymentDetails
    {
        public ObjectId Id { get; set; }
        public int PaymentId { get; set; }
        public string MaskedCardNumber { get; set; }
        public string CardholderName { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
    }
}
