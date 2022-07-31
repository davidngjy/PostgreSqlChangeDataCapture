#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ChangeDataCapture.Data.Migrate/ChangeDataCapture.Data.Migrate.csproj", "ChangeDataCapture.Data.Migrate/"]
COPY ["ChangeDataCapture.Data.Migrations/ChangeDataCapture.Data.Migrations.csproj", "ChangeDataCapture.Data.Migrations/"]
RUN dotnet restore "ChangeDataCapture.Data.Migrate/ChangeDataCapture.Data.Migrate.csproj"
COPY "ChangeDataCapture.Data.Migrate/" "ChangeDataCapture.Data.Migrate/"
COPY "ChangeDataCapture.Data.Migrations/" "ChangeDataCapture.Data.Migrations/"
WORKDIR "/src/ChangeDataCapture.Data.Migrate"
RUN dotnet build "ChangeDataCapture.Data.Migrate.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ChangeDataCapture.Data.Migrate.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChangeDataCapture.Data.Migrate.dll"]
