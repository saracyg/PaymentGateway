using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using PaymentGateway.Contract;
using PaymentGateway.Payment;
using PaymentDetails = PaymentGateway.Database.PaymentDetails;

namespace PaymentGateway.Tests
{
    [TestFixture]
    public class PaymentProcessorShould
    {
        private IPaymentProcessor _paymentProcessor;
        private Mock<IPaymentApiClient> _apiClient;
        private Mock<IPaymentRepository> _repository;
        private Mock<IMapper> _mapper;
        private Mock<ICardNumberMaskingService> _cardNumberMaskingService;
        private Contract.Payment _newPayment;
        private PaymentResult _paymentResult;
        private PaymentDetails _mappedPayment;
        private string _maskedCardNumber;

        [SetUp]
        public void Setup()
        {
            _apiClient = new Mock<IPaymentApiClient>();
            _repository = new Mock<IPaymentRepository>();
            _cardNumberMaskingService = new Mock<ICardNumberMaskingService>();
            _mapper = new Mock<IMapper>();

            _paymentProcessor = new PaymentProcessor(_apiClient.Object, _repository.Object, _mapper.Object, _cardNumberMaskingService.Object, new Mock<ILogger<PaymentProcessor>>().Object);

            _newPayment = new Contract.Payment();

            _paymentResult = new PaymentResult();
            _apiClient.Setup(c => c.SendPayment(_newPayment)).ReturnsAsync(_paymentResult);

            _mappedPayment = new PaymentDetails();
            _mapper.Setup(m => m.Map<PaymentDetails>(_newPayment)).Returns(_mappedPayment);

            _maskedCardNumber = "1234********1234";
            _cardNumberMaskingService.Setup(s => s.MaskCardNumber(_newPayment.CardNumber)).Returns(_maskedCardNumber);
        }

        [Test]
        public async Task SendRequestThroughApiClient()
        {
            await _paymentProcessor.ProcessNewPayment(_newPayment);

            _apiClient.Verify(c => c.SendPayment(_newPayment), Times.Once);
        }

        [Test]
        public async Task ReturnResultFromApiClient()
        {
            var result = await _paymentProcessor.ProcessNewPayment(_newPayment);

            result.Should().Be(_paymentResult);
        }

        [Test]
        public async Task SavePaymentDetails()
        {
            _mappedPayment.MaskedCardNumber = _maskedCardNumber;
            _mappedPayment.PaymentStatus = _paymentResult.PaymentStatus;
            _mappedPayment.PaymentId = _paymentResult.PaymentId;

            await _paymentProcessor.ProcessNewPayment(_newPayment);

            _repository.Verify(c => c.SavePayment(_mappedPayment), Times.Once);
        }

        [Test]
        public async Task GetPaymentDetailsFromStorage()
        {
            var id = 21;

            await _paymentProcessor.GetPayment(id);

            _repository.Verify(c => c.GetPayment(id), Times.Once);
        }

        [Test]
        public async Task ReturnPaymentDetails()
        {
            var id = 21;
            var databasePaymentDetails = new PaymentDetails();
            var expectedPaymentDetails = new Contract.PaymentDetails();
            _repository.Setup(r => r.GetPayment(id)).ReturnsAsync(databasePaymentDetails);
            _mapper.Setup(m => m.Map<Contract.PaymentDetails>(databasePaymentDetails)).Returns(expectedPaymentDetails);

            var result = await _paymentProcessor.GetPayment(id);

            result.Should().Be(expectedPaymentDetails);
        }
    }
}
