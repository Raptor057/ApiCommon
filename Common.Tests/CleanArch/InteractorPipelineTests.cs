using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Common.CleanArch;
using MediatR;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace Common.Tests.CleanArch;

public class InteractorPipelineTests
{
    [Fact]
    public async Task Handle_PublishesResponse_OnSuccess()
    {
        var mediator = new FakeMediator();
        var logger = NullLogger<InteractorPipeline<PingRequest, Result<string>>>.Instance;
        var pipeline = new InteractorPipeline<PingRequest, Result<string>>(mediator, logger);

        var request = new PingRequest("ok");
        var response = await pipeline.Handle(request, _ => Task.FromResult<Result<string>>(Result.OK("ok")), CancellationToken.None);

        Assert.IsType<SuccessResult<string>>(response);
        Assert.Single(mediator.Published);
        Assert.Same(response, mediator.Published[0]);
    }

    [Fact]
    public async Task Handle_PublishesResponse_OnFailure()
    {
        var mediator = new FakeMediator();
        var logger = NullLogger<InteractorPipeline<PingRequest, Result<string>>>.Instance;
        var pipeline = new InteractorPipeline<PingRequest, Result<string>>(mediator, logger);

        var response = await pipeline.Handle(new PingRequest("bad"), _ => Task.FromResult<Result<string>>(Result.Fail<string>("fail")), CancellationToken.None);

        Assert.IsType<FailureResult<string>>(response);
        Assert.Single(mediator.Published);
        Assert.Same(response, mediator.Published[0]);
    }

    [Fact]
    public async Task Handle_RethrowsBusinessRuleException()
    {
        var mediator = new FakeMediator();
        var logger = NullLogger<InteractorPipeline<PingRequest, Result<string>>>.Instance;
        var pipeline = new InteractorPipeline<PingRequest, Result<string>>(mediator, logger);

        var ex = new BusinessRuleException("rule");

        await Assert.ThrowsAsync<BusinessRuleException>(() =>
            pipeline.Handle(new PingRequest("bad"), _ => Task.FromException<Result<string>>(ex), CancellationToken.None));
    }

    private sealed record PingRequest(string Value) : MediatR.IRequest<Result<string>>;

    private sealed class FakeMediator : IMediator
    {
        public List<object> Published { get; } = new();

        public Task Publish(object notification, CancellationToken cancellationToken = default)
        {
            Published.Add(notification);
            return Task.CompletedTask;
        }

        public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
            where TNotification : INotification
        {
            Published.Add(notification!);
            return Task.CompletedTask;
        }

        public Task<TResponse> Send<TResponse>(MediatR.IRequest<TResponse> request, CancellationToken cancellationToken = default)
            => throw new NotImplementedException();

        public Task<object?> Send(object request, CancellationToken cancellationToken = default)
            => throw new NotImplementedException();

        public Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default)
            where TRequest : MediatR.IRequest
            => throw new NotImplementedException();

        public IAsyncEnumerable<TResponse> CreateStream<TResponse>(MediatR.IStreamRequest<TResponse> request, CancellationToken cancellationToken = default)
            => throw new NotImplementedException();

        public IAsyncEnumerable<object?> CreateStream(object request, CancellationToken cancellationToken = default)
            => throw new NotImplementedException();
    }
}
