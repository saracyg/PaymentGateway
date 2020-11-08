using System.Threading.Tasks;
using PaymentGateway.Contract;

namespace PaymentGateway.Payment
{
    public interface IPaymentProcessor
    {
        Task<PaymentResult> ProcessNewPayment(Contract.Payment payment);
        Task<PaymentDetails> GetPayment(int id);
    }

    public class PaymentProcessor : IPaymentProcessor
    {
        private readonly IPaymentApiClient _paymentApiClient;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IPaymentMapper _paymentMapper;

        public PaymentProcessor(IPaymentApiClient paymentApiClient, IPaymentRepository paymentRepository, IPaymentMapper paymentMapper)
        {
            _paymentApiClient = paymentApiClient;
            _paymentRepository = paymentRepository;
            _paymentMapper = paymentMapper;
        }

        public async Task<PaymentResult> ProcessNewPayment(Contract.Payment payment)
        {
            var result = await _paymentApiClient.SendPayment(payment);

            var paymentDetails = _paymentMapper.Map(payment, result);
            _paymentRepository.SavePayment(paymentDetails);

            return result;
        }

        public async Task<PaymentDetails> GetPayment(int id)
        {
            return new PaymentDetails();
        }
    }
}
