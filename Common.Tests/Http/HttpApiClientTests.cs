using System;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Common.Infra.HttpApi;
using Xunit;

namespace Common.Tests.Http;

public class HttpApiClientTests
{
    [Fact]
    public async Task ParseResponseAsync_ReturnsFailure_WhenEmpty()
    {
        var response = new HttpResponseMessage(HttpStatusCode.NoContent)
        {
            ReasonPhrase = "No Content",
            Content = new StringContent(string.Empty, Encoding.UTF8, "application/json")
        };

        var parsed = await InvokeParseResponseAsync<string>(response);

        Assert.False(parsed.IsSuccess);
        Assert.Equal("No Content", parsed.Message);
        Assert.Null(parsed.Data);
    }

    [Fact]
    public async Task ParseResponseAsync_ReturnsData_WhenValidJson()
    {
        var timestamp = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var json = $"{{\"isSuccess\":true,\"message\":\"ok\",\"data\":{{\"value\":42}},\"utcTimeStamp\":\"{timestamp:O}\"}}";

        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };

        var parsed = await InvokeParseResponseAsync<SampleData>(response);

        Assert.True(parsed.IsSuccess);
        Assert.Equal("ok", parsed.Message);
        Assert.Equal(42, parsed.Data?.Value);
        Assert.Equal(timestamp, parsed.UtcTimeStamp.ToUniversalTime());
    }

    private static Task<HttpApiJsonResponse<T>> InvokeParseResponseAsync<T>(HttpResponseMessage response)
    {
        var method = typeof(HttpApiClient).GetMethod("ParseResponseAsync", BindingFlags.NonPublic | BindingFlags.Static);
        Assert.NotNull(method);

        var generic = method!.MakeGenericMethod(typeof(T));
        var result = generic.Invoke(null, new object[] { response });

        return (Task<HttpApiJsonResponse<T>>)result!;
    }

    private sealed class SampleData
    {
        public int Value { get; init; }
    }
}
