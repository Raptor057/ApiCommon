using System;
using System.Collections.Generic;
using Common.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Xunit;

namespace Common.Tests.Logging;

public class ServiceCollectionExTests
{
    [Fact]
    public void AddLoggingServices_RegistersLoggerFactory()
    {
        var settings = new Dictionary<string, string?>
        {
            ["CustomLogging:Project"] = "TestProject",
            ["CustomLogging:SeqUri"] = "http://localhost:5341",
            ["CustomLogging:LogEventLevel"] = "Information"
        };

        var configuration = new TestConfiguration(settings);

        var services = new ServiceCollection();
        var returned = services.AddLoggingServices(configuration);

        Assert.Same(services, returned);

        using var provider = services.BuildServiceProvider();
        var factory = provider.GetService<ILoggerFactory>();

        Assert.NotNull(factory);
    }

    private sealed class TestConfiguration : IConfigurationRoot
    {
        private readonly Dictionary<string, string?> _values;

        public TestConfiguration(IDictionary<string, string?> values)
        {
            _values = new Dictionary<string, string?>(values, StringComparer.OrdinalIgnoreCase);
        }

        public string? this[string key]
        {
            get => _values.TryGetValue(key, out var value) ? value : null;
            set => _values[key] = value;
        }

        public IEnumerable<IConfigurationSection> GetChildren() => Array.Empty<IConfigurationSection>();

        public IChangeToken GetReloadToken() => new NoopChangeToken();

        public IConfigurationSection GetSection(string key) => new TestSection(this, key);

        public IEnumerable<IConfigurationProvider> Providers => Array.Empty<IConfigurationProvider>();

        public void Reload()
        {
        }

        private sealed class TestSection : IConfigurationSection
        {
            private readonly TestConfiguration _root;
            private readonly string _path;

            public TestSection(TestConfiguration root, string path)
            {
                _root = root;
                _path = path;
            }

            public string? this[string key]
            {
                get => _root[JoinPath(_path, key)];
                set => _root[JoinPath(_path, key)] = value;
            }

            public string Key => _path.Contains(':') ? _path.Split(':')[^1] : _path;

            public string Path => _path;

            public string? Value
            {
                get => _root[_path];
                set => _root[_path] = value;
            }

            public IEnumerable<IConfigurationSection> GetChildren() => Array.Empty<IConfigurationSection>();

            public IChangeToken GetReloadToken() => _root.GetReloadToken();

            public IConfigurationSection GetSection(string key) => new TestSection(_root, JoinPath(_path, key));
        }

        private sealed class NoopChangeToken : IChangeToken
        {
            public bool ActiveChangeCallbacks => false;

            public bool HasChanged => false;

            public IDisposable RegisterChangeCallback(Action<object?> callback, object? state) => new NoopDisposable();
        }

        private sealed class NoopDisposable : IDisposable
        {
            public void Dispose()
            {
            }
        }

        private static string JoinPath(string left, string right) => $"{left}:{right}";
    }
}
