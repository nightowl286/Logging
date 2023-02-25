﻿using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;

namespace TNO.Logging.Common.Entries.Components;

/// <summary>
/// Represents a <see cref="ComponentKind.Thread"/> component.
/// </summary>
/// <param name="ManagedId">The managed id of the thread.</param>
/// <param name="Name">The optional name of the thread.</param>
/// <param name="State">The current state of the thread.</param>
/// <param name="IsThreadPoolThread">Whether the thread belongs to a thread pool.</param>
/// <param name="Priority">The priority of the thread.</param>
/// <param name="ApartmentState">The apartment state of the thread.</param>
public record class ThreadComponent(
   int ManagedId,
   string Name,
   ThreadState State,
   bool IsThreadPoolThread,
   ThreadPriority Priority,
   ApartmentState ApartmentState) : IThreadComponent
{
   #region Properties
   /// <inheritdoc/>
   public ComponentKind Kind => ComponentKind.Thread;
   #endregion

   #region Functions
   /// <summary>Creates a <see cref="ThreadComponent"/> from the given <paramref name="thread"/>.</summary>
   /// <param name="thread">The thread to extract information from.</param>
   /// <returns>The newly created <see cref="ThreadComponent"/>.</returns>
   public static ThreadComponent FromThread(Thread thread)
   {
      ThreadComponent component = new ThreadComponent(
         thread.ManagedThreadId,
         thread.Name ?? string.Empty,
         thread.ThreadState,
         thread.IsBackground,
         thread.Priority,
         thread.GetApartmentState());

      return component;
   }
   #endregion
}
