using GenericResult.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Xml.Serialization;

namespace GenericResult;

public class Result : IResult
{
    /// <summary>Gets or sets a value indicating whether the operation was succesful or not.</summary>
    /// <value>
    ///   <c>true</c> if success; otherwise, <c>false</c>.
    ///   </value>
    [System.Text.Json.Serialization.JsonInclude]
    [Newtonsoft.Json.JsonProperty]
    [XmlElement]
    public bool Success { get; set; }

    /// <summary>
    ///   <para>
    /// Gets or sets the message. It is recommended to use a non-sensitive information, usually focused on users.</para>
    ///   <para>If you need to pass technical data, you can use the DiagnosticData property, which can be set using the optional parameters in helpers methods.</para>
    /// </summary>
    /// <value>The message.</value>
    [System.Text.Json.Serialization.JsonInclude]
    [Newtonsoft.Json.JsonProperty]
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
    [Newtonsoft.Json.JsonIgnore]
    [XmlIgnore]
    public string[] DiagnosticData { get; set; }

    /// <summary>Returns success = false with message = exception.Message</summary>
    /// <param name="ex">Exception details passed as the message string</param>
    /// <param name="optionalParams">Not serialized.Always passed as diagnostic data.</param>
    /// <returns>The instance based on the <see cref="IResult" />interface</returns>
    public IResult Error(Exception ex, params object[] optionalParams)
        => Error(String.Empty, ex, optionalParams);

    /// <summary>Returns success = false with message = message parameter</summary>
    /// <param name="message">A non-sensitive message.</param>
    /// <param name="optionalParams">Not serialized.Always passed as diagnostic data.</param>
    /// <returns>The instance based on the <see cref="IResult" />interface</returns>
    public IResult Error(string message, params object[] optionalParams)
        => Error(message, default, optionalParams);

    /// <summary>Returns success = false with message = message parameter
    /// The exception is not serialized, but its message exception is included as diagnostic data.</summary>
    /// <param name="message">A non-sensitive message.</param>
    /// <param name="ex">
    /// Its message exception is included as diagnostic data.
    /// This is because the exception object is too big for being serialized. If you need to serialize the exception object too,
    /// then pass the exception with the <paramref name="optionalParams" /> param.
    /// </param>
    /// <param name="optionalParams">Not serialized.Always passed as diagnostic data.</param>
    /// <returns>The instance based on the <see cref="IResult" />interface</returns>
    public IResult Error(string message, Exception ex, params object[] optionalParams)
    {
        List<string> strs = new();
        foreach (var param in optionalParams ?? new object[0])
            strs.Add(param is Exception exception ? exception.Message : JsonSerializer.Serialize(param));

        if (ex is not null)
            strs.Add(ex.Message);

        (this.Success, this.Message, this.DiagnosticData) = (false, message, strs.ToArray());
        return this;
    }

    /// <summary>Returns success = true with message = message parameter</summary>
    /// <param name="message">A non-sensitive message.</param>
    /// <returns>The instance based on the <see cref="IResult" />interface</returns>
    public IResult Ok(string message)
        => Ok(message, null);

    /// <summary>Returns success = true with message = string.empty</summary>
    /// <returns>The instance based on the <see cref="IResult" />interface</returns>
    public IResult Ok()
        => Ok(string.Empty, null);

    /// <summary>Returns success = true with message = message parameter</summary>
    /// <param name="message">A non-sensitive message.</param>
    /// <param name="optionalParams">Not serialized.Always passed as diagnostic data.</param>
    /// <returns>The instance based on the <see cref="IResult" />interface</returns>
    public IResult Ok(string message, params object[] optionalParams)
    {
        List<string> strs = new();
        foreach (var param in optionalParams ?? new object[0])
            strs.Add(param is Exception exception ? exception.Message : JsonSerializer.Serialize(param));

        (this.Success, this.Message, this.DiagnosticData) = (true, message, strs.ToArray());
        return this;
    }
}