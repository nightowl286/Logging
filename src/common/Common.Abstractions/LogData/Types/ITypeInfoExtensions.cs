﻿namespace TNO.Logging.Common.Abstractions.LogData.Types;

/// <summary>
/// Contains useful extension methods related to the <see cref="ITypeInfo"/>.
/// </summary>
public static class ITypeInfoExtensions
{
   #region Methods
   /// <summary>
   /// Checks whether the given <paramref name="typeInfo"/>
   /// has a <see cref="ITypeInfo.BaseTypeId"/>.
   /// </summary>
   /// <param name="typeInfo">The <see cref="ITypeInfo"/> to check.</param>
   /// <returns>
   /// <see langword="true"/> if the given <paramref name="typeInfo"/>
   /// has a base type, <see langword="false"/> otherwise.
   /// </returns>
   public static bool HasBaseType(ITypeInfo typeInfo) => typeInfo.BaseTypeId != 0;

   /// <summary>
   /// Checks whether the given <paramref name="typeInfo"/>
   /// is declared in a different type.
   /// </summary>
   /// <param name="typeInfo">The <see cref="ITypeInfo"/> to check.</param>
   /// <returns>
   /// <see langword="true"/> if the given <paramref name="typeInfo"/> is 
   /// declared in a different type, <see langword="false"/> otherwise.
   /// </returns>
   public static bool HasDeclaredInType(ITypeInfo typeInfo) => typeInfo.DeclaringTypeId != 0;
   #endregion
}
