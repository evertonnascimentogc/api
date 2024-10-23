# Use a imagem base do .NET SDK
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Use a imagem base do .NET SDK para compilar a aplicação
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Eventify.csproj", "./"]
RUN dotnet restore "./SeuProjeto.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Eventify.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Eventify.csproj" -c Release -o /app/publish

# Use a imagem base do .NET Runtime para executar a aplicação
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Eventify.dll"]
