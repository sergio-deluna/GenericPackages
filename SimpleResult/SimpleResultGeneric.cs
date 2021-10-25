using SimpleResult.Interfaces;
using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SimpleResult
{
    public class SimpleResultGeneric<T> : IResult<T> where T : class
    {
        [JsonInclude]
        public bool Success { get; set; }

        [JsonInclude]
        public string Message { get; set; }

        [JsonIgnore]
        public string DiagnosticData { get; set; }

        public T Object { get; set; }

        public IResult<T> Error(Exception ex, params object[] optionalParams)
            => Error(ex?.Message, optionalParams);

        public IResult<T> Error(string message, params object[] optionalParams)
        {
            StringBuilder strs = new();
            foreach (var obj in optionalParams)
                strs.AppendLine(JsonSerializer.Serialize(obj));
            (this.Success, this.Message, this.DiagnosticData) = (false, message, strs.ToString());
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