# NUGET-0004 - Publicar con Trusted Publishing, sin llave guardada

**Fecha:** 2026-09-24
**Estado:** Aceptado
**Proyecto:** ApiCommon (paquetes `Raptor.Common.*`)

---

## Contexto

El workflow `Publish` empujaba con una llave de nuget.org guardada en el secreto `NUGET_API_KEY`.
La primera publicacion de la 2.1.0 fallo con 403: la llave no tenia permiso sobre los paquetes
nuevos de [NUGET-0002](NUGET-0002-un-paquete-por-sub-libreria.md), o habia caducado. nuget.org
desaconseja las llaves para publicar desde CI y ofrece Trusted Publishing para GitHub Actions.

---

## Opciones consideradas

### Opcion A - Llave de larga vida en un secreto

**Pros:** ya estaba montado; funciona igual desde la linea de comandos.
**Contras:** caduca y hay que rotarla a mano; su alcance (patron de paquetes) se desfasa cuando se
agrega un paquete; si se filtra, sirve hasta que caduca.

### Opcion B - Trusted Publishing (OIDC)

**Pros:** no hay llave guardada: `NuGet/login` cambia el token OIDC del workflow por una llave que
dura minutos. La politica en nuget.org fija repo y archivo de workflow, asi que solo `publish.yml`
de este repo puede publicar.
**Contras:** depende de una politica configurada a mano en nuget.org, y publicar desde la maquina
local ya no es el camino normal.

---

## Decision

**Optamos por la Opcion B.** `publish.yml` pide `id-token: write`, obtiene la llave temporal con
`NuGet/login@v1` (usuario de nuget.org en el secreto `NUGET_USER`) y empuja con ella. La politica
de nuget.org es: dueño `Raptor057`, repo `ApiCommon`, workflow `publish.yml`, sin environment.

---

## Consecuencias

**Positivas:**
- Ninguna llave que rotar ni que filtrar. El secreto `NUGET_API_KEY` ya no se usa y se puede borrar.

**Negativas / trade-offs a vigilar:**
- Renombrar `publish.yml` o mover el repo rompe la publicacion hasta que se actualice la politica.
