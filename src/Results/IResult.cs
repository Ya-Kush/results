namespace Results;

public interface IResult<E> where E : Error
{
    bool Success { get; }
    E Error { get; }
}