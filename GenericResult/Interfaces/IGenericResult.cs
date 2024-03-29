﻿using System;

namespace GenericResult.Interfaces;

public interface IResult<T> : IResultBase
{
    /// <summary>
    /// The data object to be returned.
    /// </summary>
    T Object { get; set; }

    /// <summary>
    /// Returns success = false but still returning data for whatever reason.
    /// </summary>
    /// <param name="obj">The data object to be returned</param>
    /// <returns>The instance based on the <see cref="GenericResult.Interfaces.IResult{T}" />interface</returns>
    IResult<T> Error(T obj);

    /// <summary>
    /// Returns success = false with message = message parameter, but still returning data for whatever reason.
    /// </summary>
    /// <param name="message">A non-sensitive message.</param>
    /// <param name="obj">The data object to be returned</param>
    /// <param name="optionalParams">Not serialized.Always passed as diagnostic data.</param>
    /// <returns>The instance based on the <see cref="GenericResult.Interfaces.IResult{T}" />interface</returns>
    IResult<T> Error(T obj, Exception ex);

    /// <summary>
    /// Returns success = false with message = message parameter, but still returning data for whatever reason.
    /// </summary>
    /// <param name="message">A non-sensitive message.</param>
    /// <param name="obj">The data object to be returned</param>
    /// <param name="optionalParams">Not serialized.Always passed as diagnostic data.</param>
    /// <returns>The instance based on the <see cref="IResult{T}"/>interface</returns>
    IResult<T> Error(string message, T obj);

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
    /// <returns>The instance based on the <see cref="IResult{T}"/>interface</returns>
    IResult<T> Error(string message, T obj, Exception ex, params object[] optionalParams);

    /// <summary>
    /// Returns success = true with message = string.empty and data = obj parameter
    /// </summary>
    /// <returns>The instance based on the <see cref="IResult{T}"/>interface</returns>
    IResult<T> Ok(T obj);

    /// <summary>
    /// Returns success = true with message = message parameter and data = obj parameter
    /// </summary>
    /// <param name="message">A non-sensitive message.</param>
    /// <param name="obj">The data object to be returned</param>
    /// <returns>The instance based on the <see cref="IResult{T}"/>interface</returns>
    IResult<T> Ok(string message, T obj);

    /// <summary>
    /// Returns success = true with message = message parameter and data = obj parameter
    /// </summary>
    /// <param name="message">A non-sensitive message.</param>
    /// <param name="obj">The data object to be returned</param>
    /// <param name="optionalParams">Not serialized.Always passed as diagnostic data.</param>
    /// <returns>The instance based on the <see cref="IResult{T}"/>interface</returns>
    IResult<T> Ok(string message, T obj, params object[] optionalParams);

    /// <summary>
    /// Returns success = true with message = string.empty and data = obj parameter
    /// </summary>
    /// <param name="obj">The data object to be returned</param>
    /// <param name="optionalParams">Not serialized.Always passed as diagnostic data.</param>
    /// <returns>The instance based on the <see cref="IResult{T}"/>interface</returns>
    IResult<T> Ok(T obj, params object[] optionalParams);
}