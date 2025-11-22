namespace Results;

public abstract class Error(string? msg = null)
{
    public string? Message { get; } = msg;
}