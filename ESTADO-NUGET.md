# Estado del espejo NuGet - para continuar en otra maquina

**Cerrado el:** 2026-09-24. Se sobrescribe en cada cierre.

Este repo es copia 1:1 de [`Raptor-Dev-Services/Common`](https://github.com/Raptor-Dev-Services/Common)
mas lo necesario para publicar en nuget.org ([`docs/adr-nuget/`](docs/adr-nuget/README.md)). El estado
de la libreria en si esta en el `ESTADO.md` de Common; aqui solo el del paquete.

## Donde quedo

- Rama `main` en `52ce972`, empujada, arbol limpio, sin otras ramas. Ultimo tag: **`v2.1.3`**, copia
  de Common `v2.1.3` (`dace97a`), comprobada archivo por archivo.
- **nuget.org:** `Raptor.Common`, `.Contracts`, `.Messaging`, `.MultiTenancy`, `.Infra` y `.Web` en
  **2.1.3**, publicados por el workflow `Publish` e indexados. Las versiones 2.0.0, 2.1.0 y 2.1.2 nunca
  se publicaron (ver [`CHANGELOG-NUGET.md`](CHANGELOG-NUGET.md)).
- Tag `archive/common-legacy`: la version previa al espejo, conservada al borrar su rama.
- CI y Publish en verde en `ubuntu-24.04` con `actions/checkout@v7` y `actions/setup-dotnet@v6`.

## Lo que sigue

Nada pendiente. La proxima publicacion es la siguiente version de Common: el procedimiento esta en su
`CONTRIBUTING.md` (tambien copiado aqui).

## Lo que NO viaja con el `pull`

- **La politica de Trusted Publishing vive en nuget.org**, no en el repo: cuenta `Raptor057`, repo
  `ApiCommon`, workflow `publish.yml`, sin environment, alcance "empujar paquetes nuevos y versiones",
  patron `Raptor.Common*`. Si se renombra `publish.yml` o se mueve el repo, la publicacion falla hasta
  actualizarla ([NUGET-0004](docs/adr-nuget/NUGET-0004-trusted-publishing.md)).
- **El secreto `NUGET_USER`** del repo lleva el usuario de nuget.org (no el correo). Ya no hay llave de
  API: se borro.
- **Antes de sincronizar**, revisa si Common trae un archivo nuevo con el nombre de uno propio del
  espejo: asi se piso el `CHANGELOG.md` en la 2.1.2 ([NUGET-0001](docs/adr-nuget/NUGET-0001-espejo-1a1-de-common.md)).
