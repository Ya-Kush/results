namespace Results;

public interface IVoidResult<E> where E : Exception
{
    bool Success { get; }
    E Exception { get; }
}
public interface IVoidResult : IVoidResult<Exception>;

public interface ITypedResult<V, E> : IVoidResult<E> where E : Exception
{
    V Value { get; }
}
public interface ITypedResult<V> : ITypedResult<V, Exception>;