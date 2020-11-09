using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using PaymentGateway.Database;

namespace PaymentGateway.Payment
{
    public interface IPaymentRepository
    {
        Task SavePayment(PaymentDetails payment);
        Task<PaymentDetails> GetPayment(int id);
    }

    public class PaymentRepository : IPaymentRepository
    {
        private readonly IDatabaseContext _databaseContext;
        private readonly ILogger<PaymentRepository> _logger;

        public PaymentRepository(IDatabaseContext databaseContext, ILogger<PaymentRepository> logger)
        {
            _databaseContext = databaseContext;
            _logger = logger;
        }

        public async Task SavePayment(PaymentDetails payment)
        {
            _logger.Log(LogLevel.Debug, "Saving payment to db", payment);
            await _databaseContext.PaymentDetails.InsertOneAsync(payment);
        }

        public async Task<PaymentDetails> GetPayment(int id)
        {
            _logger.Log(LogLevel.Debug, "Getting payment from db", id);
            return await _databaseContext.PaymentDetails.Find(i => i.PaymentId == id).SingleOrDefaultAsync();
        }
    }
}
