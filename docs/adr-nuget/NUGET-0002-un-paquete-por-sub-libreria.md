# NUGET-0002 - Un paquete NuGet por sub-libreria, mas la facade

**Fecha:** 2026-09-24 (registrado a posteriori el mismo dia)
**Estado:** Aceptado
**Proyecto:** ApiCommon (paquetes `Raptor.Common.*`)
**Origen:** commit `114c51a`.

---

## Contexto

Common son seis proyectos: cinco sub-librerias y la facade `Common`, que no tiene codigo y solo las
referencia (ADR-0001 de Common). En un paquete NuGet, un `ProjectReference` se convierte en
dependencia de otro paquete, asi que empaquetar solo la facade daria un paquete que depende de
paquetes que no existen.

---

## Opciones consideradas

### Opcion A - Un solo paquete `Raptor.Common` con los seis ensamblados adentro

**Pros:** un solo `PackageId`, como hasta la 0.0.x.
**Contras:** hay que meter las DLL a mano y declarar a mano las dependencias de las cinco
sub-librerias en la facade, una lista duplicada que se desfasa en cada sincronizacion. Y se pierde
la referencia por capa, que es la razon de haber dividido Common.

### Opcion B - Un paquete por proyecto

**Pros:** `dotnet pack` lo resuelve solo, sin listas a mano; `Domain` puede referenciar solo
`Raptor.Common.Contracts`, igual que con el submodulo.
**Contras:** seis paquetes que publicar, y la clave de nuget.org tiene que poder crear los nuevos.

---

## Decision

**Optamos por la Opcion B.** `Directory.Build.props` fija `PackageId` a `Raptor.$(MSBuildProjectName)`:
`Raptor.Common` (facade), `Raptor.Common.Contracts`, `.Messaging`, `.MultiTenancy`, `.Infra` y
`.Web`, todos con la misma version. `Common.Tests` no se empaqueta. Cada paquete lleva el README y
sus simbolos (`.snupkg`).

---

## Consecuencias

**Positivas:**
- Quien ya usaba `Raptor.Common` sigue usando ese nombre y recibe todo.

**Negativas / trade-offs a vigilar:**
- `NUGET_API_KEY` tiene que permitir crear paquetes nuevos (patron `Raptor.Common*`). Si no, la
  primera publicacion falla en los cinco nuevos.
- Un `PackageId` publicado en nuget.org no se puede borrar, solo ocultar: los nombres quedan fijos.
