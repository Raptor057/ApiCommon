# GTM.Common

[![NuGet Version](https://img.shields.io/nuget/v/GTM.Common.svg?label=NuGet&color=blue)](https://www.nuget.org/packages/GTM.Common)

**Librería común para las APIs de GTM y el ecosistema Raptor057**

Este paquete contiene componentes reutilizables para facilitar el desarrollo de APIs limpias, desacopladas y resilientes, incluyendo:
- Modelo de resultados (`Result`, `SuccessResult`, `FailureResult`)
- Envoltorios para errores (`BusinessRuleException`, `ErrorList`)
- Interfaces para arquitectura limpia (`IInteractor`, `IPresenter`, `IResponse`, etc.)
- Cliente HTTP API (`HttpApiClient`)
- Extensión para logging con Serilog + Seq

---

## 📦 Instalación

Puedes instalar el paquete desde [nuget.org](https://www.nuget.org/packages/GTM.Common):

```bash
dotnet add package GTM.Common

```

o en tu `.csproj`:

```xml
<PackageReference Include="GTM.Common" Version="x.y.z" />

```

Reemplaza `x.y.z` por la [última versión publicada](https://www.nuget.org/packages/GTM.Common).

----------

## ⚙️ Requisitos

-   [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
    
-   Compatible con proyectos ASP.NET Core, librerías de dominio y microservicios.
    

----------

## 🚀 Uso básico

### Respuestas estándar con `Result`:

```csharp
using Common;

Result<string> resultado = Result.OK("Todo bien");

// Para fallos
Result<string> error = Result.Fail<string>("Algo falló");

```

----------

### Aplicación de arquitectura limpia:

```csharp
public class GetUserInteractor : IResultInteractor<GetUserRequest, UserDto>
{
    public Task<Result<UserDto>> Handle(GetUserRequest request, CancellationToken cancellationToken)
    {
        if (request.Id <= 0)
            return Task.FromResult(Result.Fail<UserDto>("ID inválido"));

        var user = new UserDto { Id = request.Id, Name = "Ejemplo" };
        return Task.FromResult(Result.OK(user));
    }
}

```

----------

### Cliente HTTP centralizado (`HttpApiClient`):

```csharp
var client = new HttpApiClient("https://api.ejemplo.com/");
var response = await client.GetAsync<MyResponse>("endpoint");

```

----------

## 🧩 Logging con Serilog + Seq

Agrega al `appsettings.json`:

```json
"CustomLogging": {
  "Project": "MiProyectoAPI",
  "SeqUri": "http://localhost:5341",
  "LogEventLevel": "Information"
}

```

Y registra los servicios:

```csharp
builder.Services.AddLoggingServices(configuration);

```

----------

## 🤝 Contribuciones

Este paquete está pensado para uso interno, pero si tienes mejoras o ideas útiles, puedes abrir un issue o pull request.

----------

## 📝 Licencia

Distribuido bajo licencia MIT. Consulta el archivo [`LICENSE`](https://chatgpt.com/g/g-p-67ff7cd214b48191b3d71bfeed4cfe5d-raptor/c/LICENSE) para más detalles.

----------

> Hecho con ❤️ por [Rogelio Arriaga](https://www.linkedin.com/in/rogelio-arri/) y  [Marcos Vazquez](https://www.linkedin.com/in/marcosjvc/)