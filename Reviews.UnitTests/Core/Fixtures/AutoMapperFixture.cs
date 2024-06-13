

using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Reviews.UnitTests.Core.Fixtures;

public class AutoMapperFixture
{
    public AutoMapperFixture(Assembly targetProjectAssembly)
    {
        var services = new ServiceCollection();
        services.AddAutoMapper(targetProjectAssembly);

        var sp = services.BuildServiceProvider();
        Mapper = sp.GetRequiredService<IMapper>();
    }

    public IMapper Mapper { get; }
}