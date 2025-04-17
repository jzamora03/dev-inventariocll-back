# dev-inventariocll-back

# Backend - Sistema de Gestión de Inventario (CCL)

Este backend fue desarrollado en **.NET Core 9** con **Entity Framework Core** y **PostgreSQL** para la gestión básica de inventario (entradas y salidas de productos), a su vez para poder visualizar estas entradas y salidas, agregar, eliminar o editar productos.

---

##  Requisitos Previos

- [.NET SDK 9.0](https://dotnet.microsoft.com/en-us/download)
- [PostgreSQL](https://www.postgresql.org/download/)
- Algún cliente para probar la API (Postman o navegador)

---

## Configuración Inicial

1. **Tener clonado el repositorio**
2. **Configurar la cadena de conexión**
 -  Editar el archivo **appsettings.json**
   
   "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=inventariodb;Username=postgres;Password=tu_clave"
  }
  
3. **Asegúrate de que:**
- La base de datos inventariodb existe (En este caso se uso DBeaver pero se puede usar cualquiera).
- El usuario/clave coinciden con tu configuración local.
  
6. **Verificar que las tablas se hayan creado correctamente, las cuales son:**
  - Productos
  - movimientos_inventario
    
5. **Restaurar dependencias y compilar:**
- dotnet restore
- dotnet build
  
6. ** Ejecutar el proyecto:**
- dotnet run
