# Prueba Técnica API

## Requisitos Previos

Asegúrate de tener instalados los siguientes programas en tu máquina:

- [Docker](https://www.docker.com/get-started) (incluye Docker Compose)
- [.NET SDK](https://dotnet.microsoft.com/download) (8.0.300)

## Ejecución de la API

Sigue los pasos a continuación para ejecutar la API:

1. **Ejecutar los comandos en Docker:**
   Abre una terminal y navega hasta el directorio donde se encuentra tu archivo `docker-compose.yml`. Ejecuta el siguiente comando:

   ```
   docker compose -f docker-compose.yml -f docker-compose.override.yml up -d
   ```
2. **Navegar al directorio de la API:**
   Una vez que los contenedores estén en ejecución, dirígete al directorio de la API:

   ```
   cd PruebaTecnica.Api
   ```
3. **Ejecutar la aplicación:**
   En la misma terminal, ejecuta el siguiente comando para iniciar la API:

   ```
   dotnet run
   ```
4. **Probar la API:**
   Abre un navegador web y accede a la siguiente URL:

   ```
   http://localhost:5157/swagger/index.html
   ```
