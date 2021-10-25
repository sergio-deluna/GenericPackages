namespace SimpleResult.Interfaces
{
    public interface IGenericStructure
    {
        bool Success { get; set; }
        string Message { get; set; }
        public string DiagnosticData { get; set; }
    }
}