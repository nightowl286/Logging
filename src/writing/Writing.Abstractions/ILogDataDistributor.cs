namespace TNO.Logging.Writing.Abstractions;

/// <summary>
/// Denotes a distributor of log data.
/// </summary>
public interface ILogDataDistributor : ILogDataCollector
{
   #region Methods
   /// <summary>Checks whether the given <paramref name="collector"/> is assigned to this distributor.</summary>
   /// <param name="collector">The collector to check for.</param>
   /// <returns>
   /// <see langword="true"/> if the <paramref name="collector"/> is
   /// assigned to this distributor, <see langword="false"/> otherwise.
   /// </returns>
   bool IsAssigned(ILogDataCollector collector);

   /// <summary>Assigns the given <paramref name="collector"/> to distribute the collected data to.</summary>
   /// <param name="collector">The collector to assign.</param>
   /// <returns>
   /// <see langword="true"/> if the <paramref name="collector"/> was assigned successfully,
   /// <see langword="false"/> if the <paramref name="collector"/> has already been assigned to this distributor.
   /// </returns>
   bool Assign(ILogDataCollector collector);

   /// <summary>Unassigns the given <paramref name="collector"/> and stops the collected data from being distributed to it.</summary>
   /// <param name="collector">The collector to unassign.</param>
   /// <returns>
   /// <see langword="true"/> if the <paramref name="collector"/> was unassigned successfully,
   /// <see langword="false"/>if the <paramref name="collector"/> has not been assigned to this distributor.
   /// </returns>
   bool Unassign(ILogDataCollector collector);
   #endregion
}
