using TNO.DependencyInjection;
using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Reading.Abstractions;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Abstractions.Exceptions;
using TNO.Logging.Reading.Deserialisers;
using TNO.Logging.Reading.Deserialisers.Registrants;
using TNO.Logging.Reading.Exceptions;

namespace TNO.Logging.Reading.Builders;

internal sealed class LogReaderConfigurator : ILogReaderConfigurator
{
   #region Fields
   private readonly IServiceScope _scope;

   private readonly IDeserialiser _deserialiser;
   private readonly ExceptionDataDeserialiserRegistrar _exceptionDataDeserialiserRegistrar;
   private readonly ExceptionDataDeserialiser _exceptionDataDeserialiser;
   #endregion
   public LogReaderConfigurator()
   {
      _scope = new ServiceFacade().CreateNew();

      _deserialiser = new Deserialiser(_scope.Requester);
      _exceptionDataDeserialiserRegistrar = new ExceptionDataDeserialiserRegistrar();
      _exceptionDataDeserialiser = new ExceptionDataDeserialiser(_exceptionDataDeserialiserRegistrar, _scope.Builder);

      _scope.Registrar
         .Instance<IExceptionDataDeserialiser>(_exceptionDataDeserialiser)
         .Instance(_deserialiser);

      WithRegistrant(new BuiltinDeserialiserRegistrant());
      WithRegistrant(new BuiltinExceptionDataDeserialiserRegistrant());
   }

   #region Methods
   /// <inheritdoc/>
   public IServiceScope GetScope() => _scope;

   /// <inheritdoc/>
   public ILogReaderConfigurator WithRegistrant(IDeserialiserRegistrant registrant)
   {
      registrant.Register(_scope);
      return this;
   }

   /// <inheritdoc/>
   public ILogReaderConfigurator WithRegistrant(IExceptionDataDeserialiserRegistrant registrant)
   {
      registrant.Register(_exceptionDataDeserialiserRegistrar, _scope);
      return this;
   }
   #endregion
}
