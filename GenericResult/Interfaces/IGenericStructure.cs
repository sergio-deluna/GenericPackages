namespace GenericResult.Interfaces
{
    public interface IGenericStructure
    {
        /// <summary>Gets or sets a value indicating whether the operation was succesful or not.</summary>
        /// <value>
        ///   <c>true</c> if success; otherwise, <c>false</c>.</value>
        bool Success { get; set; }

        /// <summary>
        ///   <para>
        /// Gets or sets the message. It is recommended to use a non-sensitive information, usually focused on users.</para>
        ///   <para>If you need to pass technical data, you can use the DiagnosticData property, which can be set using the optional parameters in helpers methods.</para>
        /// </summary>
        /// <value>The message.</value>
        string Message { get; set; }
        /// <summary>
        ///   <para>
        /// Gets or sets the diagnostic data for technical purposes.</para>
        ///   <para>This data will not be serialized when using a Json or Xml serializer, so you can set any value for logging or debugging purposes without including sensitive information, specially when building API's responses.</para>
        ///   <para>When using helper methods, it is filled through the optional parameters (params array), so you can pass any number of data, which are serialized as json. If you pass an exception, it wont be serialized, instead, the Message property will be used, because exceptions are too big to be serialized quickly.</para>
        /// </summary>
        /// <value>The diagnostic data.</value>
        string[] DiagnosticData { get; set; }
    }
}