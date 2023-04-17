﻿using TNO.Logging.Common.Abstractions;
using TNO.Logging.Writing.Abstractions;
using TNO.Logging.Writing.Abstractions.Serialisers.Bases;

namespace TNO.Logging.Writing.SerialiserProviders;

internal sealed class LockingSerialiserProvider : ISerialiserProvider
{
   #region Fields
   private readonly ISerialiserProvider _innerProvider;
   private readonly object _lock = new object();
   #endregion

   #region Constructors
   public LockingSerialiserProvider(ISerialiserProvider innerProvider)
   {
      _innerProvider = innerProvider;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public T GetSerialiser<T>() where T : notnull, ISerialiser
   {
      lock (_lock)
      {
         return _innerProvider.GetSerialiser<T>();
      }
   }

   /// <inheritdoc/>
   public DataVersionMap GetVersionMap()
   {
      lock (_lock)
      {
         return _innerProvider.GetVersionMap();
      }
   }
   #endregion
}
