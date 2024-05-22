using AutoMapper;

namespace Reviews.Application.Abstractions.Mappings;

public interface IMapTo<T>
{
    void CreateMap(Profile profile)
    {
        profile.CreateMap(GetType(), typeof(T));
    }
}