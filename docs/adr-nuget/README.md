# ADR del espejo NuGet

Decisiones que son **solo** de `Raptor057/ApiCommon`: como se empaqueta y se publica en nuget.org el
codigo de `Raptor-Dev-Services/Common`.

Las decisiones de la libreria en si viven en [`../adr/`](../adr/README.md), que es copia 1:1 de
Common y no se toca aqui. Por eso esta carpeta tiene su propia numeracion (`NUGET-NNNN`): asi las dos
series nunca chocan cuando se sincroniza.

| ADR | Decision | Estado |
|---|---|---|
| [NUGET-0001](NUGET-0001-espejo-1a1-de-common.md) | El repo es espejo 1:1 de Common; el empaquetado vive en archivos aparte | Aceptado |
| [NUGET-0002](NUGET-0002-un-paquete-por-sub-libreria.md) | Un paquete NuGet por sub-libreria, mas la facade | Aceptado |
| [NUGET-0003](NUGET-0003-version-alineada-con-common.md) | La version del paquete es la del tag de Common | Aceptado |

Las tres se registraron **a posteriori** el 2026-09-24. Formato y reglas: los de
[`../adr/README.md`](../adr/README.md), con la plantilla [`../adr/0000-plantilla.md`](../adr/0000-plantilla.md).
