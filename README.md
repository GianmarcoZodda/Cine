# Reserva Espectaculo 📖

## Objetivos 📋
Desarrollar un sistema, que permita la administración general de un cine.
Utilizar Visual Studio 2022 preferentemente y crear una aplicación utilizando ASP.NET MVC Core (versión a definir por el docente ).

<hr />

## Enunciado 📢
La idea principal de este trabajo práctico, es que Uds. se comporten como un equipo de desarrollo.
Este documento, les acerca, un equivalente al resultado de una primera entrevista entre el cliente y alguien del equipo, el cual relevó e identificó la información aquí contenida. 
A partir de este momento, deberán comprender lo que se está requiriendo y construir dicha aplicación web.

Lo  primero debra ser comprender al detalle, que es lo que se espera y busca del proyecto, para ello, deben recopilar todas las dudas que tengan entre Uds. y evacuarlas con su nexo (el docente) de cara al cliente. De esta manera, él nos ayudará a conseguir la información ya un poco más procesada. 
Es importante destacar, que este proceso no debe esperar a hacerlo en clase; deben ir contemplandolas independientemente, las unifican y hace una puesta comun dentro del equipo, ya sean de índole funcional o técnicas, en lugar de que cada consulta enviarla de forma independiente, se recomienda que las envien de manera conjunta. 

Al inicio del proyecto, las consultas seran realizadas por correo y deben seguir el siguiente formato:

Subject: [NT1-<CURSO LETRA>-GRP-<GRUPO NUMERO>] <Proyecto XXX> | Informativo o Consulta

Body: 

1.<xxxxxxxx>
2.< xxxxxxxx>

# Ejemplo
**Subject:** [NT1-A-GRP-5] Agenda de Turnos | Consulta

**Body:**

1.La relación del paciente con Turno es 1:1 o 1:N?
2.Está bien que encaremos la validación del turno activo, con una propiedad booleana en el Turno?

<hr />

Es sumamente importante que los correos siempre tengan:
1.Subject con la referencia, para agilizar cualquier interaccion entre el docente y el grupo
2. Siempre que envien una duda o consulta, pongan en copia a todos los participantes del equipo. 

Nota: A medida que avancemos en la materia, las dudas seran canalizadas por medio de Github, y alli tendremos las dudas comentadas, accesibles por todos y el avance de las mismas. 

**Crear un Issue o escribir un nuevo comentario sobre el issue** que se requiere asistencia, siempre arrobando al docente, ejemplo: @marianolongoort


### Proceso de ejecución en alto nivel ☑️
 - Crear un nuevo proyecto en [visual studio](https://visualstudio.microsoft.com/en/vs/) utilizando la template de MVC.
 - Crear todos los modelos definidos y/o detectados por ustedes, dentro de la carpeta Models cada uno en un archivo separado (Modelos anemicos).
 - En el proyecto encararemos y permitiremos solo una herencia entre los modelos anemicos. Comforme avancemos, veremos que en este nivel, que estos modelos tengan una herencia, sera visto como una mala practica, pero es la mejor forma de visualizarlo. Esta unica herencia soportada sera PERSONA como clase base y luego diferentes especializaciones, segun sea el proyecto (Cliente, Alumno, Profesional, etc.).  
 - Sobre dichos modelos, definir y aplica las restricciones necesarias y solicitadas para cada una de las entidades. [DataAnnotations](https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations?view=netcore-3.1).
 - Agregar las propiedades navegacionales, sobre las relaciones entre las entidades (modelos).
 - Agregar las propiedades relacionales, en el modelo donde se quiere alojar la relacion (entidad dependiente).
 - Crear una carpeta Data que dentro tendrá al menos la clase que representará el contexto de la base de datos (DbContext) en nuestra aplicacion. 
 - Agregar los paquetes necesarios para Incorporar Entity Framework e Identitiy en nuestros proyectos.
 - Crear el DbContext utilizando en esta primera estapa con base de datos en memoria (con fines de testing inicial, introduccion y fine tunning de las relaciones entre modelos). [DbContext](https://docs.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbcontext?view=efcore-3.1), [Database In-Memory](https://docs.microsoft.com/en-us/ef/core/providers/in-memory/?tabs=vs).
 - Agregar los DbSet para cada una de las entidades que queremos persistir en el DbContext.
 - Agregar Identity a nuestro poryecto, para facilitar la inclusion de funcionalidades como Iniciar y cerrar sesion, agregado de entidades de soporte para esto Usuario y Roles que nos serviran para aplicar un control de acceso basado en roles (RBAC) basico. 
 - Por medio de Scaffolding, crear en esta instancia todos los CRUD de las entidades a persistir. Luego verificaremos que se mantiene, que se remueve, que se modifica y que debemos agregar.
 - Antes de continuar es importante realizar algun tipo de pre-carga de la base de datos. No solo es requisito del proyecto, sino que les ahorrara mucho tiempo en las pruebas y adecuaciones de los ABM.
 - Testear en detalle los ABM generado, y detectar todas las modificaciones requeridas para nuestros ABM e interfaces de usuario faltantes para resolver funcionalidades requeridas. (siempre tener presente el checklist de evaluacion final, que les dara el rumbo para esto).
 - Cambiar el dabatabase service provider de Database In Memory a SQL. Para aquellos casos que algunos alumnos utilicen MAC, tendran dos opciones para avanzar (adecuar el proyecto, para utilizar SQLLite o usar un docker con SQL Server instalado alli).
 - Aplicar las adecuaciones y validaciones necesarias en los controladores.  
 - Si el proyecto lo requiere, generar el proceso de auto-registración. Es importante aclarar que este proceso estara supeditado a las necesidades de cada proyecto y seguramente para una entidad especifica. 
 - A estas alturas, ya se han topado con varios inconvenientes en los procesos de adecuacion de las vistas y por consiguiente es una buena idea que generen ViewModels para desbloquear esas problematicas que nos estan trayendo los Modelos anemicos utilizados hasta el momento.
 - En el caso de ser requerido en el enunciado, un administrador podrá realizar todas tareas que impliquen interacción del lado del negocio (ABM "Alta-Baja-Modificación" de las entidades del sistema y configuraciones en caso de ser necesarias).
 - El <Usuario Cliente o equivalente> sólo podrá tomar acción en el sistema, en base al rol que que se le ha asignado al momento de auto-registrarse o creado por otro medio o entidad.
 - Realizar todos los ajustes necesarios en los modelos y/o funcionalidades.
 - Realizar los ajustes requeridos desde la perspectiva de permisos y validaciones.
 - Todo lo referido a la presentación de la aplicaión (cuestiones visuales).
 
 Nota: Para la pre-carga de datos, las cuentas creadas por este proceso, deben cumplir las siguientes reglas:
 1. La contraseña por defecto para todas las cuentas pre-cargadas será: Password1!
 2. El UserName y el Email deben seguir la siguiente regla:  <classname>+<rolname si corresponde diferenciar>+<indice>@ort.edu.ar Ej.: cliente1@ort.edu.ar, empleado1@ort.edu.ar, empleadorrhh1@ort.edu.ar

<hr />

## Entidades 📄

- Persona
- Cliente
- Empleado
- Reserva
- Función
- Pelicula
- Sala
- Genero

`Importante: Todas las entidades deben tener su identificador unico. Id`

`
Las propiedades descriptas a continuación, son las minimas que deben tener las entidades. Uds. pueden agregar las que consideren necesarias.
De la misma manera Uds. deben definir los tipos de datos asociados a cada una de ellas, como así también las restricciones.
`

**Persona**
```
- UserName
- Password
- Email
- FechaAlta
```

**Cliente**
```
- Nombre
- Apellido
- DNI
- Telefono
- Direccion
- FechaAlta
- Email 
- Reservas
```

**Empleado**
```
- Nombre
- Apellido
- DNI
- Telefono
- Direccion
- FechaAlta
- Email
- Legajo
```

**Pelicula**
```
- FechaLanzamiento
- Titulo
- Descripcion
- Genero
- Funciones
```
**Genero**
```
- Nombre
- Peliculas
```

**Sala**
```
- Numero
- TipoSala
- CapacidadButacas
- Funciones
```

**TipoSala**
```
- Nombre
- Precio
```

**Función**
```
- Fecha
- Hora
- Descripcion
- ButacasDisponibles
- Confirmada
- Pelicula
- Sala
- Reservas
```


**Reserva**
```
- Funcion
- FechaAlta
- Cliente
- CantidadButacas
```

**NOTA:** aquí un link para refrescar el uso de los [Data annotations](https://www.c-sharpcorner.com/UploadFile/af66b7/data-annotations-for-mvc/).

<hr />

## Caracteristicas y Funcionalidades ⌨️
`Todas las entidades, deben tener implementado su correspondiente ABM, a menos que sea implicito el no tener que soportar alguna de estas acciones.`

**General**
- Los clientes pueden auto registrarse.
- La autoregistración desde el sitio, es exclusiva para los clientes. Por lo cual, se le asignará dicho rol.
- Los empleados, deben ser agregados por otro Empleado.
	- Al momento, del alta del empleado, se le definirá un username y la password será definida por el sistema.
    - También se le asignará a estas cuentas el rol de empleado.

**Cliente**
- Un cliente puede realizar una reserva Online
    - El proceso será en modo Wizard.
        - Selecciona la pelicula
        - Selecciona la cantidad de butacas que quiere reservar.
        - Seleccionará una función disponible dentro de la oferta.
            - La oferta estará restringida desde el momento de la consulta hasta 7 dias posteriores.
            - Las funciones deben estar confirmadas
            - No debe incluir desde luego funciones que no tenga butacas disponibles.            
            - Debe ser en base a la oferta de la pelicula seleccionada.
            - El cliente, solo puede tener una reserva activa.
        - El cliente, podrá en todo momento, ver si tiene o no una reserva para una función futura.            
            - Podrá cancelarla, solo si es hasta 24hs. antes.
- Puede ver las reservas pasadas.
- Puede actualizar datos de contacto, como el telefono, dirección,etc.. Pero no puede modificar su DNI, Nombre, Apellido, etc.

**Empleado**
- El empleado puede listar las reservas por cada función "en el futuro" o "en el pasado".
- El empleado, puede habilitar o cancelar funciones. 
    - Solo pueden cancelarse, si no tiene reservas.
- También, puede ver un balance de recaudación por pelicula en mes calendario. 
- Puede dar de alta las Salas, Peliculas, etc. 
    - Nadie, puede eliminar las salas, pero si puede cambiar el tipo.


**Reserva**
- La reserva al crearse debe estar en estado activa.
- El cliente solo puede tener una reserva activa.
- La reserva, tiene que validar, que sea factible, en cuanto a la cantidad de butacas que selecciona al cliente para una función especifica.
    - Si puede realizar la reserva se debe actualizar las butacas disponibles (Capacidad de la sala vs Reservas realizadas previas y actual).


**Aplicación General**
- Información institucional.
- Se deben listar las peliculas en cartelera sin iniciar sesión.
- Poder reservar, automáticamente desde la una pelicula (con solicitud de inicio de sesión previo si fuese requerido)
- Por cada pelicula, se tiene que poder listar las funciones activas para la proxima semana. 
- La disponibilidad de las funciones, solo puede verse al tener una sesión iniciada como cliente.


`
Nota: Las butacas no son numeradas. El complejo, no tiene limites fisicos en la construcción de salas. Las funciones tienen una duración fija de 2hs. 
`
