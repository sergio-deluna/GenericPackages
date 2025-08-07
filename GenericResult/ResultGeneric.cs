using GenericResult.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;

namespace GenericResult;

public class Result<T> : IResult<T>
{
    /// <summary>Gets or sets a value indicating whether the operation was succesful or not.</summary>
    /// <value>
    ///   <c>true</c> if success; otherwise, <c>false</c>.
    ///   </value>
    [System.Text.Json.Serialization.JsonInclude]
    [XmlElement]
    public bool Success { get; set; }

    /// <summary>
    ///   <para>
    /// Gets or sets the message. It is recommended to use a non-sensitive information, usually focused on users.</para>
    ///   <para>If you need to pass technical data, you can use the DiagnosticData property, which can be set using the optional parameters in helpers methods.</para>
    /// </summary>
    /// <value>The message.</value>
    [System.Text.Json.Serialization.JsonInclude]
    [XmlElement]
    public string Message { get; set; }

    /// <summary>
    ///   <para>
    /// Gets or sets the diagnostic data for technical purposes.</para>
    ///   <para>This data will not be serialized when using a Json or Xml serializer, so you can set any value for logging or debugging purposes without including sensitive information, specially when building API's responses.</para>
    ///   <para>When using helper methods, it is filled through the optional parameters (params array), so you can pass any number of data, which are serialized as json. If you pass an exception, it wont be serialized, instead, the Message property will be used, because exceptions are too big to be serialized quickly.</para>
    /// </summary>
    /// <value>The diagnostic data.</value>
    [System.Text.Json.Serialization.JsonIgnore]
    [XmlIgnore]
    public string[] DiagnosticData { get; set; }

    /// <summary>
    /// The data object to be returned.
    /// </summary>
    public T Object { get; set; }

    /// <summary>
    /// Gets the string representation of each property, including the diagnostic data.
    /// The purpose of this override is to be used only to print values to the console or output window.
    /// </summary>
    /// <returns>
    /// A <see cref="System.String" /> that describes this instance.
    /// </returns>
    public override string ToString()
    {
        var str = new StringBuilder();
        str.AppendLine($"{nameof(Success)}: {Success}");
        str.AppendLine($"{nameof(Message)}: {Message}");
        str.AppendLine($"{nameof(Object)}:  {JsonSerializer.Serialize(this.Object ?? default)}");

        if (DiagnosticData is not null && DiagnosticData.Any())
        {
            str.AppendLine($"{nameof(DiagnosticData)}: ");
            for (var index = 0; index < DiagnosticData?.Length; index++)
                str.AppendLine($" [{index}]: {DiagnosticData[index]}");
        }

        return str.ToString();
    }

    /// <summary>
    /// Returns success = false but still returning data for whatever reason.
    /// </summary>
    /// <param name="obj">The data object to be returned</param>
    /// <returns>The instance based on the <see cref="GenericResult.Interfaces.IResult{T}" />interface</returns>
    public IResult<T> Error(T obj)
        => this.Error(string.Empty, obj, default, null);

    /// <summary>
    /// Returns success = false with message = message parameter, but still returning data for whatever reason.
    /// </summary>
    /// <param name="message">A non-sensitive message.</param>
    /// <param name="obj">The data object to be returned</param>
    /// <param name="optionalParams">Not serialized.Always passed as diagnostic data.</param>
    /// <returns>The instance based on the <see cref="GenericResult.Interfaces.IResult{T}" />interface</returns>
    public IResult<T> Error(string message, T obj)
        => this.Error(message, obj, default, null);

    /// <summary>
    /// Returns success = false but still returning data for whatever reason.
    /// The exception is not serialized, but its message exception is included as diagnostic data.
    /// </summary>
    /// <param name="obj">The data object to be returned</param>
    /// <param name="ex">
    /// Its message exception is included as diagnostic data.
    /// This is because the exception object is too big for being serialized. If you need to serialize the exception object too,
    /// then pass the exception with the <paramref name="optionalParams" /> param.
    /// </param>
    /// <returns>The instance based on the <see cref="GenericResult.Interfaces.IResult{T}" />interface</returns>
    public IResult<T> Error(T obj, Exception ex)
        => this.Error(string.Empty, obj, ex, null);

    /// <summary>
    /// Returns success = false
    /// The exception is not serialized, but its message exception is included as diagnostic data.
    /// </summary>
    /// <param name="ex">
    /// Its message exception is included as diagnostic data.
    /// This is because the exception object is too big for being serialized. If you need to serialize the exception object too,
    /// then pass the exception with the <paramref name="optionalParams" /> param.
    /// </param>
    /// <returns>The instance based on the <see cref="GenericResult.Interfaces.IResult{T}" />interface</returns>
    public IResult<T> Error(Exception ex)
        => this.Error(string.Empty, default, ex, null);

    /// <summary>
    /// Returns success = false with message = message parameter, but still returning data for whatever reason.
    /// The exception is not serialized, but its message exception is included as diagnostic data.
    /// </summary>
    /// <param name="message">A non-sensitive message.</param>
    /// <param name="obj">The data object to be returned</param>
    /// <param name="ex">
    /// Its message exception is included as diagnostic data.
    /// This is because the exception object is too big for being serialized. If you need to serialize the exception object too,
    /// then pass the exception with the <paramref name="optionalParams" /> param.
    /// </param>
    /// <param name="optionalParams">Not serialized.Always passed as diagnostic data.</param>
    /// <returns>The instance based on the <see cref="GenericResult.Interfaces.IResult{T}" />interface</returns>
    public IResult<T> Error(string message, T obj, Exception ex, params object[] optionalParams)
    {
        List<string> strs = [];
        foreach (var param in optionalParams ?? [])
            strs.Add(param is Exception exception ? exception.Message : JsonSerializer.Serialize(param));

        if (ex is not null)
            strs.Add(ex.Message);

        (this.Success, this.Message, this.Object, this.DiagnosticData) = (false, message, obj, strs.ToArray());
        return this;
    }

    /// <summary>Returns success = true with message = string.empty and data = obj parameter</summary>
    /// <param name="obj">The data object to be returned</param>
    /// <returns>The instance based on the <see cref="GenericResult.Interfaces.IResult{T}" />interface</returns>
    public IResult<T> Ok(T obj)
        => Ok(string.Empty, obj, null);

    /// <summary>Returns success = true with message = message parameter and data = obj parameter</summary>
    /// <param name="message">A non-sensitive message.</param>
    /// <param name="obj">The data object to be returned</param>
    /// <returns>The instance based on the <see cref="GenericResult.Interfaces.IResult{T}" />interface</returns>
    public IResult<T> Ok(string message, T obj)
        => this.Ok(message, obj, null);

    /// <summary>Returns success = true with message = string.empty and data = obj parameter</summary>
    /// <param name="obj">The data object to be returned</param>
    /// <param name="optionalParams">Not serialized.Always passed as diagnostic data.</param>
    /// <returns>The instance based on the <see cref="GenericResult.Interfaces.IResult{T}" />interface</returns>
    public IResult<T> Ok(T obj, params object[] optionalParams)
        => this.Ok(string.Empty, obj, optionalParams);

    /// <summary>Returns success = true with message = message parameter and data = obj parameter</summary>
    /// <param name="message">A non-sensitive message.</param>
    /// <param name="obj">The data object to be returned</param>
    /// <param name="optionalParams">Not serialized.Always passed as diagnostic data.</param>
    /// <returns>The instance based on the <see cref="GenericResult.Interfaces.IResult{T}" />interface</returns>
    public IResult<T> Ok(string message, T obj, params object[] optionalParams)
    {
        List<string> strs = [];
        foreach (var param in optionalParams ?? [])
            strs.Add(param is Exception exception ? exception.Message : JsonSerializer.Serialize(param));

        (this.Success, this.Message, this.Object, this.DiagnosticData) = (true, message, obj, strs.ToArray());
        return this;
    }
}