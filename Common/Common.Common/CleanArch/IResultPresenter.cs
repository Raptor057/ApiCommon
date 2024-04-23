namespace Common.Common.CleanArch
{
    public interface IResultPresenter<TResponse> : MediatR.INotificationHandler<Result<TResponse>>
    { }
}
