using System;
using System.Reflection;
using AutoMapper;

namespace UserRegister.Application.Tests.Utils;

public static class MapperTests
{
    public static IMapper Mapping<TMapper>() where TMapper : Profile, new() => ToMapper<TMapper>().Value;
    private static Lazy<IMapper> ToMapper<TMapper>() where TMapper : Profile, 
        new() => new ((Func<IMapper>) (() => new MapperConfiguration( 
        (
            mapperCongiguration =>
            {
                mapperCongiguration.ShouldMapProperty = (
                    result =>
                    {
                        if (!(result.GetMethod != (MethodInfo) null))
                            return false;
                        return result.GetMethod.IsPublic || result.GetMethod.IsAssembly;
                    });
                mapperCongiguration.AddProfile<TMapper>();
            }
        )).CreateMapper()));
}