using System;
using PaymentGateway.Contract;

namespace PaymentGateway.Payment
{
    public interface IPaymentRepository
    {
        bool SavePayment(PaymentDetails payment);
        bool GetPayment(int id);
    }

    public class PaymentRepository : IPaymentRepository
    {
        public bool SavePayment(PaymentDetails payment)
        {
            return false;
        }

        public bool GetPayment(int id)
        {
            throw new NotImplementedException();
        }
    }
}
