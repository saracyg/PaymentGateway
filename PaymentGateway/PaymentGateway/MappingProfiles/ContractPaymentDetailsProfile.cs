using AutoMapper;

namespace PaymentGateway.MappingProfiles
{
    public class ContractPaymentDetailsProfile : Profile
    {
        public ContractPaymentDetailsProfile()
        {
            CreateMap<Database.PaymentDetails, Contract.PaymentDetails>();
        }
    }
}
