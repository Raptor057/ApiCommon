namespace Common.CleanArch;

public interface IResultRequest<TResult> : MediatR.IRequest<Result<TResult>>
{ }