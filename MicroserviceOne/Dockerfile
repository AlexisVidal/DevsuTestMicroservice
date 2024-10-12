# Usar la imagen base de .NET
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

# Usar la imagen de SDK de .NET para construir
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copiar y restaurar dependencias
COPY ["MicroserviceOne/MicroserviceOne.csproj", "MicroserviceOne/"]
RUN dotnet restore "MicroserviceOne/MicroserviceOne.csproj"

# Copiar el resto del código y compilar
COPY . .
WORKDIR "/src/MicroserviceOne"
RUN dotnet build "MicroserviceOne.csproj" -c Release -o /app/build

# Publicar el proyecto
FROM build AS publish
RUN dotnet publish "MicroserviceOne.csproj" -c Release -o /app/publish

# Imagen final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MicroserviceOne.dll"]
