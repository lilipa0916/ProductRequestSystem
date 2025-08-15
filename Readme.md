# Sistema de Solicitudes de Producto y Ofertas de Proveedores

## 📋 Descripción

Sistema web que permite a **Clientes** crear solicitudes de productos y a **Proveedores** responder con ofertas. Los clientes pueden aceptar, rechazar o contraofertar las propuestas recibidas.

### 🎯 Características Principales

- **Autenticación JWT** con roles de Cliente y Proveedor
- **Gestión de Solicitudes** para clientes
- **Sistema de Ofertas** para proveedores  
- **Negociación** de ofertas (aceptar/rechazar/contraofertar)
- **Interfaz única** `/manage` adaptada según el rol del usuario
- **API REST** documentada con Swagger
- **Base de datos** SQL Server con Entity Framework Core

## 🏗️ Arquitectura

El proyecto implementa **Clean Architecture** con las siguientes capas:

```
ProductRequestSystem/
├── 📁 Arquitectura/                    # Documentación y diagramas
├── 📁 src/
│   ├── 📁 ProductRequestSystem.Domain/          # Entidades y reglas de negocio
│   ├── 📁 ProductRequestSystem.Application/     # Casos de uso y servicios
│   ├── 📁 ProductRequestSystem.Infrastructure/  # Acceso a datos y servicios externos
│   ├── 📁 ProductRequestSystem.WebApi/          # API REST
│   └── 📁 ProductRequestSystem.Client/          # Blazor WebAssembly
├── 📄 ProductRequestSystem.sln
└── 📄 README.md
```

### 🔧 Tecnologías Utilizadas

- **.NET 8** - Framework de desarrollo
- **Blazor WebAssembly** - Frontend SPA
- **ASP.NET Core Web API** - Backend REST API
- **Entity Framework Core** - ORM para acceso a datos
- **SQL Server** - Base de datos relacional
- **JWT Bearer** - Autenticación y autorización
- **AutoMapper** - Mapeo entre objetos
- **FluentValidation** - Validación de datos
- **Serilog** - Logging estructurado
- **Bootstrap 5** - Framework CSS

## 🚀 Instalación y Configuración

### Prerrequisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server LocalDB](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb) (incluido con Visual Studio)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) o [Visual Studio Code](https://code.visualstudio.com/)

### 📥 Pasos de Instalación

1. **Clonar el repositorio**
```bash
git clone <repository-url>
cd ProductRequestSystem
```

2. **Restaurar paquetes NuGet**
```bash
dotnet restore
```

3. **Configurar la base de datos**

La aplicación está configurada para usar **SQL Server LocalDB** por defecto. La base de datos se crea automáticamente al iniciar la aplicación.

**Cadena de conexión por defecto** (en `appsettings.json`):
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ProductRequestSystemDb;Trusted_Connection=true;MultipleActiveResultSets=true;TrustServerCertificate=true"
  }
}
```

4. **Ejecutar el Backend (API)**

```bash
cd src/ProductRequestSystem.WebApi
dotnet run
```

La API estará disponible en: `https://localhost:7000`
Documentación Swagger: `https://localhost:7000/swagger`

5. **Ejecutar el Frontend (Blazor)**

En una nueva terminal:
```bash
cd src/ProductRequestSystem.Client
dotnet run
```

La aplicación web estará disponible en: `https://localhost:7001`

### 🗄️ Datos de Prueba

La aplicación incluye usuarios de prueba que se crean automáticamente:

**Cliente:**
- Email: `client@test.com`
- Password: `Password123!`
- Rol: Cliente

**Proveedor:**
- Email: `provider@test.com`  
- Password: `Password123!`
- Rol: Proveedor

## 📖 Uso del Sistema

### 🔐 Autenticación

1. Navegar a `/login`
2. Iniciar sesión con credenciales de prueba o registrar nuevo usuario
3. Seleccionar rol (Cliente o Proveedor) al registrarse
4. Será redirigido automáticamente a `/manage`

### 👤 Funcionalidades por Rol

#### **Cliente**
- ✅ Crear solicitudes de producto
- ✅ Ver todas sus solicitudes
- ✅ Ver ofertas recibidas para cada solicitud
- ✅ Aceptar/Rechazar ofertas
- ✅ Ver estados de negociación

#### **Proveedor**
- ✅ Ver solicitudes abiertas de todos los clientes
- ✅ Crear ofertas para solicitudes disponibles
- ✅ Ver todas las ofertas realizadas
- ✅ Monitorear estado de ofertas

### 🔄 Flujo de Negocio

1. **Cliente** crea una solicitud de producto
2. **Proveedor** ve la solicitud en "Solicitudes Abiertas"
3. **Proveedor** hace una oferta (precio + días estimados)
4. **Cliente** recibe la oferta y puede:
   - ✅ **Aceptar**: Cierra la solicitud y rechaza otras ofertas
   - ❌ **Rechazar**: Mantiene la solicitud abierta para otras ofertas
   - 🔄 **Contraofertar**: (funcionalidad preparada para extensión)

## 🛠️ Desarrollo

### 🏃‍♂️ Ejecutar en Modo Desarrollo

**Terminal 1 - API:**
```bash
cd src/ProductRequestSystem.WebApi
dotnet watch run
```

**Terminal 2 - Client:**
```bash
cd src/ProductRequestSystem.Client  
dotnet watch run
```

### 🧪 Ejecutar Tests

```bash
# Tests unitarios (cuando se implementen)
dotnet test

# Verificar cobertura de código
dotnet test --collect:"XPlat Code Coverage"
```

### 📊 Base de Datos

#### Migraciones (si se usan)
```bash
cd src/ProductRequestSystem.Infrastructure

# Crear migración
dotnet ef migrations add InitialCreate --startup-project ../ProductRequestSystem.WebApi

# Aplicar migraciones
dotnet ef database update --startup-project ../ProductRequestSystem.WebApi
```

#### Resetear Base de Datos
```bash
cd src/ProductRequestSystem.WebApi
dotnet ef database drop --force
dotnet run  # Se recreará automáticamente
```

## 📡 API Endpoints

### 🔑 Autenticación
- `POST /api/auth/login` - Iniciar sesión
- `POST /api/auth/register` - Registrar usuario

### 📋 Solicitudes de Producto
- `GET /api/productrequests/my-requests` - Solicitudes del cliente (🔒 Cliente)
- `GET /api/productrequests/open` - Solicitudes abiertas (🔒 Proveedor)
- `POST /api/productrequests` - Crear solicitud (🔒 Cliente)
- `GET /api/productrequests/{id}` - Obtener solicitud por ID

### 💰 Ofertas
- `GET /api/offers/my-offers` - Ofertas del proveedor (🔒 Proveedor)
- `POST /api/offers` - Crear oferta (🔒 Proveedor)
- `PUT /api/offers/{id}/status` - Actualizar estado de oferta (🔒 Cliente)
- `GET /api/offers/{id}` - Obtener oferta por ID

## 🔧 Configuración

### Variables de Entorno / appsettings.json

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ProductRequestSystemDb;Trusted_Connection=true;MultipleActiveResultSets=true;TrustServerCertificate=true"
  },
  "Jwt": {
    "Key": "ThisIsAVeryLongSecretKeyForJwtTokenGenerationThatShouldBeAtLeast256Bits",
    "Issuer": "ProductRequestSystem",
    "Audience": "ProductRequestSystemClient"
  }
}
```

### 🌐 CORS

La API está configurada para permitir requests desde:
- `https://localhost:7001` (Blazor Client)
- `http://localhost:5001` (Blazor Client HTTP)

## 🗂️ Estructura de Proyecto

### Domain Layer
```
ProductRequestSystem.Domain/
├── Entities/           # Entidades del dominio
│   ├── User.cs
│   ├── ProductRequest.cs
│   └── Offer.cs
├── Enums/             # Enumeraciones
├── Interfaces/        # Contratos de repositorios
└── Common/           # Clases base
```

### Application Layer
```
ProductRequestSystem.Application/
├── DTOs/              # Data Transfer Objects
├── Interfaces/        # Contratos de servicios
├── Services/          # Lógica de negocio
├── Validators/        # Validaciones con FluentValidation
├── Mappings/          # Perfiles de AutoMapper
└── Extensions/        # Extensiones de DI
```

### Infrastructure Layer
```
ProductRequestSystem.Infrastructure/
├── Data/              # DbContext y configuraciones
├── Repositories/      # Implementación de repositorios
└── Extensions/        # Configuración de servicios
```

### WebApi Layer
```
ProductRequestSystem.WebApi/
├── Controllers/       # Controladores REST
├── Program.cs         # Configuración de la aplicación
└── appsettings.json   # Configuración
```

### Client Layer
```
ProductRequestSystem.Client/
├── Components/        # Componentes Blazor reutilizables
├── Pages/            # Páginas de la aplicación
├── Services/         # Servicios para consumir API
├── Models/           # DTOs del cliente
├── Shared/           # Layout y componentes compartidos
└── wwwroot/          # Archivos estáticos
```

## 📚 Documentación Adicional

- 📖 [Justificación de Decisiones Técnicas](Arquitectura/Justificacion.md)
- 🏢 [Gestión de Equipo y Metodologías Ágiles](Arquitectura/Gestion_Equipo.md)
- 🏗️ [Diagrama de Arquitectura](Arquitectura/Diagrama_Arquitectura.png)

## 🐛 Troubleshooting

### Problemas Comunes

#### ❌ Error de conexión a base de datos
```
Microsoft.Data.SqlClient.SqlException: A network-related or instance-specific error occurred
```
**Solución:** Verificar que SQL Server LocalDB esté instalado:
```bash
sqllocaldb info
sqllocaldb start MSSQLLocalDB
```

#### ❌ Error CORS en Blazor
```
Access to fetch at 'https://localhost:7000/api/...' from origin 'https://localhost:7001' has been blocked by CORS policy
```
**Solución:** Verificar que la API esté ejecutándose en el puerto correcto (7000) y que la configuración CORS incluya el puerto del cliente.

#### ❌ JWT Token inválido
```
401 Unauthorized
```
**Solución:** Verificar que el token no haya expirado (24h) y que la configuración JWT sea consistente entre API y Client.

### 🔍 Logs

Los logs se escriben en:
- **Consola**: Logs de desarrollo
- **Archivo**: `logs/api-{fecha}.txt`

### 📞 Soporte

Para reportar bugs o solicitar funcionalidades, crear un issue en el repositorio del proyecto.

## 🚀 Deployment

### Preparación para Producción

1. **Configurar variables de entorno**
```bash
export ASPNETCORE_ENVIRONMENT=Production
export ConnectionStrings__DefaultConnection="Server=prod-server;Database=ProductRequestSystemDb;..."
export Jwt__Key="your-production-secret-key"
```

2. **Publicar aplicación**
```bash
# API
cd src/ProductRequestSystem.WebApi
dotnet publish -c Release -o ./publish

# Client
cd src/ProductRequestSystem.Client
dotnet publish -c Release -o ./publish
```

3. **Configurar servidor web** (IIS, Nginx, Apache)

### 🐳 Docker (Opcional)

```dockerfile
# Dockerfile ejemplo para la API
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/ProductRequestSystem.WebApi/ProductRequestSystem.WebApi.csproj", "src/ProductRequestSystem.WebApi/"]
RUN dotnet restore "src/ProductRequestSystem.WebApi/ProductRequestSystem.WebApi.csproj"
COPY . .
WORKDIR "/src/src/ProductRequestSystem.WebApi"
RUN dotnet build "ProductRequestSystem.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProductRequestSystem.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductRequestSystem.WebApi.dll"]
```

## 📝 Licencia

Este proyecto es parte de una prueba técnica y está disponible bajo los términos que defina la organización.

## 👥 Contribución

1. Fork el proyecto
2. Crear feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push al branch (`git push origin feature/AmazingFeature`)
5. Abrir Pull Request

---

## ⭐ Funcionalidades Destacadas

- ✨ **Clean Architecture** con separación clara de responsabilidades
- 🔐 **Seguridad robusta** con JWT y autorización por roles
- 🎨 **UI moderna** con Bootstrap 5 y componentes responsivos
- 📱 **SPA** con navegación fluida sin recargas
- 🔄 **Estado en tiempo real** de solicitudes y ofertas
- 📊 **Validación dual** en frontend y backend
- 🛡️ **Manejo de errores** comprehensivo
- 📈 **Escalable** y preparado para crecimiento

**¡Sistema listo para producción!** 🚀