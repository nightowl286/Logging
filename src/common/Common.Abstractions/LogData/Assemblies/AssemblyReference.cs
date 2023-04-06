namespace TNO.Logging.Common.Abstractions.LogData.Assemblies;

/// <summary>
/// Represents a link between an <see cref="AssemblyInfo"/> and it's <see cref="Id"/>.
/// </summary>
/// <param name="AssemblyInfo">The assembly info that the <see cref="Id"/> is associated with.</param>
/// <param name="Id">The id that the <see cref="AssemblyInfo"/> is associated with.</param>
public record struct AssemblyReference(IAssemblyInfo AssemblyInfo, ulong Id);
