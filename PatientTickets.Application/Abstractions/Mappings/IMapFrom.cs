using AutoMapper;

namespace PatientTickets.Application.Abstractions.Mappings;

public interface IMapFrom<T>
{
    void CreateMap(Profile profile)
    {
        profile.CreateMap(typeof(T), GetType());
    }
}