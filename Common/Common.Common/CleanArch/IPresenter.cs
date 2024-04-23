namespace Common.Common.CleanArch
{
    public interface IPresenter<TResult> : MediatR.INotificationHandler<TResult>
        where TResult : IResponse
    { }
}
