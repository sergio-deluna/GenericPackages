namespace GenericResult.Interfaces
{
    public interface IGenericStructure
    {
        bool Success { get; set; }
        string Message { get; set; }
        string[] DiagnosticData { get; set; }
    }
}