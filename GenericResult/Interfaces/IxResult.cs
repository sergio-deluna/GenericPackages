using System;

namespace GenericResult.Interfaces;

public interface IxResult : IxBase
{
    /// <summary>
    /// Returns success = false with message = exception.Message
    /// </summary>
    /// <param name="ex">Exception details passed as the message string</param>
    /// <param name="optionalParams">Not serialized.Always passed as diagnostic data.</param>
    /// <returns>The instance based on the <see cref="IxResult"/>interface</returns>
    IxResult Error(Exception ex, params object[] optionalParams);

    /// <summary>
    /// Returns success = false with message = message parameter
    /// </summary>
    /// <param name="message">A non-sensitive message.</param>
    /// <param name="optionalParams">Not serialized.Always passed as diagnostic data.</param>
    /// <returns>The instance based on the <see cref="IxResult"/>interface</returns>
    IxResult Error(string message, params object[] optionalParams);

    /// <summary>
    /// Returns success = false with message = message parameter
    /// The exception is not serialized, but its message exception is included as diagnostic data.
    /// </summary>
    /// <param name="message">A non-sensitive message.</param>
    /// <param name="ex">Its message exception is included as diagnostic data.
    /// This is because the exception object is too big for being serialized. If you need to serialize the exception object too,
    /// then pass the exception with the <paramref name="optionalParams"/> param.</param>
    /// <param name="optionalParams">Not serialized.Always passed as diagnostic data.</param>
    /// <returns>The instance based on the <see cref="IxResult"/>interface</returns>
    IxResult Error(string message, Exception ex, params object[] optionalParams);

    /// <summary>
    /// Returns success = true with message = string.empty
    /// </summary>
    /// <returns>The instance based on the <see cref="IxResult"/>interface</returns>
    IxResult Ok();

    /// <summary>
    /// Returns success = true with message = message parameter
    /// </summary>
    /// <param name="message">A non-sensitive message.</param>
    /// <returns>The instance based on the <see cref="IxResult"/>interface</returns>
    IxResult Ok(string message);

    /// <summary>
    /// Returns success = true with message = message parameter
    /// </summary>
    /// <param name="message">A non-sensitive message.</param>
    /// <param name="optionalParams">Not serialized.Always passed as diagnostic data.</param>
    /// <returns>The instance based on the <see cref="IxResult"/>interface</returns>
    IxResult Ok(string message, params object[] optionalParams);
}