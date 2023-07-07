FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PterodactylMigrator/PterodactylMigrator.csproj", "PterodactylMigrator/"]
RUN dotnet restore "PterodactylMigrator/PterodactylMigrator.csproj"
COPY . .
WORKDIR "/src/PterodactylMigrator"
RUN dotnet build "PterodactylMigrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PterodactylMigrator.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PterodactylMigrator.dll"]
