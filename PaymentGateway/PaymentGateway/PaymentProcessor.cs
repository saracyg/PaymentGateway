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
        private readonly IPaymentApiClient _paymentApiClient;
        private readonly IPaymentRepository _paymentRepository;

        public PaymentProcessor(IPaymentApiClient paymentApiClient, IPaymentRepository paymentRepository)
        {
            _paymentApiClient = paymentApiClient;
            _paymentRepository = paymentRepository;
        }

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
