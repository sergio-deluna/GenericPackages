using GenericResult.Interfaces;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GenericResult
{
    public class Result<T> : IxResult<T>
    {
        [JsonInclude]
        public bool Success { get; set; }

        [JsonInclude]
        public string Message { get; set; }

        [JsonIgnore]
        public string[] DiagnosticData { get; set; }

        public T Object { get; set; }

        public IxResult<T> Error(Exception ex, params object[] optionalParams)
            => this.Error(string.Empty, default, ex, optionalParams);

        public IxResult<T> Error(string message, params object[] optionalParams)
            => this.Error(message, default, default, optionalParams);

        public IxResult<T> Error(string message, T obj, params object[] optionalParams)
            => this.Error(message, obj, default, optionalParams);

        public IxResult<T> Error(string message, Exception ex, params object[] optionalParams)
            => this.Error(message, default, ex, optionalParams);

        public IxResult<T> Error(string message, T obj, Exception ex, params object[] optionalParams)
        {
            List<string> strs = new();
            foreach (var param in optionalParams ?? new object[0])
                strs.Add(JsonSerializer.Serialize(param));

            if (ex is not null)
                strs.Add(ex.Message);

            (this.Success, this.Message, this.DiagnosticData) = (false, message, strs.ToArray());
            return this;
        }

        public IxResult<T> Ok()
           => Ok(string.Empty, default, null);

        public IxResult<T> Ok(string message)
            => Ok(message, default, null);

        public IxResult<T> Ok(T obj)
            => Ok(string.Empty, obj, null);

        public IxResult<T> Ok(string message, T obj)
            => this.Ok(message, obj, null);

        public IxResult<T> Ok(string message, params object[] optionalParams)
            => this.Ok(message, default, optionalParams);

        public IxResult<T> Ok(string message, T obj, params object[] optionalParams)
        {
            List<string> strs = new();
            foreach (var param in optionalParams ?? new object[0])
                strs.Add(JsonSerializer.Serialize(param));

            (this.Success, this.Message, this.Object, this.DiagnosticData) = (true, message, obj, strs.ToArray());
            return this;
        }

        public IxResult<T> Ok(T obj, params object[] optionalParams)
         => this.Ok(string.Empty, obj, optionalParams);
    }
}