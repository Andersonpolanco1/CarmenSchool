# CarmenSchool API

CarmenSchool es una API creada en .NET 8, diseñada para manejar la gestión de estudiantes, cursos, períodos e inscripciones. Este proyecto está compuesto por cuatro bibliotecas de clases:

- **CarmenSchool.Core**: Contiene interfaces de repositorios y servicios, DTOs, helpers y modelos.
- **CarmenSchool.Infrastructure**: Contiene el contexto, migraciones, configuraciones de modelos en el contexto e implementaciones de los repositorios.
- **CarmenSchool.Services**: Contiene implementaciones de los servicios.
- **CarmenSchool.Web**: Contiene la API con los controladores, archivo de configuración, etc.

## Estructura del Proyecto

La estructura del proyecto es la siguiente:

![image](https://github.com/user-attachments/assets/937720bf-afdc-44eb-a7bb-054f1ab5451a)


## Dependencias entre Capas

- **CarmenSchool.Core**: No depende de ninguna capa.
- **CarmenSchool.Infrastructure**: Depende de CarmenSchool.Core.
- **CarmenSchool.Services**: Depende de CarmenSchool.Infrastructure y CarmenSchool.Core.
- **CarmenSchool.Web**: Depende de CarmenSchool.Services.

## Endpoints

La API expone los siguientes endpoints CRUD para cada modelo:

### Estudiantes (Students)

- **GET** `/api/students/`  
  Obtiene todos los estudiantes.
  
- **GET** `/api/students/{int id}`  
  Obtiene un estudiante por ID.

- **POST** `/api/students/`  
  Crea un nuevo estudiante.

- **PUT** `/api/students/{int id}`  
  Actualiza un estudiante por ID enviando los campos a actualizar en el body.

- **DELETE** `/api/students/{int id}`  
  Elimina un estudiante por ID.

### Cursos (Courses)

- **GET** `/api/courses/`  
  Obtiene todos los cursos.
  
- **GET** `/api/courses/{int id}`  
  Obtiene un curso por ID.

- **POST** `/api/courses/`  
  Crea un nuevo curso.

- **PUT** `/api/courses/{int id}`  
  Actualiza un curso por ID enviando los campos a actualizar en el body.

- **DELETE** `/api/courses/{int id}`  
  Elimina un curso por ID.

### Períodos (Periods)

- **GET** `/api/periods/`  
  Obtiene todos los períodos.
  
- **GET** `/api/periods/{int id}`  
  Obtiene un período por ID.

- **POST** `/api/periods/`  
  Crea un nuevo período.

- **PUT** `/api/periods/{int id}`  
  Actualiza un período por ID enviando los campos a actualizar en el body.

- **DELETE** `/api/periods/{int id}`  
  Elimina un período por ID.

### Inscripciones (Enrollments)

- **GET** `/api/enrollments/`  
  Obtiene todas las inscripciones.
  
- **GET** `/api/enrollments/{int id}`  
  Obtiene una inscripción por ID.

- **POST** `/api/enrollments/`  
  Crea una nueva inscripción.

- **PUT** `/api/enrollments/{int id}`  
  Actualiza una inscripción por ID enviando los campos a actualizar en el body.

- **DELETE** `/api/enrollments/{int id}`  
  Elimina una inscripción por ID.

## Configuración

Para configurar la API, asegúrate de tener el archivo de configuración adecuado y de haber instalado todas las dependencias necesarias.

## Instalación

1. Clona el repositorio.
2. Abre la solución en Visual Studio.
3. Restaura los paquetes NuGet.
4. Configura la cadena de conexión en el archivo `appsettings.json`.
5. Crea la base de datos de nombre "CarmenSchool" o el nombre que se ha colocado en el connection string del `appsettings.json`.

6. Sitúate en la carpeta raíz del proyecto `CarmenSchool` con la consola y corre los siguientes comandos:

   Para agregar la migración (si no está creada, confirmar verificando la carpeta Migrations en el proyecto `CarmenSchool.Infrastructure`):

   ```bash
   dotnet ef migrations add InitialCreate --project CarmenSchool.Infrastructure --startup-project CarmenSchool.Web

7. Para crear las tablas en la base de datos, corre el siguiente comando en la misma carpeta raíz:


   ```bash
   dotnet ef database update --project CarmenSchool.Infrastructure --startup-project CarmenSchool.Web

## Mock 
La API expone endpoints para el Mock de datos. En el proyecto  `CarmenSchool.Infrastructure` se encuentra la carpeta `Mock Data` en la cual se encuentran los siguientes archivos JSON :

	 - Courses.json
	 - Periods.json
	 - Students.json
	 - Enrollments.json

En estos archivos estan los datos que son creados al momento de llamar a los endpoints Mock:

- **GET** `/api/mock/students`  
  Crear estudiantes.
  
- **GET** `/api/mock/courses`  
  Crear cursos.

- **GET** `/api/mock/periods`  
  Crear periodos.

- **GET** `/api/mock/enrollments`  
  Crear inscripciones de estudiantes a los cursos en los periodos registrados.

Estos endpoints deben ser llamados en este mismo orden para crear los registros correctamente.

## Contribuciones

Las contribuciones son bienvenidas. Si deseas contribuir, por favor abre un *issue* o envía un *pull request*.

## Licencia

Este proyecto está bajo la Licencia MIT. Para más detalles, consulta el archivo [LICENSE](LICENSE).
