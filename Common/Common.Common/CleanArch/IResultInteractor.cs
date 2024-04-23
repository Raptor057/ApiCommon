namespace Common.Common.CleanArch
{
    public interface IResultInteractor<TRequest, TResult> : MediatR.IRequestHandler<TRequest, Result<TResult>>
        where TRequest : IResultRequest<TResult>
    { }
}
