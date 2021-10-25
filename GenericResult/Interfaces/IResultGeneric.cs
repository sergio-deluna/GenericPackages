using System;

namespace GenericResult.Interfaces
{
    public interface IResult<T> : IGenericStructure 
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