using System;
using PaymentGateway.Contract;

namespace PaymentGateway
{
    public interface IPaymentApiClient
    {
        PaymentResult SendPayment(Payment payment);
    }

    public class PaymentApiClient : IPaymentApiClient
    {
        public PaymentResult SendPayment(Payment payment)
        {
            throw new NotImplementedException();
        }
    }
}
