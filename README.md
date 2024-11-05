# Common

![Imagen](./Common.Documentation/Sources/networking.png)

[TODO](/Common.Documentation/TODO.md)

[CHANGELOG](/Common.Documentation/CHANGELOG.md)

# Introducción

## Tabla de contenidos:


## Instalación
- Instrucciones paso a paso para clonar y configurar el proyecto localmente.

## Requisitos Previos
- Lista de herramientas y versiones necesarias (como .NET, SQL Server, etc.).

## Configuración
- Detalles sobre archivos de configuración importantes, como appsettings.json o variables de entorno necesarias.


## Estructura del Proyecto
- Explicación de la arquitectura del proyecto (carpetas principales y su propósito).
- 
### Distribucion del proyecto
| Proyecto |
|--|
||

### Arquitectura 

Este proyecto fue creado bajo el tipo de "Clean Architecture", que se centra en la separación de responsabilidades y la independencia entre capas. Esta arquitectura consiste en dividir el código en varias capas bien definidas, donde cada una tiene una responsabilidad específica y está desacoplada de las demás. Las principales características de Clean Architecture son:

Capas organizadas en círculos concéntricos:

Entidad/Core: Representa las reglas de negocio y los modelos más fundamentales del dominio. Esta capa es la más interna y no depende de otras capas.
Caso de uso/Aplicación: Contiene la lógica de aplicación específica, como los casos de uso y los servicios que coordinan las interacciones entre las entidades y otras capas.
Infraestructura: Gestiona la interacción con el mundo externo, como las bases de datos, servicios externos, o la interacción con frameworks específicos (como Entity Framework en el caso de .NET). Esta capa contiene implementaciones detalladas de interfaces definidas en el núcleo de la aplicación.
UI/Web API: Capa más externa, encargada de manejar las interacciones con el usuario o cliente, como las solicitudes HTTP en el caso de una API.
Independencia: Las capas internas no dependen de las externas, lo que significa que las reglas de negocio no conocen nada sobre bases de datos, interfaces de usuario o frameworks específicos.

Interfaces desacopladas: Los detalles de implementación, como los repositorios de datos, están separados a través de interfaces que permiten intercambiar tecnologías (por ejemplo, cambiar la base de datos de SQL Server a MySQL) sin modificar la lógica de negocio.

Inversión de dependencias: Utiliza principios de inversión de control (IoC) para lograr que las dependencias fluyan hacia adentro, de las capas externas hacia las internas. Esto facilita pruebas unitarias y permite aislar el código en diferentes capas.

Esta estructura asegura que el código sea mantenible, escalable, y fácilmente testeable, además de permitir cambios en las tecnologías subyacentes sin afectar las reglas de negocio.

### Libros de ayuda
![enter image description here](./Documentacion/Img/libro-educativo.png)
|Nombre del libro y libro | Breve Descripcion del libro|
|--|--|
|[Clean Architecture A Craftsman's Guide to Software Structure and Design](/Documentacion/ExtraFiles/Books/Clean%20Architecture%20A%20Craftsman's%20Guide%20to%20Software%20Structure%20and%20Design.pdf) |Este libro se enfoca en los principios de código limpio aplicados específicamente al lenguaje C#. Proporciona una guía para escribir código más legible, mantenible y eficiente en este entorno. Además, aborda temas como el diseño orientado a objetos, patrones de diseño, y mejores prácticas de refactorización en C#, adaptando los principios del código limpio a las particularidades de este lenguaje.|
|[Clean Code A Handbook of Agile Software Craftsmanship](/Documentacion/ExtraFiles/Books/Clean%20Code%20A%20Handbook%20of%20Agile%20Software%20Craftsmanship.pdf)|Este libro es un clásico en el mundo del desarrollo de software. Robert C. Martin (Uncle Bob) explica cómo escribir código que sea limpio, fácil de entender y mantener, aplicando principios y técnicas de desarrollo ágil. El libro está lleno de ejemplos de código, consejos sobre refactorización, y cómo evitar los errores comunes que llevan a sistemas difíciles de gestionar. Es esencial para cualquier desarrollador que busque mejorar su habilidad para escribir software de alta calidad.|
|[Clean Code in C#](/Documentacion/ExtraFiles/Books/Clean%20Code%20in%20C#.pdf)|Este libro se enfoca en los principios de código limpio aplicados específicamente al lenguaje C#. Proporciona una guía para escribir código más legible, mantenible y eficiente en este entorno. Además, aborda temas como el diseño orientado a objetos, patrones de diseño, y mejores prácticas de refactorización en C#, adaptando los principios del código limpio a las particularidades de este lenguaje.|

### Proyectos



### Tecnologias usadas

|Tecnologia  | Nombre |
|--|--|
|[![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://github.com/dotnet/core/blob/main/release-notes/8.0/8.0.8/8.0.401.md)|NET 8.0.401 - August 15, 2024|
|[![NUGET](https://img.shields.io/badge/NuGet-004880?style=for-the-badge&logo=nuget&logoColor=white)]()|NUGET|
|[![VisualStudio](https://img.shields.io/badge/Visual_Studio-5C2D91?style=for-the-badge&logo=visual%20studio&logoColor=white)]()|Microsoft Visual Studio Professional 2022 (64-bit) - Current Version 17.11.2|



## API Endpoints

- Documentación de los principales endpoints de la API (métodos, rutas, y ejemplos de respuestas).

## Base de Datos
- Descripción de la base de datos utilizada, tablas principales y su relación.

## Tests
- Instrucciones sobre cómo ejecutar las pruebas y verificar que todo funcione correctamente.

## Despliegue
- Guía para realizar el despliegue en entornos de producción.

## Contribuciones
- Guía para contribuir al proyecto, incluyendo buenas prácticas y reglas de estilo de código.


### Tips de documentacion

[𝗗𝗶𝗱 𝘆𝗼𝘂 𝗸𝗻𝗼𝘄 𝘁𝗵𝗮𝘁 𝘆𝗼𝘂 𝗰𝗮𝗻 𝗴𝗲𝗻𝗲𝗿𝗮𝘁𝗲 𝗽𝗿𝗼𝗳𝗲𝘀𝘀𝗶𝗼𝗻𝗮𝗹-𝗹𝗼𝗼𝗸𝗶𝗻𝗴 𝗱𝗼𝗰𝘂𝗺𝗲𝗻𝘁𝗮𝘁𝗶𝗼𝗻 𝗱𝗶𝗿𝗲𝗰𝘁𝗹𝘆 𝗳𝗿𝗼𝗺 𝘆𝗼𝘂𝗿 𝗖# 𝗰𝗼𝗱𝗲? 😃](https://www.linkedin.com/posts/ahmedyezdane_csharp-coding-programming-activity-7232363427917086721-Sivz?utm_source=share&utm_medium=member_desktop)

    /// <summary>
    /// Describe el propósito del método o la clase.
    /// Asi se pone un enlace en la documentacion: <a href="http://stackoverflow.com">here</a>
    /// </summary>
    /// <remarks>se utiliza para proporcionar información adicional, por ejemplo, si hay algún problema conocido que desee que otros desarrolladores conozcan.</remarks>
    /// <param name="paramname">Describe un parámetro de método.</param>
    /// <returns>Describe el valor devuelto de un método.</returns>
    /// <exception cref="InvalidProgramException">Describe una excepción que se puede producir.</exception>

## Informacion del desarollador

|Enlaces|
|--|
| [![Linkedin](https://img.shields.io/badge/LinkedIn-0077B5?style=for-the-badge&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/rogelio-arri/)|
