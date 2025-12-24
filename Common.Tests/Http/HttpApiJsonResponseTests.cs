using System;
using Common.Infra.HttpApi;
using Xunit;

namespace Common.Tests.Http;

public class HttpApiJsonResponseTests
{
    [Fact]
    public void HttpApiJsonResponse_HoldsValues()
    {
        var timestamp = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        var response = new HttpApiJsonResponse(true, "ok", timestamp);

        Assert.True(response.IsSuccess);
        Assert.Equal("ok", response.Message);
        Assert.Equal(timestamp, response.UtcTimeStamp);
    }

    [Fact]
    public void HttpApiJsonResponse_WithData_HoldsValues()
    {
        var timestamp = new DateTime(2024, 1, 2, 0, 0, 0, DateTimeKind.Utc);
        var payload = new Payload { Value = 7 };

        var response = new HttpApiJsonResponse<Payload>(true, "ok", payload, timestamp);

        Assert.True(response.IsSuccess);
        Assert.Equal("ok", response.Message);
        Assert.Equal(7, response.Data?.Value);
        Assert.Equal(timestamp, response.UtcTimeStamp);
    }

    private sealed class Payload
    {
        public int Value { get; init; }
    }
}
