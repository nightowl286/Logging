using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.InteropServices;
using TNO.Common.Extensions;
using TNO.Logging.Reading.Abstractions.Exceptions;

namespace TNO.Logging.Reading.Exceptions;

/// <summary>
/// Represents both a registrar and a requester for types that implement <see cref="IExceptionDataDeserialiser{TExceptionData}"/>.
/// </summary>
public class ExceptionDataDeserialiserRegistrar : IExceptionDataDeserialiserRegistrar, IExceptionDataDeserialiserRequester
{
   #region Fields
   private readonly Dictionary<Guid, IExceptionDataDeserialiserInfo> _byId = new Dictionary<Guid, IExceptionDataDeserialiserInfo>();

   #endregion

   #region Methods
   /// <inheritdoc/>
   public void Register(Type deserialiserType)
   {
      List<Type> implementations = deserialiserType.GetOpenInterfaceImplementations(typeof(IExceptionDataDeserialiser<>)).ToList();
      if (implementations.Count > 1)
      {
         throw new ArgumentException($"The exception data deserialiser interface ({typeof(IExceptionDataDeserialiser<>)}) is not allowed to be implemented multiple " +
            $"times on the same type. Got {implementations.Count} implementations on the given type ({deserialiserType}).", nameof(deserialiserType));
      }
      else if (implementations.Count == 0)
         throw new ArgumentException($"The given deserialiser type ({deserialiserType}) does not implement {typeof(IExceptionDataDeserialiser<>)}.", nameof(deserialiserType));

      GuidAttribute guidAttribute =
         deserialiserType.GetCustomAttribute<GuidAttribute>(false) ??
         throw new ArgumentException($"The give deserialiser type ({deserialiserType}) does not have the {typeof(GuidAttribute)} set.", nameof(deserialiserType));

      Guid guid = new Guid(guidAttribute.Value);

      if (_byId.ContainsKey(guid))
         throw new ArgumentException($"The guid ({guid}) on given deserialiser type ({deserialiserType}) has already been registered.", nameof(deserialiserType));

      ExceptionDataDeserialiserInfo info = new ExceptionDataDeserialiserInfo(guid, deserialiserType);

      _byId.Add(guid, info);
   }

   /// <inheritdoc/>
   public bool ById(Guid id, [NotNullWhen(true)] out IExceptionDataDeserialiserInfo? info)
   {
      return _byId.TryGetValue(id, out info);
   }
   #endregion
}
