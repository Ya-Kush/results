namespace Results.Exceptions;

public class UninitializedResultException(string? message = null) : ResultException(message);