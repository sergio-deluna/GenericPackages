using System;

namespace GenericException;

/// <summary>
/// Represents an exception caused by no valid/incoherent data
/// </summary>
/// <seealso cref="System.Exception" />
/// <seealso cref="GenericException.ICustomException" />
public class DataCorruptionException : Exception, ICustomException
{
    /// <summary>
    /// Gets or sets the error code for internal purposes.
    /// </summary>
    /// <value>
    /// The error code.
    /// </value>
    public string ExceptionCode { get; set; } = string.Empty;
}