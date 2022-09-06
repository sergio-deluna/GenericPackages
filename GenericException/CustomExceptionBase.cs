using GenericException.Interfaces;
using System;

namespace GenericException;

/// <summary>
/// Base exception for any new custom exception.
/// </summary>
/// <seealso cref="System.Exception" />
/// <seealso cref="GenericException.Interfaces.ICustomException" />
public class CustomExceptionBase : Exception, ICustomException
{
    /// <summary>
    /// Gets or sets the error code for internal purposes.
    /// </summary>
    /// <value>
    /// The error code.
    /// </value>
    public string ExceptionCode { get; set; } = string.Empty;
}