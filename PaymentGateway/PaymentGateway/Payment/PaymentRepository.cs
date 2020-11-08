﻿using System;
using System.Threading.Tasks;
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

        public PaymentRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task SavePayment(PaymentDetails payment)
        {
            await _databaseContext.PaymentDetails.InsertOneAsync(payment);
        }

        public async Task<PaymentDetails> GetPayment(int id)
        {
            throw new NotImplementedException();
        }
    }
}
