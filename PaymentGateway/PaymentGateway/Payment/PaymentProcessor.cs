using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
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
        private readonly ICardNumberMaskingService _cardNumberMaskingService;
        private readonly ILogger<PaymentProcessor> _logger;

        public PaymentProcessor(IPaymentApiClient paymentApiClient, IPaymentRepository paymentRepository, IMapper mapper, ICardNumberMaskingService cardNumberMaskingService, ILogger<PaymentProcessor> logger)
        {
            _paymentApiClient = paymentApiClient;
            _paymentRepository = paymentRepository;
            _mapper = mapper;
            _cardNumberMaskingService = cardNumberMaskingService;
            _logger = logger;
        }

        public async Task<PaymentResult> ProcessNewPayment(Contract.Payment payment)
        {
            var result = await _paymentApiClient.SendPayment(payment);

            var paymentDetails = CreatePaymentDetails(payment, result);

            await _paymentRepository.SavePayment(paymentDetails);

            return result;
        }

        public async Task<PaymentDetails> GetPayment(int id)
        {
            Database.PaymentDetails paymentDetails;
            try
            {
                paymentDetails = await _paymentRepository.GetPayment(id);
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, $"Error when getting payment {id} from db", e);
                throw;
            }

            if (paymentDetails == null)
            {
                _logger.Log(LogLevel.Debug, "Trying to get not existing payment", id);
                return null;
            }

            return _mapper.Map<PaymentDetails>(paymentDetails);
        }

        private Database.PaymentDetails CreatePaymentDetails(Contract.Payment payment, PaymentResult result)
        {
            var paymentDetails = _mapper.Map<Database.PaymentDetails>(payment);
            paymentDetails.PaymentId = result?.PaymentId;
            paymentDetails.PaymentStatus = result?.PaymentStatus;
            paymentDetails.MaskedCardNumber = _cardNumberMaskingService.MaskCardNumber(payment.CardNumber);
            paymentDetails.TimeStamp = DateTime.UtcNow;
            return paymentDetails;
        }
    }
}
