namespace TNO.Logging.Common.Exceptions.Abstractions;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

/// <summary>
/// Contains the different guids that are used for exception groups.
/// </summary>
public static class ExceptionGroups
{
   /// <summary>The guids for exceptions in the <see cref="global::System"/> namespace.</summary>
   public static class System
   {
      public const string Exception = "08A56F9D-0634-49AE-A748-DB6AB5666F84";

      public const string ArgumentException = "45BA4F3E-3768-48B3-B3BE-BC014A6EAFEA";
      public const string ArgumentNullException = "3C21E3A0-CA73-4891-B26B-5924A17C3B12";
      public const string ArgumentOutOfRangeException = "C0F1BC53-D2F2-4AF1-93DC-92D44B77F723";

      public const string AggregateException = "2B79EAB5-CE44-4E78-AD71-E4C8FCE2DEA8";
   }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member