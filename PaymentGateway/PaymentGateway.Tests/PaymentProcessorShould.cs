using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using PaymentGateway.Contract;
using PaymentGateway.Payment;

namespace PaymentGateway.Tests
{
    [TestFixture]
    public class PaymentProcessorShould
    {
        private IPaymentProcessor _paymentProcessor;
        private Mock<IPaymentApiClient> _apiClient;
        private Mock<IPaymentRepository> _repository;
        private Mock<IPaymentMapper> _mapper;
        private Contract.Payment _newPayment;
        private PaymentResult _paymentResult;

        [SetUp]
        public void Setup()
        {
            _apiClient = new Mock<IPaymentApiClient>();
            _repository = new Mock<IPaymentRepository>();
            _mapper = new Mock<IPaymentMapper>();

            _paymentProcessor = new PaymentProcessor(_apiClient.Object, _repository.Object, _mapper.Object);

            _newPayment = new Contract.Payment();

            _paymentResult = new PaymentResult();
            _apiClient.Setup(c => c.SendPayment(_newPayment)).ReturnsAsync(_paymentResult);
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
        public async Task SavePayment()
        {
            var mappedPayment = new Database.PaymentDetails();
            _mapper.Setup(m => m.Map(_newPayment, _paymentResult)).Returns(mappedPayment);
            await _paymentProcessor.ProcessNewPayment(_newPayment);

            _repository.Verify(c => c.SavePayment(mappedPayment), Times.Once);
        }
    }
}
