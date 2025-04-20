### Backend - Sistema de Gestión de Inventario (CCL)

Este backend fue desarrollado en **.NET Core 9** con **Entity Framework Core** y **PostgreSQL** para la gestión básica de inventario (entradas y salidas de productos), a su vez para poder visualizar estas entradas y salidas, agregar, eliminar o editar productos.

---

### Requisitos
- [.NET SDK 9.0](https://dotnet.microsoft.com/en-us/download)
- [PostgreSQL](https://www.postgresql.org/download/)
- Algún cliente para probar la API (Postman o navegador)

---

### Configuración Inicial

> [!IMPORTANT]
> Verificar el puerto en el que se esta levantando el servidor, normalmente es en 5000.

1. **Tener clonado el repositorio**
   
   ```bash
   git clone https://github.com/jzamora03/dev-inventariocll-back.git
   ```
   - Si se descarga .zip, al descomprimir borrar el main, y que quede solo **dev-inventariocll-back**
2. **Agregar base de datos y tablas necesarias**
   - Correr el script `inventariodb_schema.sql` el cual contiene las tablas necesarias para el correcto funcionamiento del proyecto.

3. **Verificar que las tablas se hayan creado correctamente, las cuales son:**
   - Productos y movimientos_inventario
     
4. **Configurar la conexión, luego haber creado las tablas necesarias:**
    -  Editar el archivo **appsettings.json** el cual se encuentra en la carpeta raíz.
      ```json
      "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Port=5432;Database=inventariodb;Username=postgres;Password=tu_clave"
     }
      ```
5. **Restaurar dependencias y compilar:**
   - `dotnet restore`
   - `dotnet build`
  
6. **Ejecutar el proyecto en Visual Studio Code:**
   - `dotnet run`
