FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .
RUN dotnet build src/BaGet/BaGet.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish src/BaGet/BaGet.csproj -c Release -o /app

FROM base AS final
LABEL org.opencontainers.image.source="https://github.com/moyilong/BaGet"
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BaGet.dll"]
