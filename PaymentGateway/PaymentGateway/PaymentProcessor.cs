using PaymentGateway.Contract;

namespace PaymentGateway
{
    public interface IPaymentProcessor
    {
        PaymentResult ProcessNewPayment(Payment payment);
        PaymentDetails GetPayment(in int id);
    }

    public class PaymentProcessor : IPaymentProcessor
    {
        public PaymentResult ProcessNewPayment(Payment payment)
        {
            throw new System.NotImplementedException();
        }

        public PaymentDetails GetPayment(in int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
