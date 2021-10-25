using System;

namespace SimpleResult.Interfaces
{
    public interface IResult<T> : IGenericStructure where T : class
    {
        IResult<T> Error(Exception ex, params object[] optionalParams);

        IResult<T> Error(string message, params object[] optionalParams);

        IResult<T> Ok();

        IResult<T> Ok(string message);

        IResult<T> Ok(string message, T obj);

        IResult<T> Ok(T obj);

        T Object { get; set; }
    }
}