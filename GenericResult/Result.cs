using GenericResult.Interfaces;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GenericResult
{
    public class Result : Interfaces.IxResult
    {
        [JsonInclude]
        public bool Success { get; set; }

        [JsonInclude]
        public string Message { get; set; }

        [JsonIgnore]
        public string[] DiagnosticData { get; set; }

        public Interfaces.IxResult Error(Exception ex, params object[] optionalParams)
            => Error(String.Empty, ex, optionalParams);

        public Interfaces.IxResult Error(string message, params object[] optionalParams)
            => Error(message, default, optionalParams);

        public IxResult Error(string message, Exception ex, params object[] optionalParams)
        {
            List<string> strs = new();
            foreach (var obj in optionalParams ?? new object[0])
                strs.Add(JsonSerializer.Serialize(obj));

            if (ex is not null)
                strs.Add(ex.Message);

            (this.Success, this.Message, this.DiagnosticData) = (false, message, strs.ToArray());
            return this;
        }

        public Interfaces.IxResult Ok(string message)
            => Ok(message, null);

        public Interfaces.IxResult Ok()
            => Ok(string.Empty, null);

        public IxResult Ok(string message, params object[] optionalParams)
        {
            List<string> strs = new();
            foreach (var obj in optionalParams ?? new object[0])
                strs.Add(JsonSerializer.Serialize(obj));

            (this.Success, this.Message, this.DiagnosticData) = (true, message, strs.ToArray());
            return this;
        }
    }
}