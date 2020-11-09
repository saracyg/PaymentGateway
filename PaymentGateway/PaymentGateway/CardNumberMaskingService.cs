using System;
using Microsoft.Extensions.Options;
using PaymentGateway.ConfigOptions;

namespace PaymentGateway
{
    public interface ICardNumberMaskingService
    {
        string MaskCardNumber(string cardNumber);
    }

    public class CardNumberMaskingService : ICardNumberMaskingService
    {
        public CardNumberMaskingService(IOptions<CardMaskingRulesOptions> config)
        {
            _maskingStartIndex = config.Value.MaskingStartIndex;
            _numberOfMaskedChars = config.Value.NumberOfMaskedChars;
            _maskingChar = config.Value.MaskingChar;
        }

        private readonly int _maskingStartIndex;
        private readonly int _numberOfMaskedChars;
        private readonly char _maskingChar;
        
        public string MaskCardNumber(string cardNumber)
        {
            if (cardNumber == null || cardNumber.Length < _maskingStartIndex + _numberOfMaskedChars)
            {
                throw new ArgumentException("Card number is too short for masking.");
            }
            return cardNumber
                .Remove(_maskingStartIndex, _numberOfMaskedChars)
                .Insert(_maskingStartIndex, new string(_maskingChar, _numberOfMaskedChars));
        }
    }
}
