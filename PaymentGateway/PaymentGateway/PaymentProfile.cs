using AutoMapper;
using PaymentGateway.Contract;

namespace PaymentGateway
{
    public class PaymentProfile: Profile
    {
        public PaymentProfile()
        {
            CreateMap<Payment, PaymentDetails>()
                .ForMember(dest => dest.MaskedCardNumber,
                    opt => opt.MapFrom(src => src.CardNumber)); // todo: mask card number
        }
    }
}
