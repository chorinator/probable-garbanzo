FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Presentation/HRAcuity.Presentation.WebApi/HRAcuity.Presentation.WebApi.csproj", "src/Presentation/HRAcuity.Presentation.WebApi/"]
RUN dotnet restore "src/Presentation/HRAcuity.Presentation.WebApi/HRAcuity.Presentation.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Presentation/HRAcuity.Presentation.WebApi"
RUN dotnet build "HRAcuity.Presentation.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "HRAcuity.Presentation.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HRAcuity.Presentation.WebApi.dll"]
