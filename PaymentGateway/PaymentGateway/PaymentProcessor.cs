using AutoMapper;
using PaymentGateway.Contract;

namespace PaymentGateway
{
    public interface IPaymentProcessor
    {
        PaymentResult ProcessNewPayment(Payment payment);
        PaymentDetails GetPayment(in int id);
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

        public PaymentResult ProcessNewPayment(Payment payment)
        {
            var result = _paymentApiClient.SendPayment(payment);

            var paymentDetails = _mapper.Map<PaymentDetails>(payment);
            _paymentRepository.SavePayment(paymentDetails);

            return result;
        }

        public PaymentDetails GetPayment(in int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
