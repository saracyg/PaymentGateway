using AutoMapper;

namespace PaymentGateway.MappingProfiles
{
    public class DatabasePaymentDetailsProfile :Profile
    {
        public DatabasePaymentDetailsProfile()
        {
            CreateMap<Contract.Payment, Database.PaymentDetails>();
        }
    }
}
