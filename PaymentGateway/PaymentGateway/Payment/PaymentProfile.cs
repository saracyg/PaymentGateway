using AutoMapper;
using PaymentGateway.Contract;

namespace PaymentGateway.Payment
{
    public class PaymentProfile: Profile
    {
        public PaymentProfile()
        {
            CreateMap<Contract.Payment, PaymentDetails>()
                .ForMember(dest => dest.MaskedCardNumber,
                    opt => opt.MapFrom(src => src.CardNumber)); // todo: mask card number
        }
    }
}
