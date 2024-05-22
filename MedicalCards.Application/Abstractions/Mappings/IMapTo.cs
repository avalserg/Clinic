using AutoMapper;

namespace MedicalCards.Application.Abstractions.Mappings;

public interface IMapTo<T>
{
    void CreateMap(Profile profile)
    {
        profile.CreateMap(GetType(), typeof(T));
    }
}