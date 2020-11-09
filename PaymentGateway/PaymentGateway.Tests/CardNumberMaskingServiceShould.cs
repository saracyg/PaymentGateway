using System;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using PaymentGateway.ConfigOptions;

namespace PaymentGateway.Tests
{
    [TestFixture]
    public class CardNumberMaskingServiceShould
    {
        private ICardNumberMaskingService _cardNumberMaskingService;
        private Mock<IOptions<CardMaskingRulesOptions>> _config;
        private readonly int _numberOfMaskedChars = 6;
        private readonly int _maskingStartIndex = 6;
        private readonly char _maskingChar = '*';
        private CardMaskingRulesOptions _mockedOptions;

        [SetUp]
        public void Setup()
        {
            _mockedOptions = new CardMaskingRulesOptions
            {
                MaskingChar = _maskingChar,
                MaskingStartIndex = _maskingStartIndex,
                NumberOfMaskedChars = _numberOfMaskedChars
            };
            _config = new Mock<IOptions<CardMaskingRulesOptions>>();
            _config.Setup(c => c.Value).Returns(_mockedOptions);
            _cardNumberMaskingService = new CardNumberMaskingService(_config.Object);
        }

        [Test]
        public void MaskCorrectNumber()
        {
            var cardNumber = "1234567812345678";
            var expectedResult = "123456******5678";
            
            var result = _cardNumberMaskingService.MaskCardNumber(cardNumber);

            result.Should().Be(expectedResult);
        }

        [Test]
        public void ThrowWhenCardNumberIsToShortForMasking()
        {
            var cardNumber = "123";

            Action act = () => _cardNumberMaskingService.MaskCardNumber(cardNumber);

            act.Should().Throw<ArgumentException>();
        }
    }
}
