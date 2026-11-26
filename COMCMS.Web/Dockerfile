# Multi-stage production Dockerfile for COMCMS (.NET 11)
FROM mcr.microsoft.com/dotnet/sdk:11.0 AS build
WORKDIR /src

COPY ["COMCMS.Web/COMCMS.Web.csproj", "COMCMS.Web/"]
COPY ["COMCMS.API/COMCMS.API.csproj", "COMCMS.API/"]
COPY ["COMCMS.Common/COMCMS.Common.csproj", "COMCMS.Common/"]
COPY ["COMCMS.Core/COMCMS.Core.csproj", "COMCMS.Core/"]
COPY ["Lib.Core/Lib.Core.csproj", "Lib.Core/"]
RUN dotnet restore "COMCMS.Web/COMCMS.Web.csproj"

COPY . .
WORKDIR "/src/COMCMS.Web"
RUN dotnet publish "COMCMS.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:11.0 AS final
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080 \
    DOTNET_EnableDiagnostics=0

EXPOSE 8080

COPY --from=build /app/publish .
RUN mkdir -p /app/App_Data/uploads /app/App_Data/media /app/Log && chown -R $APP_UID:$APP_UID /app

USER $APP_UID

HEALTHCHECK --interval=30s --timeout=5s --start-period=15s --retries=3 \
    CMD curl -fsS http://127.0.0.1:8080/health/live || exit 1

ENTRYPOINT ["dotnet", "COMCMS.Web.dll"]
