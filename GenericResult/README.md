# IxResult
Simple set of generic structures when simple success?->message->data information is required.

## Interfaces   
There are two main interfaces for returning results:\
***IxResult***: 
```cs
        bool Success            
        string Message        
        string[] DiagnosticData  //Not included when serialized into json representation. Just for debugging purposes
```
***IxResult\<T>***:
```cs
        // Same structure as IxResult, but including the following property
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
***IxResult***
```cs
        // sets Success = false. More details can be found in the comments in the source code or via intellisense
        IxResult Error(Exception ex, params object[] optionalParams);    
        IxResult Error(string message, params object[] optionalParams);    
        IxResult Error(string message, Exception ex, params object[] optionalParams); 

        // sets Success = true. More details can be found in the comments in the source code or via intellisense
        IxResult Ok();
        IxResult Ok(string message);
        IxResult Ok(string message, params object[] optionalParams);

        // call to logger.Log(this.Success ? LogLevel.Information : LogLevel.Error, this.Message, this.DiagnosticData)
        // intended for making shorter and simpler lines of code. Does not catch any exception. 
        IxResult Log(ILogger logger);

```
***IxResult\<T>***
```cs
        // sets Success = false. More details can be found in the comments in the source code or via intellisense
        IxResult<T> Error(Exception ex, params object[] optionalParams);
        IxResult<T> Error(string message, params object[] optionalParams);
        IxResult<T> Error(string message, T obj, params object[] optionalParams);
        IxResult<T> Error(string message, Exception ex, params object[] optionalParams);
        IxResult<T> Error(string message, T obj, Exception ex, params object[] optionalParams);

        // sets Success = true. More details can be found in the comments in the source code or via intellisense
        IxResult<T> Ok(T obj);
        IxResult<T> Ok(string message, T obj);
        IxResult<T> Ok(string message, T obj, params object[] optionalParams);
        IxResult<T> Ok(T obj, params object[] optionalParams);

        // call to logger.Log(this.Success ? LogLevel.Information : LogLevel.Error, this.Message, this.DiagnosticData)
        // intended for making shorter and simpler lines of code. Does not catch any exception. 
        IxResult<T> Log(ILogger logger);
```
## Quick Examples
Some examples: 
```cs

        // simple result without any data
        IxResult res = new Result().Ok();

        // simple error result
        IxResult res = new Result().Error("Error test message");

        // typical exception handling pattern, with aditional data
        IxResult<bool> res;
        ...
        catch (System.Exception ex)
        {
            object[] someData = new object[] { "uno", "dos", 3, 4.0, null };
            res = new Result<bool>().Error(ex, true, false, someData, "ultimo");
        }
```
