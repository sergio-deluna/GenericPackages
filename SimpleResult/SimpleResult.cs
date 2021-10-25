using SimpleResult.Interfaces;
using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SimpleResult
{
    public class SimpleResult : IResult
    {
        [JsonInclude]
        public bool Success { get; set; }

        [JsonInclude]
        public string Message { get; set; }

        [JsonIgnore]
        public string DiagnosticData { get; set; }

        public IResult Error(Exception ex, params object[] optionalParams)
            => Error(ex?.Message, optionalParams);

        public IResult Error(string message, params object[] optionalParams)
        {
            StringBuilder strs = new();
            foreach (var obj in optionalParams)
                strs.AppendLine(JsonSerializer.Serialize(obj));
            (this.Success, this.Message, this.DiagnosticData) = (false, message, strs.ToString());
            return this;
        }

        public IResult Ok(string message)
        {
            (this.Success, this.Message) = (true, message);
            return this;
        }

        public IResult Ok()
        {
            (this.Success, this.Message) = (true, string.Empty);
            return this;
        }
    }
}