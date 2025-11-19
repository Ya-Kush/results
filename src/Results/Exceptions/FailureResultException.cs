namespace Results.Exceptions;

public class FailureResultException(Error error) : ResultException(error.Message);