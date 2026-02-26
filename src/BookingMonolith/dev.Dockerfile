# ---------- Build Stage ----------
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy solution-level files
COPY .editorconfig .
COPY global.json .
COPY Directory.Build.props .

# Copy project files first (for better Docker layer caching)
COPY src/BuildingBlocks/BuildingBlocks.csproj src/BuildingBlocks/
COPY src/BookingMonolith/src/BookingMonolith.csproj src/BookingMonolith/src/
COPY src/Api/src/Api.csproj src/Api/src/
COPY src/Aspire/src/ServiceDefaults/ServiceDefaults.csproj src/Aspire/src/ServiceDefaults/

# Restore dependencies
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet restore src/Api/src/Api.csproj

# Copy the rest of the source code
COPY src ./src

# Publish (build included, no need separate build step)
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish src/Api/src/Api.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

# ---------- Runtime Stage ----------
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=docker

EXPOSE 80

ENTRYPOINT ["dotnet", "Api.dll"]