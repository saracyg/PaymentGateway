using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentGateway.Contract;

namespace PaymentGateway
{
    public interface IPaymentMapper
    {
        PaymentDetails Map(Contract.Payment payment, PaymentResult paymentResult);
    }

    public class PaymentMapper : IPaymentMapper
    {
        private readonly int _maskingStartIndex = 6;
        private readonly int _maskingNumberOfChars = 6;

        public PaymentDetails Map(Contract.Payment payment, PaymentResult paymentResult)
        {
            return new PaymentDetails
            {
                Amount = payment.Amount,
                CardholderName = payment.CardholderName,
                Currency = payment.Currency,
                ExpiryYear = payment.ExpiryYear,
                ExpiryMonth = payment.ExpiryMonth,
                Id = paymentResult.PaymentId,
                MaskedCardNumber = MaskCardNumber(payment.CardNumber),
                PaymentStatus = paymentResult.PaymentStatus
            };
        }

        private string MaskCardNumber(string cardNumber)
        {
            return cardNumber.Remove(_maskingStartIndex, _maskingNumberOfChars).Insert(_maskingStartIndex, new string('*', _maskingNumberOfChars));
        }
    }
}
