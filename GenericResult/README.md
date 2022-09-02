# IResult
Simple set of generic structures when simple success?->message->data information is required.

## Interfaces   
There are two main interfaces for returning results:\
***IResult***: 
```cs
        bool Success            
        string Message        
        string[] DiagnosticData  //Not included when serialized into json or xml representation. Just for debugging purposes
```
***IResult\<T>***:
```cs
        // Same structure as IResult, but including the following property
        T Object 
```

***DiagnosticData***: Intended for technical purposes.  

This data will not be serialized when using a Json or Xml serializer, so you can set any value for logging or debugging purposes without including sensitive information, specially when building API's responses.

When using helper methods, it is filled through the optional parameters (params array), so you can pass any number of data, which are serialized as json. If you pass an exception, it wont be serialized, instead, the Message property will be used, because exceptions are too big to be serialized quickly.

```cs
        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        [XmlIgnore]
        string[] DiagnosticData
```

## Helper Methods
And some methods for making easy and simplified response constructions \
***IResult***
```cs
        // sets Success = false. More details can be found in the comments in the source code or via intellisense
        IResult Error(Exception ex, params object[] optionalParams);    
        IResult Error(string message, params object[] optionalParams);    
        IResult Error(string message, Exception ex, params object[] optionalParams); 

        // sets Success = true. More details can be found in the comments in the source code or via intellisense
        IResult Ok();
        IResult Ok(string message);
        IResult Ok(string message, params object[] optionalParams);
```
***IResult\<T>***
```cs
        // sets Success = false. More details can be found in the comments in the source code or via intellisense
        IResult<T> Error(T obj);
        IResult<T> Error(T obj, Exception ex);
        IResult<T> Error(string message, T obj);
        IResult<T> Error(string message, T obj, Exception ex, params object[] optionalParams);

        // sets Success = true. More details can be found in the comments in the source code or via intellisense
        IResult<T> Ok(T obj);
        IResult<T> Ok(string message, T obj);
        IResult<T> Ok(T obj, params object[] optionalParams);
        IResult<T> Ok(string message, T obj, params object[] optionalParams);
```
***ToString() Override***

Both classes Result & Result\<T> have a ToString() overridden method. 
The intention of this, is to get details quickly, as printing to the console or the output window.

An example of this:
```cs
Success: False
Message: Error test message
Object:  {"Id":"c32426fe-f47f-4299-9a96-2914e1cd4307","Text":"Test text"}
DiagnosticData: 
 [0]: "uno"
 [1]: "dos"
 [2]: 3
 [3]: 4
 [4]: null
 [5]: Exception error test message
 ```

## Quick Examples
IResult examples: 
```cs
        object[] optionalParams = new object[] { "uno", "dos", 3, 4.0, null };
        var msgEx = "Exception error test message";
        var msg = "Error test message";

        // Simple result with Success = true and Message = msg
        IResult res = new Result().Ok(msg);
        
        // Simple error result with success = false with message = msg
        // and DiagnosticData filled with the exception.Message and each optionalParams values as Json data.
        res = new Result().Error(msg, new InvalidOperationException(msgEx), optionalParams);    
```

IResult<T> examples: 
```cs           
        var msg = "Success";
        var ex = new InvalidOperationException("Error");
        object[] optionalParams = new object[] { "uno", "dos", 3, 4.0, null };

        var data = new mockClass
        {
            Id = Guid.NewGuid(),
            Text = "Test text"
        };

        // Result with Success=true, Message = msg, Object = data
        // and DiagnosticData filled with each optionalParams values as Json data.
        var res = new Result<mockClass>().Ok(msg, data, optionalParams);

        // Result with Success=false, Object = data
        // and DiagnosticData with the exception.Message
        var res = new Result<mockClass>().Error(data, ex);            
```