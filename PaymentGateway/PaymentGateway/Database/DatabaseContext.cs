using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PaymentGateway.ConfigOptions;

namespace PaymentGateway.Database
{
    public interface IDatabaseContext
    {
        IMongoCollection<PaymentDetails> PaymentDetails { get; }
    }

    public class DatabaseContext : IDatabaseContext
    {
        private readonly IMongoDatabase _database;

        public DatabaseContext(IMongoClient mongoClient, IOptions<DatabaseConfigOptions> config)
        {
            _database = mongoClient.GetDatabase(config.Value.DatabaseName);
        }

        public IMongoCollection<PaymentDetails> PaymentDetails => _database.GetCollection<PaymentDetails>("PaymentDetails");
    }
}
