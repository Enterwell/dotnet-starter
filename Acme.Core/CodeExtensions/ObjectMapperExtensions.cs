using AutoMapper;
using System.Linq.Expressions;

namespace Acme.Core.CodeExtensions;

/// <summary>
/// Object mapper extensions.
/// </summary>
public static class ObjectMapperExtensions
{
    /// <summary>
    /// An IMappingExpression&lt;TSource, TDest&gt; extension method that maps the property to source member.
    /// </summary>
    /// <typeparam name="TSource">Type of the source.</typeparam>
    /// <typeparam name="TDest">Type of the destination.</typeparam>
    /// <typeparam name="TMemberDest">Type of the member destination.</typeparam>
    /// <typeparam name="TResult">Type of the result.</typeparam>
    /// <param name="map">The map to act on.</param>
    /// <param name="dest">Destination to map to.</param>
    /// <param name="sourceFunc">Source to map from.</param>
    /// <returns>An <see cref="IMappingExpression"/>.</returns>
    public static IMappingExpression<TSource, TDest> MapPropertyFunc<TSource, TDest, TMemberDest, TResult>(
        this IMappingExpression<TSource, TDest> map,
        Expression<Func<TDest, TMemberDest>> dest,
        Func<TSource, TResult> sourceFunc)
    {
        return map.ForMember(dest, opt => opt.MapFrom((source, _) => sourceFunc(source)));
    }

    /// <summary>
    /// An object extension method that maps one object to specified object type using provided mapper.
    /// </summary>
    /// <typeparam name="T">Generic type parameter of the type to map to.</typeparam>
    /// <param name="obj">The object to act on.</param>
    /// <param name="mapper">The mapper.</param>
    /// <returns>The mapped object.</returns>
    public static T MapTo<T>(this object obj, IMapper mapper)
    {
        return mapper.Map<T>(obj);
    }
}