# Common Library

[![NuGet Version](https://img.shields.io/nuget/v/Common.svg?label=NuGet&color=blue)](https://www.nuget.org/packages/Common)

**Reusable library for building clean, decoupled, and resilient APIs.**

This package provides components to facilitate the development of modern APIs and microservices, including:

-   Result modeling (`Result`, `SuccessResult`, `FailureResult`)
    
-   Error wrappers (`BusinessRuleException`, `ErrorList`)
    
-   Interfaces for clean architecture (`IInteractor`, `IPresenter`, `IResponse`, etc.)
    
-   Centralized HTTP API client (`HttpApiClient`)
    
-   Logging extensions for Serilog + Seq
    

----------

## 📦 Installation

Install the package from [nuget.org](https://www.nuget.org/packages/Common):

```bash
dotnet add package Common

```

Or add it to your `.csproj`:

```xml
<PackageReference Include="Common" Version="x.y.z" />

```

Replace `x.y.z` with the [latest available version](https://www.nuget.org/packages/Common).

----------

## ⚙️ Requirements

-   [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
    
-   Compatible with ASP.NET Core, domain libraries, and microservices.
    

----------

## 🚀 Basic Usage

### Standard responses with `Result`:

```csharp
using Common;

Result<string> result = Result.OK("All good");

// For errors
Result<string> error = Result.Fail<string>("Something went wrong");

```

----------

### Clean Architecture example:

```csharp
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

----------

### Centralized HTTP Client (`HttpApiClient`):

```csharp
var client = new HttpApiClient("https://api.example.com/");
var response = await client.GetAsync<MyResponse>("endpoint");

```

----------

## 🧩 Logging with Serilog + Seq

Add the following to your `appsettings.json`:

```json
"CustomLogging": {
  "Project": "MyApiProject",
  "SeqUri": "http://localhost:5341",
  "LogEventLevel": "Information"
}

```

Then register the services:

```csharp
builder.Services.AddLoggingServices(configuration);

```

----------

## 🤝 Contributions

This package is designed for internal use, but feel free to open issues or pull requests for improvements or suggestions.

----------

## 📝 License

Distributed under the MIT License. See the [`LICENSE`](https://chatgpt.com/g/g-p-67ff7cd214b48191b3d71bfeed4cfe5d-raptor/c/LICENSE) file for more details.

----------

> Built with ❤️ by the development team.

¿Quieres que lo suba a un `README.md` limpio o lo formateamos de alguna forma especial para NuGet?