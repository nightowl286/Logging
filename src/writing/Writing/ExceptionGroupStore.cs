using System.Diagnostics.CodeAnalysis;

namespace TNO.Logging.Writing;

public record class ExceptionGroup(Type SerialiserType, Type ConverterType, Type ExceptionType, Type ExceptionDataType, Guid GroupId);

public class ExceptionGroupStore
{
   #region Subclass
   #endregion

   #region Fields
   private readonly Dictionary<Guid, ExceptionGroup> _byGuid = new Dictionary<Guid, ExceptionGroup>();
   private readonly Dictionary<Type, ExceptionGroup> _byExceptionType = new Dictionary<Type, ExceptionGroup>();
   #endregion

   #region Methods

   public bool TryGet(
      Type exceptionType,
      [NotNullWhen(true)] out ExceptionGroup? group)
   {
      if (_byExceptionType.TryGetValue(exceptionType, out group))
         return true;

      return false;
   }

   public bool TryGet(
      Guid groupId,
      [NotNullWhen(true)] out ExceptionGroup? group)
   {
      if (_byGuid.TryGetValue(groupId, out group))
         return true;

      return false;
   }

   public void Add(Type serialiserType, Type converterType, Type exceptionType, Type exceptionDataType, Guid groupId)
   {
      if (TryGet(exceptionType, out _))
         throw new ArgumentException($"An exception group for the given exception type ({exceptionType}) has already been added.", nameof(exceptionType));

      if (TryGet(groupId, out _))
         throw new ArgumentException($"An exception group for the given group id ({groupId}) has already been added.", nameof(groupId));

      ExceptionGroup group = new ExceptionGroup(serialiserType, converterType, exceptionType, exceptionDataType, groupId);

      _byExceptionType.Add(exceptionType, group);
      _byGuid.Add(groupId, group);
   }
   #endregion
}
