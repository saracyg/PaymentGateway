namespace PaymentGateway
{
    public interface ICardNumberMaskingService
    {
        string MaskCardNumber(string cardNumber);
    }

    public class CardNumberMaskingService : ICardNumberMaskingService
    {
        private readonly int _maskingStartIndex = 6;
        private readonly int _maskingNumberOfChars = 6;
        
        public string MaskCardNumber(string cardNumber)
        {
            return cardNumber.Remove(_maskingStartIndex, _maskingNumberOfChars).Insert(_maskingStartIndex, new string('*', _maskingNumberOfChars));
        }
    }
}
