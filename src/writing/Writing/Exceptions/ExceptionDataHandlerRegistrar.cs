using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.InteropServices;
using TNO.Common.Extensions;
using TNO.Logging.Writing.Abstractions.Exceptions;

namespace TNO.Logging.Writing.Exceptions;

/// <summary>
/// Represents both a registrar and a requester for types that implement <see cref="IExceptionDataHandler{TException, TExceptionData}"/>.
/// </summary>
public class ExceptionDataHandlerRegistrar : IExceptionDataHandlerRegistrar, IExceptionDataHandlerRequester
{
   #region Fields
   private readonly Dictionary<Type, IExceptionDataHandlerInfo> _byExceptionType = new Dictionary<Type, IExceptionDataHandlerInfo>();
   private readonly Dictionary<Guid, IExceptionDataHandlerInfo> _byId = new Dictionary<Guid, IExceptionDataHandlerInfo>();
   #endregion

   #region Methods
   /// <inheritdoc/>
   public void Register(Type handlerType)
   {
      List<Type> implementations = handlerType.GetOpenInterfaceImplementations(typeof(IExceptionDataHandler<,>)).ToList();
      if (implementations.Count > 1)
      {
         throw new ArgumentException($"The exception data handler interface ({typeof(IExceptionDataHandler<,>)}) is not allowed to be implemented multiple " +
            $"times on the same type. Got {implementations.Count} implementations on the given type ({handlerType}).", nameof(handlerType));
      }
      else if (implementations.Count == 0)
         throw new ArgumentException($"The given handler type ({handlerType}) does not implement {typeof(IExceptionDataHandler<,>)}.", nameof(handlerType));

      Type implementation = implementations[0];

      GuidAttribute guidAttribute =
         handlerType.GetCustomAttribute<GuidAttribute>(false) ??
         throw new ArgumentException($"The give handler type ({handlerType}) does not have the {typeof(GuidAttribute)} set.", nameof(handlerType));

      Guid guid = new Guid(guidAttribute.Value);

      if (_byId.ContainsKey(guid))
         throw new ArgumentException($"The guid ({guid}) on given handler type ({handlerType}) has already been registered.", nameof(handlerType));


      Type[] generics = implementation.GetGenericArguments();
      Debug.Assert(generics.Length == 2);

      Type exceptionType = generics[0];
      Type exceptionDataType = generics[1];

      if (_byExceptionType.ContainsKey(exceptionType))
         throw new ArgumentException($"A handler for the exception type ({exceptionType}) has already been registered.", nameof(handlerType));

      ExceptionDataHandlerInfo info = new ExceptionDataHandlerInfo(guid, exceptionType, exceptionDataType, handlerType);

      _byExceptionType.Add(exceptionType, info);
      _byId.Add(guid, info);
   }

   /// <inheritdoc/>
   public void RegisterIfMissing(Type handlerType)
   {
      if (IsRegisteredForExceptionType(handlerType) == false)
         Register(handlerType);
   }

   /// <inheritdoc/>
   public bool ByExceptionType(Type exceptionType, [NotNullWhen(true)] out IExceptionDataHandlerInfo? info)
   {
      return _byExceptionType.TryGetValue(exceptionType, out info);
   }

   /// <inheritdoc/>
   public bool ById(Guid id, [NotNullWhen(true)] out IExceptionDataHandlerInfo? info)
   {
      return _byId.TryGetValue(id, out info);
   }
   #endregion

   #region Helpers
   private bool IsRegisteredForExceptionType(Type handlerType)
   {
      Type[] implementations = handlerType.GetOpenInterfaceImplementations(typeof(IExceptionDataHandler<,>)).ToArray();
      if (implementations.Length != 1)
         return false;

      Type implementation = implementations[0];
      Type exceptionType = implementation.GetGenericArguments()[0];

      return _byExceptionType.ContainsKey(exceptionType);
   }
   #endregion
}
