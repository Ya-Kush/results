namespace Results;

public abstract class Error(string? msg = null) : ApplicationException(msg);