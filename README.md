# Common Library (Raptor.Common)

[![NuGet Version](https://img.shields.io/nuget/v/Raptor.Common?label=NuGet&color=blue)](https://www.nuget.org/packages/Raptor.Common)

Reusable library for building clean, decoupled, and resilient APIs on .NET 8.

## Overview

This package provides:

- Result modeling (`Result`, `SuccessResult`, `FailureResult`)
- Error wrappers (`BusinessRuleException`, `ErrorList`)
- Clean Architecture interfaces (`IInteractor`, `IPresenter`, `IResponse`, etc.)
- Centralized HTTP client (`HttpApiClient`)
- Logging extensions for Serilog + Seq

## Installation

Install from NuGet:

```bash
dotnet add package Raptor.Common
```

Or add to your `.csproj`:

```xml
<PackageReference Include="Raptor.Common" Version="x.y.z" />
```

Replace `x.y.z` with the latest version from NuGet.

## Requirements

- .NET 8 SDK (net8.0)
- Compatible with ASP.NET Core, domain libraries, and microservices

## Project structure (updated paths)

The library now lives at `Common/` (previous nested paths were flattened).

```
Common/                      core library
  CleanArch/                 clean architecture abstractions
  Infra/HttpApi/             HTTP API client and responses
  Logging/                   Serilog + Seq extensions
  Users/                     sample user domain helpers
Common.Tests/                tests
Common.sln                   solution
Common/version               version source for packaging
```

## Usage

### Result modeling

```csharp
using Common;

Result<string> ok = Result.OK("All good");
Result<string> error = Result.Fail<string>("Something went wrong");
```

### Clean Architecture interactor

```csharp
using Common;
using Common.CleanArch;

public class GetUserInteractor : IResultInteractor<GetUserRequest, UserDto>
{
    public Task<Result<UserDto>> Handle(GetUserRequest request, CancellationToken cancellationToken)
    {
        if (request.Id <= 0)
            return Task.FromResult(Result.Fail<UserDto>("Invalid ID"));

        var user = new UserDto { Id = request.Id, Name = "Example" };
        return Task.FromResult(Result.OK(user));
    }
}
```

### Centralized HTTP client

```csharp
using Common.Infra.HttpApi;

var client = new HttpApiClient("https://api.example.com/");
var response = await client.GetAsync<MyResponse>("endpoint");
```

### Logging (Serilog + Seq)

`appsettings.json`:

```json
"CustomLogging": {
  "Project": "MyApiProject",
  "SeqUri": "http://localhost:5341",
  "LogEventLevel": "Information"
}
```

Registration:

```csharp
using Common.Logging;

builder.Services.AddLoggingServices(configuration);
```

## Versioning and releases

- PackageId: `Raptor.Common`
- Version source: `Common/version` (used by `Common/Common.csproj`)
- Recommended tags: `vX.Y.Z` (matching the NuGet package version)

### Release checklist

1. Update `Common/version` and `CHANGELOG.md`.
2. Run `dotnet test Common.sln`.
3. Run `dotnet pack Common/Common.csproj -c Release`.
4. Tag the commit: `git tag vX.Y.Z`.
5. Push branch and tags.

## Git flow (adjusted)

- `main`: stable, published code
- `develop`: integration branch
- `feature/*`: new work
- `fix/*` or `hotfix/*`: urgent fixes
- `legacy/*`: legacy snapshots (for example `legacy/common-legacy`)
- Release tags: `vX.Y.Z`

## Contributions

This is an internal package. Issues and PRs are welcome for improvements.

## License

MIT (per package metadata).
