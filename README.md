# CandyApi

API minimal en .NET 8 para gestión de usuarios y autenticación JWT.

## Contenido
- Program.cs: configuración de servicios, Swagger, JWT y DbContext.
- Data/ApplicationDBContext.cs: DbContext con `DbSet<CatUsuario>`.
- Entities/CatUsuario.cs: entidad para la tabla de usuarios.
- Repository/UserRepository.cs: acceso a datos (con consultas a otra base vía nombre totalmente calificado).
- Controllers/: controladores (UsersController, AuthController, ...).

## Requisitos
- .NET 8 SDK
- SQL Server accesible desde la aplicación (si usas la cadena de conexión por defecto)

## Configuración
1. No subir secretos al repositorio. El archivo `.gitignore` ya excluye `appsettings.Development.json`.
2. Valores recomendados:

- appsettings.json (en repo): mantener placeholders.
- appsettings.Development.json (local): colocar la `ConnectionStrings:ConexionSql` y `ApiSetting:SecretKey` solo en tu máquina.

Ejemplo usando variables de entorno (macOS zsh):

```bash
export ConnectionStrings__ConexionSql="Server=HOST;Database=DB;User Id=USER;Password=PASSWORD;TrustServerCertificate=true;"
export ApiSetting__SecretKey="UnaClaveMuyLarga"
```

Alternativa para desarrollo: usar `dotnet user-secrets`:

```bash
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:ConexionSql" "Server=...;Database=...;User Id=...;Password=...;"
dotnet user-secrets set "ApiSetting:SecretKey" "valor-secreto"
```

## Paquetes necesarios
- Microsoft.EntityFrameworkCore.SqlServer (si usas SQL Server)
- Microsoft.AspNetCore.Authentication.JwtBearer

Instalación:

```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
```

## Ejecutar
1. Restaurar y compilar:

```bash
dotnet restore
dotnet build
```

2. Ejecutar:

```bash
dotnet run
```

La aplicación expone Swagger UI en `/docs` (se redirige la raíz `/` a `/docs` en Development).

## Autenticación JWT
- Endpoint para obtener token: `POST /api/auth/login` (ajustar según implementación).
- En Swagger UI usar botón "Authorize" y pegar: `Bearer <token>`.
- En controladores proteger con `[Authorize]`.

## Base de datos
- Si no quieres usar migraciones, la app puede llamar `db.Database.EnsureCreated()` al iniciar para crear tablas según `DbSet`.
- Si necesitas acceder a una tabla en otra base, puedes usar consultas con nombre totalmente calificado: `OtherDb.dbo.cat_Usuarios` o usar ADO.NET vía `Database.GetDbConnection()`.

## Buenas prácticas
- No exponer secretos en logs o respuestas.
- Rotar credenciales si se filtran.
- Para producción usar un gestor de secretos (Azure Key Vault, AWS Secrets Manager, etc.).

## Contribuir
1. Crear rama feature/
2. Abrir PR con descripción y pruebas.

---
CandyApi — Proyecto generado localmente. Mantener las credenciales fuera del repo.
