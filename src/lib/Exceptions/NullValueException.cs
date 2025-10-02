namespace Results.Exceptions;

public class NullValueException(string? message = null) : ResultsException(message);