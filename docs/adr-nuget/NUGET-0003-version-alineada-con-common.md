# NUGET-0003 - La version del paquete es la del tag de Common

**Fecha:** 2026-09-24
**Estado:** Aceptado
**Proyecto:** ApiCommon (paquetes `Raptor.Common.*`)

---

## Contexto

El espejo empezo en 2.0.0, pero Common ya tenia un `v2.0.0` en otro commit (`c5b419d`, la division
en sub-librerias), anterior al codigo que se estaba copiando (`aedf830`). Con dos series
independientes, "la 2.0.0" significaba dos cosas distintas segun a quien se le preguntara.

---

## Opciones consideradas

### Opcion A - Numeracion propia, con el commit de origen en el CHANGELOG

**Pros:** el espejo publica cuando quiere, sin esperar un tag en Common.
**Contras:** para saber que codigo trae un paquete hay que ir al CHANGELOG.

### Opcion B - La version del paquete es la del tag de Common

**Pros:** `Raptor.Common 2.1.0` y el submodulo en `v2.1.0` son el mismo codigo, sin tabla de
equivalencias.
**Contras:** no se puede publicar un arreglo solo del empaquetado sin un tag nuevo en Common.

---

## Decision

**Optamos por la Opcion B.** Solo se sincroniza desde un tag de Common, el archivo `version` lleva
ese mismo numero y el tag del espejo tambien (`vX.Y.Z`). El workflow de publicacion falla si el tag
no coincide con `version`. La serie empieza en **2.1.0**: la 2.0.0 del espejo nunca se publico.

---

## Consecuencias

**Positivas:**
- Un bug reportado contra un paquete se busca directo en el tag de Common con ese numero.

**Negativas / trade-offs a vigilar:**
- Un cambio que sea solo del empaquetado (metadatos, workflow) espera al siguiente tag de Common, o
  se publica como parche de Common aunque su codigo no cambie.
