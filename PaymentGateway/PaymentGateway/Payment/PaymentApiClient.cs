using System;
using PaymentGateway.Contract;

namespace PaymentGateway.Payment
{
    public interface IPaymentApiClient
    {
        PaymentResult SendPayment(Contract.Payment payment);
    }

    public class PaymentApiClient : IPaymentApiClient
    {
        public PaymentResult SendPayment(Contract.Payment payment)
        {
            throw new NotImplementedException();
        }
    }
}
