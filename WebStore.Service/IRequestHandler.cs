namespace WebStore.Service
{
    public interface IRequestHandler<in TRequest, TResponse>
    {
        Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken = default);
    }

    public interface IRequestHandler<TResponse>
    {
        Task<TResponse> Handle(CancellationToken cancellationToken = default);
    }
}
