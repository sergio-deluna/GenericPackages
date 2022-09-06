# ICustomException

Simple package with an exception interface, adding a property for internal codes.


***IException***: 

Main interface:

```cs
public interface IException
{
    public string ExceptionCode { get; set; }
}
```

Exceptions: 

```cs
BusinessException
DataCorruptionException
```