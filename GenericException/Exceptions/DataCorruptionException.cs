using GenericException.Interfaces;

namespace GenericException.Exceptions;

/// <summary>
/// Represents an exception caused by no valid/incoherent data
/// </summary>
/// <seealso cref="ICustomException" />
public class DataCorruptionException : CustomExceptionBase
{
}