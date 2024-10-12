# Usar la imagen base de .NET
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

# Usar la imagen de SDK de .NET para construir
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copiar y restaurar dependencias
COPY ["MicroserviceTwo/MicroserviceTwo.csproj", "MicroserviceTwo/"]
RUN dotnet restore "MicroserviceTwo/MicroserviceTwo.csproj"

# Copiar el resto del código y compilar
COPY . .
WORKDIR "/src/MicroserviceTwo"
RUN dotnet build "MicroserviceTwo.csproj" -c Release -o /app/build

# Publicar el proyecto
FROM build AS publish
RUN dotnet publish "MicroserviceTwo.csproj" -c Release -o /app/publish

# Imagen final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MicroserviceTwo.dll"]
