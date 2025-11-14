namespace Results;

public interface IResult<E> where E : Exception
{
    bool Success { get; }
    E Exception { get; }
}