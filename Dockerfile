FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Drivers/API/API.csproj", "src/Drivers/API/"]
COPY ["src/Adapters/Adapters.csproj", "src/Adapters/"]
COPY ["src/Core/Application/Application.csproj", "src/Core/Application/"]
COPY ["src/Core/Entities/Entities.csproj", "src/Core/Entities/"]
COPY ["src/Drivers/External/External.csproj", "src/Drivers/External/"]
RUN dotnet restore "./src/Drivers/API/API.csproj"
COPY . .
WORKDIR "/src/src/Drivers/API"
RUN dotnet build "./API.csproj" -c $BUILD_CONFIGURATION -o /app/build /clp:ErrorsOnly


FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false /clp:ErrorsOnly

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.dll"]
