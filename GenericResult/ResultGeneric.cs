using GenericResult.Interfaces;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GenericResult
{
    public class Result<T> : IResult<T>
    {
        [JsonInclude]
        public bool Success { get; set; }

        [JsonInclude]
        public string Message { get; set; }

        [JsonIgnore]
        public string[] DiagnosticData { get; set; }

        public T Object { get; set; }

        public IResult<T> Error(Exception ex, params object[] optionalParams)
            => Error(ex?.Message, optionalParams);

        public IResult<T> Error(string message, params object[] optionalParams)
        {
            List<string> strs = new();
            foreach (var obj in optionalParams ?? new object[0])
                strs.Add(JsonSerializer.Serialize(obj));
            (this.Success, this.Message, this.DiagnosticData) = (false, message, strs.ToArray());
            return this;
        }

        public IResult<T> Ok()
           => Ok(string.Empty, default);

        public IResult<T> Ok(string message)
            => Ok(message, default);

        public IResult<T> Ok(T obj)
            => Ok(string.Empty, obj);

        public IResult<T> Ok(string message, T obj)
        {
            (this.Success, this.Message, this.Object) = (true, message, obj);
            return this;
        }
    }
}