using System.Runtime.CompilerServices;

namespace TNO.Writing.Tests;

public class Annotated
{
   #region Fields
   public readonly string Annotation;
   public readonly object? Data;
   #endregion
   public Annotated(object? data, [CallerArgumentExpression(nameof(data))] string annotation = "")
   {
      Annotation = annotation;
      Data = data;
   }

   #region Functions
   public static Annotated<T> New<T>(T data, [CallerArgumentExpression(nameof(data))] string annotation = "") => new Annotated<T>(data, annotation);
   #endregion
}

public class Annotated<T> : Annotated
{
   #region Properties
   public new T Data { get => (T)base.Data!; }
   #endregion
   public Annotated(T data, [CallerArgumentExpression(nameof(data))] string annotation = "")
      : base(data, annotation) { }
}
