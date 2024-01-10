using AutoMapper;
using System;

namespace Business.Helpers.Extensions;

public static class GenericExtensions
{
    public static Func<TSrc, TResult> ModifyFunc<TDist, TSrc, TResult>(
        this Func<TDist, TResult> originalFunc, IMapper mapper)
    {
        return input =>
        {
            var transformedInput = mapper.Map<TDist>(input);
            return originalFunc(transformedInput);
        };
    }
}
