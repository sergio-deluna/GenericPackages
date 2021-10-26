# IResult
Simple set of generic structures when simple success?->message->data information is required.

## Interfaces   
There are two main interfaces for returning results:\
***IResult***: 
```cs
        bool Success            
        string Message
        string[] DiagnosticData  //Not included when serialized into json representation. Just for debugging purposes
```
***IResult\<T>***:
```cs
        // Same structure as IResult, but including the following property
        T Object 
```
## Helper Methods
And some methods for making easy and simplified response constructions \
***IResult***
```cs
        IResult Error(Exception ex, params object[] optionalParams);

        IResult Error(string message, params object[] optionalParams);

        IResult Ok(string message);

        IResult Ok();
```
***IResult\<T>***
```cs
        IResult<T> Error(Exception ex, params object[] optionalParams);

        IResult<T> Error(string message, params object[] optionalParams);

        IResult<T> Ok();

        IResult<T> Ok(string message);

        IResult<T> Ok(string message, T obj);

        IResult<T> Ok(T obj);
```
## Quick Examples
Some examples: 
```cs

        // simple result without any data
        IResult res = new Result().Ok();

        // simple error result
        IResult res = new Result().Error("Error test message");

        // typical exception handling pattern, with aditional data
        IResult<bool> res;
        ...
        catch (System.Exception ex)
        {
            object[] someData = new object[] { "uno", "dos", 3, 4.0, null };
            res = new Result<bool>().Error(ex, true, false, someData, "ultimo");
        }
```

## Source Code
https://github.com/sergio-deluna/IResult