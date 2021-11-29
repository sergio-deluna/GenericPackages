using System;

namespace GenericResult.Interfaces
{
    public interface IxResult<T> : IGenericStructure
    {
        /// <summary>
        /// The data object to be returned.
        /// </summary>
        T Object { get; set; }

        /// <summary>
        /// Returns success = false with message = exception.Message
        /// </summary>
        /// <param name="ex">Exception details passed as the message string</param>
        /// <param name="optionalParams">Not serialized.Always passed as diagnostic data.</param>
        /// <returns>The instance based on the <see cref="IxResult{T}"/>interface</returns>
        IxResult<T> Error(Exception ex, params object[] optionalParams);

        /// <summary>
        /// Returns success = false with message = message parameter
        /// </summary>
        /// <param name="message">A non-sensitive message.</param>
        /// <param name="optionalParams">Not serialized.Always passed as diagnostic data.</param>
        /// <returns>The instance based on the <see cref="IxResult{T}"/>interface</returns>
        IxResult<T> Error(string message, params object[] optionalParams);

        /// <summary>
        /// Returns success = false with message = message parameter, but still returning data for whatever reason.
        /// </summary>
        /// <param name="message">A non-sensitive message.</param>
        /// <param name="obj">The data object to be returned</param>
        /// <param name="optionalParams">Not serialized.Always passed as diagnostic data.</param>
        /// <returns>The instance based on the <see cref="IxResult{T}"/>interface</returns>
        IxResult<T> Error(string message, T obj, params object[] optionalParams);

        /// <summary>
        /// Returns success = false with message = message parameter
        /// The exception is not serialized, but its message exception is included as diagnostic data.
        /// </summary>
        /// <param name="message">A non-sensitive message.</param>
        /// <param name="ex">Its message exception is included as diagnostic data.
        /// This is because the exception object is too big for being serialized. If you need to serialize the exception object too,
        /// then pass the exception with the <paramref name="optionalParams"/> param.</param>
        /// <param name="optionalParams">Not serialized.Always passed as diagnostic data.</param>
        /// <returns>The instance based on the <see cref="IxResult{T}"/>interface</returns>
        IxResult<T> Error(string message, Exception ex, params object[] optionalParams);

        /// <summary>
        /// Returns success = false with message = message parameter, but still returning data for whatever reason.
        /// The exception is not serialized, but its message exception is included as diagnostic data.
        /// </summary>
        /// <param name="message">A non-sensitive message.</param>
        /// <param name="obj">The data object to be returned</param>
        /// <param name="ex">Its message exception is included as diagnostic data.
        /// This is because the exception object is too big for being serialized. If you need to serialize the exception object too,
        /// then pass the exception with the <paramref name="optionalParams"/> param.</param>
        /// <param name="optionalParams">Not serialized.Always passed as diagnostic data.</param>
        /// <returns>The instance based on the <see cref="IxResult{T}"/>interface</returns>
        IxResult<T> Error(string message, T obj, Exception ex, params object[] optionalParams);

        /// <summary>
        /// Returns success = true with message = string.empty and null data
        /// </summary>
        /// <returns></returns>
        IxResult<T> Ok();

        /// <summary>
        /// Returns success = true with message = message parameter and null data
        /// </summary>
        /// <param name="message">A non-sensitive message.</param>
        /// <param name="optionalParams">Not serialized.Always passed as diagnostic data.</param>
        /// <returns>The instance based on the <see cref="IxResult{T}"/>interface</returns>
        IxResult<T> Ok(string message, params object[] optionalParams);

        /// <summary>
        /// Returns success = true with message = message parameter
        /// </summary>
        /// <param name="message">A non-sensitive message.</param>
        /// <param name="obj">The data object to be returned</param>
        /// <param name="optionalParams">Not serialized.Always passed as diagnostic data.</param>
        /// <returns>The instance based on the <see cref="IxResult{T}"/>interface</returns>
        IxResult<T> Ok(string message, T obj, params object[] optionalParams);

        /// <summary>
        /// Returns success = true with message = string.empty and the data object
        /// </summary>
        /// <param name="obj">The data object to be returned</param>
        /// <param name="optionalParams">Not serialized.Always passed as diagnostic data.</param>
        /// <returns>The instance based on the <see cref="IxResult{T}"/>interface</returns>
        IxResult<T> Ok(T obj, params object[] optionalParams);
    }
}