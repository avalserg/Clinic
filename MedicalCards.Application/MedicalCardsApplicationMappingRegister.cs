using MedicalCards.Application.BaseRealizations;

namespace MedicalCards.Application;

public class MedicalCardsApplicationMappingRegister : MappingRegister
{
    public MedicalCardsApplicationMappingRegister() : base(typeof(MedicalCardsApplicationMappingRegister).Assembly)
    {
    }
}
