using System;

namespace SimpleResult.Interfaces
{
    public interface IResult : IGenericStructure
    {
        IResult Error(Exception ex, params object[] optionalParams);

        IResult Error(string message, params object[] optionalParams);

        IResult Ok(string message);

        IResult Ok();
    }
}