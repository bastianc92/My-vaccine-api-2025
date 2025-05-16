# 🏥 MyVaccine Web Application

## 📝 Descripción

🌐 **MyVaccine** es una aplicación web desarrollada en **.NET Core**, diseñada para gestionar la administración de vacunas. Esta solución utiliza una base de datos **SQL Server** para almacenar toda la información relacionada. La aplicación y la base de datos se despliegan utilizando contenedores **Docker**, lo que facilita la implementación y el manejo de dependencias.

## ❗ Problema

El seguimiento de vacunas es crucial para mantener la salud pública. Sin embargo, administrar estos datos eficientemente puede ser desafiante sin el sistema adecuado.

## 💡 Solución

Utilizamos Docker para contenerizar tanto la aplicación web como la base de datos SQL Server. Esto permite un despliegue rápido, consistente y seguro en cualquier entorno que soporte Docker.

## 📦 Cómo Desplegar

### Prerrequisitos

- 🐳 **Docker**
- 🖥️ **Docker Desktop** (para usuarios de Windows y Mac)

### 🚀 Instalación y Despliegue

Para desplegar y ejecutar la aplicación, sigue estos pasos:

#### 1. Ejecuta el script `install-myvaccine.bat`

Este script configurado con comandos Docker realizará todo lo necesario para poner en marcha tu aplicación y la base de datos:

./install-myvaccine.bat

🧾 Explicación del script install-myvaccine.bat
Imagina que estás desmontando y reconstruyendo una pequeña fábrica cada vez que quieres cambiar su diseño. Esto es lo que hace nuestro script:

docker-compose down -v: Desmonta la fábrica actual (detiene y elimina todos los contenedores, redes y volúmenes), asegurando que no queden restos que puedan afectar la nueva construcción.
docker-compose build: Reconstruye la fábrica desde cero, siguiendo los planos detallados (construye las imágenes de Docker necesarias para la aplicación y la base de datos).
docker-compose up -d: Pone en marcha la fábrica en modo automático (levanta los contenedores en modo 'detached').
docker volume prune -f: Limpia todos los desechos o materiales no utilizados (elimina todos los volúmenes no utilizados).

### 📄 Docker Compose y Dockerfile

#### Docker Compose

Piensa en `docker-compose.yml` como un plano detallado para construir un complejo de edificios donde cada servicio (aplicación web y base de datos) es un edificio diferente. Aquí está cómo se organiza este complejo:

```markdown
version: '3.8'
services:
  webapi:
    build:
      context: .
      dockerfile: MyVaccine.WebApi/DockerFile
    ports:
      - "38791:38791"
    depends_on:
      - db
    environment:
      - ConnectionStrings__DefaultConnection=Server=db;Database=myVaccineDB;User=sa;Password=Your_password123;Encrypt=false;
    networks:
      - myvaccinenet

  db:
    image: mcr.microsoft.com/mssql/server:2019-latest
    environment:
      SA_PASSWORD: "Your_password123"
      ACCEPT_EULA: "Y"
    ports:
      - "38792:1433"
    networks:
      - myvaccinenet

networks:
  myvaccinenet:
    driver: bridge


```

#### Dockerfile

El Dockerfile es como una receta de cocina detallada para construir un pastel muy específico (tu aplicación web). Aquí está la receta:
```docker

# Use the official image as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 38791

# Use SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MyVaccine.WebApi/MyVaccine.WebApi.csproj", "MyVaccine.WebApi/"]
RUN dotnet restore "MyVaccine.WebApi/MyVaccine.WebApi.csproj"
COPY . .
WORKDIR "/src/MyVaccine.WebApi"
RUN dotnet build "MyVaccine.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyVaccine.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyVaccine.WebApi.dll"]

```

### 🔧 Configuración de la Aplicación

#### Migraciones Automáticas

En `Program.cs`, después de `var app = builder.Build();`, se agregó el siguiente bloque para aplicar migraciones de la base de datos al iniciar la aplicación:

```csharp
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MyVaccineAppDbContext>();
    try
    {
        dbContext.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating or initializing the database.");
    }
}

```

#### Cambios en Swagger
Se ha configurado Swagger para estar siempre activo, permitiendo el acceso a la documentación de la API independientemente del entorno de despliegue.

``` csharp
app.UseSwagger();
app.UseSwaggerUI();
```

🔌 Ajuste de Puertos
Es crucial mapear los puertos del contenedor a los del host para que la API sea accesible externamente:

``` yaml
  - "38791:38791"  # Mapea el puerto 38791 del host al puerto 38791 del contenedor
```

Este mapeo asegura que cualquier solicitud a localhost:38791 en tu máquina host sea correctamente dirigida al puerto 38791 del contenedor, permitiendo el acceso a la aplicación desde tu navegador.

### 🌐 Acceso a la Aplicación
#### Acceso a la Aplicación Utilizando la IP Local

Para acceder a la interfaz de Swagger y ver la documentación de la API después de desplegar la aplicación, necesitarás conocer la IP local de tu máquina. Esto es especialmente útil si estás accediendo a la aplicación desde otro dispositivo en la misma red.

#### Encontrar tu IP Local en Windows

1. Abre el Símbolo del Sistema (cmd).
2. Escribe `ipconfig` y presiona Enter.
3. Busca la línea que dice "IPv4 Address" y anota la dirección IP que aparece al lado. Esta es tu IP local.

#### Acceder a Swagger

http://<remplazar_por_tu_ip>:38791/swagger

Ex: http://192.168.1.21:38791/swagger

## Acceso a la Aplicación desde Fuera de la Red Local

Si estás trabajando en un emulador o un dispositivo físico, debes asegurarte de que tu dispositivo esté conectado a la misma red y apuntar tu código a la dirección IP local. Sin embargo, si deseas acceder a tu aplicación desde fuera de tu red local, tienes varias opciones:

### Opciones para Acceso Remoto

1. **Servidor en Internet o Nube**
   - Considera desplegar tu aplicación y base de datos en un servidor en Internet o en una plataforma de servicios en la nube. Esto te proporcionará una dirección IP accesible públicamente y gestionará la alta disponibilidad y la escalabilidad de tu aplicación.

2. **Herramientas de Túnel como Ngrok**
   - Puedes utilizar herramientas de túnel que permiten exponer un servidor local a Internet. Ejemplos incluyen:
     - [Ngrok](https://ngrok.com/): Muy popular y fácil de usar para pruebas de desarrollo.
     - [localtunnel](https://localtunnel.github.io/www/): Otra herramienta sencilla que ofrece funcionalidades similares.
     - [Serveo](https://serveo.net/): Permite exponer servidores locales sin necesidad de instalación adicional.

3. **IP Pública Estática**
   - Contacta a tu operador de internet (como Tigo, Claro, etc.) y solicita una IP pública estática. Esto asegura que tu dirección IP no cambie con el tiempo.
   - En tu máquina local, configura una IP estática para asegurar que la dirección no cambie.
   - Ajusta la configuración de DMZ en tu router para que cuando se acceda a tu IP pública en un puerto específico, este redireccione el tráfico a tu PC.
   - Asegúrate de que el puerto específico esté abierto y dirigido hacia el servicio Docker en tu máquina. Esto permitirá que el servicio sea accesible desde cualquier parte del mundo.

### Consideraciones Importantes

- Asegúrate de que haya una conexión a internet estable donde se encuentre la máquina que aloja tu aplicación.
- La máquina debe estar encendida para acceder al servicio.
- Si no se configura correctamente, el acceso puede ser solo temporal, especialmente si cambian las IPs o la configuración del router.

Al seguir estos pasos y consideraciones, podrás acceder a tu servicio desde cualquier parte del mundo de manera segura y confiable.

©️ Licencia
Este proyecto está licenciado bajo la Licencia MIT - vea el archivo LICENSE.md para más detalles.
