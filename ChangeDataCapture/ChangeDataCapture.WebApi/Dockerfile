#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["ChangeDataCapture.WebApi/ChangeDataCapture.WebApi.csproj", "ChangeDataCapture.WebApi/"]
RUN dotnet restore ChangeDataCapture.WebApi/ChangeDataCapture.WebApi.csproj

COPY . .
WORKDIR "/src/ChangeDataCapture.WebApi"
RUN dotnet build "ChangeDataCapture.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
ARG APP_VERSION=
RUN dotnet publish "ChangeDataCapture.WebApi.csproj" -c Release -o /app/publish -p:version=${APP_VERSION}

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChangeDataCapture.WebApi.dll"]
