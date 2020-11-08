using FluentAssertions;
using Moq;
using NUnit.Framework;
using PaymentGateway.Contract;

namespace PaymentGateway.Tests
{
    [TestFixture]
    public class PaymentProcessorShould
    {
        private IPaymentProcessor _paymentProcessor;
        private Mock<IPaymentApiClient> _apiClient;
        private Mock<IPaymentRepository> _repository;
        private Payment _newPayment;

        [SetUp]
        public void Setup()
        {
            _apiClient = new Mock<IPaymentApiClient>();
            _repository = new Mock<IPaymentRepository>();

            _paymentProcessor = new PaymentProcessor(_apiClient.Object, _repository.Object);

            _newPayment = new Payment();
        }

        [Test]
        public void SendRequestThroughApiClient()
        {
            _paymentProcessor.ProcessNewPayment(_newPayment);

            _apiClient.Verify(c => c.SendPayment(_newPayment), Times.Once);
        }

        [Test]
        public void ReturnResultFromApiClient()
        {
            var expectedResult = new PaymentResult();
            _apiClient.Setup(c => c.SendPayment(_newPayment)).Returns(expectedResult);

            var result = _paymentProcessor.ProcessNewPayment(_newPayment);

            result.Should().Be(expectedResult);
        }

        [Test]
        public void SavePayment()
        {
            _paymentProcessor.ProcessNewPayment(_newPayment);

            _repository.Verify(c => c.SavePayment(It.IsAny<PaymentDetails>()), Times.Once);
        }
    }
}
