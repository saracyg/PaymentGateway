using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public PaymentProcessor(IPaymentApiClient paymentApiClient, IPaymentRepository paymentRepository, IMapper mapper)
        {
            _paymentApiClient = paymentApiClient;
            _paymentRepository = paymentRepository;
            _mapper = mapper;
        }

        public async Task<PaymentResult> ProcessNewPayment(Contract.Payment payment)
        {
            var result = await _paymentApiClient.SendPayment(payment);

            var paymentDetails = _mapper.Map<PaymentDetails>(payment);
            _paymentRepository.SavePayment(paymentDetails);

            return result;
        }

        public async Task<PaymentDetails> GetPayment(int id)
        {
            return new PaymentDetails();
        }
    }
}
