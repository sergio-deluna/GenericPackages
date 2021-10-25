using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GenericResult
{
    public class Result : Interfaces.IResult
    {
        [JsonInclude]
        public bool Success { get; set; }

        [JsonInclude]
        public string Message { get; set; }

        [JsonIgnore]
        public string[] DiagnosticData { get; set; }

        public Interfaces.IResult Error(Exception ex, params object[] optionalParams)
            => Error(ex?.Message, optionalParams);

        public Interfaces.IResult Error(string message, params object[] optionalParams)
        {
            List<string>strs = new();
            foreach (var obj in optionalParams?? new object[0])
                strs.Add(JsonSerializer.Serialize(obj));
            (this.Success, this.Message, this.DiagnosticData) = (false, message, strs.ToArray());
            return this;
        }

        public Interfaces.IResult Ok(string message)
        {
            (this.Success, this.Message) = (true, message);
            return this;
        }

        public Interfaces.IResult Ok()
        {
            (this.Success, this.Message) = (true, string.Empty);
            return this;
        }
    }
}