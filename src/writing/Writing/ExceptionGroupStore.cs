using System.Diagnostics.CodeAnalysis;
using TNO.Logging.Common.Abstractions.LogData.Exceptions;
using TNO.Logging.Writing.Abstractions.Exceptions;

namespace TNO.Logging.Writing;

/// <summary>Represents an exception group.</summary>
/// <param name="SerialiserType">The type of the <see cref="IExceptionDataConverter{TException, TExceptionData}"/>.</param>
/// <param name="ConverterType">The type of the <see cref="IExceptionDataConverter{TException, TExceptionData}"/>.</param>
/// <param name="ExceptionType">The type of the <see cref="Exception"/>.</param>
/// <param name="ExceptionDataType">The type of the <see cref="IExceptionData"/>.</param>
/// <param name="GroupId">The id of the exception group.</param>
public record class ExceptionGroup(Type SerialiserType, Type ConverterType, Type ExceptionType, Type ExceptionDataType, Guid GroupId);

/// <summary>
/// A store for <see cref="ExceptionGroup"/>.
/// </summary>
public class ExceptionGroupStore
{
   #region Fields
   private readonly Dictionary<Guid, ExceptionGroup> _byGuid = new Dictionary<Guid, ExceptionGroup>();
   private readonly Dictionary<Type, ExceptionGroup> _byExceptionType = new Dictionary<Type, ExceptionGroup>();
   #endregion

   #region Methods
   /// <summary>Tries to get the <paramref name="group"/> by the given <paramref name="exceptionType"/>.</summary>
   /// <param name="exceptionType">The type of the <see cref="Exception"/> to try and get the <paramref name="group"/> by.</param>
   /// <param name="group">The obtained group, or <see langword="null"/>.</param>
   /// <returns>
   /// <see langword="true"/> if the <paramref name="group"/>,
   /// was found <see langword="false"/> otherwise.
   /// </returns>
   public bool TryGet(Type exceptionType, [NotNullWhen(true)] out ExceptionGroup? group)
   {
      if (_byExceptionType.TryGetValue(exceptionType, out group))
         return true;

      return false;
   }

   /// <summary>Tries to get the <paramref name="group"/> by the given <paramref name="groupId"/>.</summary>
   /// <param name="groupId">The id of the <see cref="ExceptionGroup"/> to try and find.</param>
   /// <param name="group">The obtained group, or <see langword="null"/>.</param>
   /// <returns>
   /// <see langword="true"/> if the <paramref name="group"/>,
   /// was found <see langword="false"/> otherwise.
   /// </returns>
   public bool TryGet(Guid groupId, [NotNullWhen(true)] out ExceptionGroup? group)
   {
      if (_byGuid.TryGetValue(groupId, out group))
         return true;

      return false;
   }

   /// <summary>Adds the given <paramref name="group"/> to the store.</summary>
   /// <param name="group">The group to add to the store.</param>
   /// <exception cref="ArgumentException">
   /// Thrown if an exception group with the given exception type or group id has already been added.
   /// </exception>
   public void Add(ExceptionGroup group)
   {

      if (TryGet(group.ExceptionType, out _))
         throw new ArgumentException($"An exception group for the given exception type ({group.ExceptionType}) has already been added.", nameof(group));

      if (TryGet(group.GroupId, out _))
         throw new ArgumentException($"An exception group for the given group id ({group.GroupId}) has already been added.", nameof(group));

      _byExceptionType.Add(group.ExceptionType, group);
      _byGuid.Add(group.GroupId, group);
   }
   #endregion
}
