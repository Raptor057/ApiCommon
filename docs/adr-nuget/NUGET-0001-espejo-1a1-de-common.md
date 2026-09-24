# NUGET-0001 - El repo es espejo 1:1 de Common; el empaquetado vive en archivos aparte

**Fecha:** 2026-09-24 (registrado a posteriori el mismo dia)
**Estado:** Aceptado
**Proyecto:** ApiCommon (paquetes `Raptor.Common.*`)
**Origen:** commit `114c51a`.

---

## Contexto

`Raptor057/ApiCommon` publicaba `Raptor.Common` 0.0.x: una libreria net8 con MediatR que se quedo
atras. Su sucesora se desarrolla en `Raptor-Dev-Services/Common`, que se consume por submodulo y no
publica paquetes. Se quiere seguir publicando en nuget.org, con el codigo de Common y sin mantener
dos librerias.

---

## Opciones consideradas

### Opcion A - Publicar desde Common directamente

**Contras:** mete en el repo de la organizacion la configuracion y los secretos de publicacion de una
cuenta personal.

### Opcion B - Espejo que adapta el codigo (renombra, cambia los `.csproj`)

**Contras:** cada sincronizacion es un merge a mano, y el espejo se aleja de Common con el tiempo.

### Opcion C - Espejo 1:1, con lo propio en archivos que Common no tiene

**Pros:** sincronizar es copiar encima; se puede comprobar blob a blob que el codigo es el mismo.
**Contras:** el empaquetado no puede tocar ningun archivo de Common, asi que va inyectado desde fuera.

---

## Decision

**Optamos por la Opcion C.** Los archivos de Common se copian sin cambios. Lo propio del espejo es
solo: `Directory.Build.props` (metadatos NuGet), `version`, `LICENSE`, `CHANGELOG.md`, `.github/` y
`docs/adr-nuget/`. Para sincronizar:

```bash
git -C ../Common archive vX.Y.Z | tar -x -C .
```

mas borrar lo que Common haya borrado, y comprobar que cada blob coincide con el del tag.

---

## Consecuencias

**Positivas:**
- Ningun cambio de codigo nace aqui: todo se hace en Common y llega por la siguiente sincronizacion.

**Negativas / trade-offs a vigilar:**
- `Directory.Build.props` afecta a todos los proyectos. Si Common agrega el suyo algun dia, chocan y
  hay que mover el empaquetado a un `Directory.Build.targets` o fusionarlos.
- El `README.md` que se publica en nuget.org es el de Common, escrito para quien usa el submodulo.

> **Nota posterior (2026-09-24, v2.1.1).** Common agrego su propio `LICENSE` (MIT, el mismo texto),
> asi que `LICENSE` deja de ser un archivo propio del espejo y llega con la sincronizacion.
