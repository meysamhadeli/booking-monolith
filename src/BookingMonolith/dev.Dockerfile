FROM mcr.microsoft.com/dotnet/sdk:9.0 AS builder
WORKDIR /

COPY ./.editorconfig ./
COPY ./global.json ./
COPY ./Directory.Build.props ./

# Setup working directory for the project
COPY ./src/BuildingBlocks/BuildingBlocks.csproj ./src/BuildingBlocks/
COPY ./src/BookingMonolith/src/BookingMonolith.csproj ./src/BookingMonolith/src/
COPY ./src/Api/src/Api.csproj ./src/Api/src/

# Restore nuget packages
RUN --mount=type=cache,id=booking_nuget,target=/root/.nuget/packages \
    dotnet restore ./src/Api/src/Api.csproj

# Copy project files
COPY ./src/BuildingBlocks ./src/BuildingBlocks/
COPY ./src/BookingMonolith/src/ ./src/BookingMonolith/src/
COPY ./src/Api/src/ ./src/Api/src/

# Build project with Release configuration
# and no restore, as we did it already
RUN ls
RUN --mount=type=cache,id=booking_nuget,target=/root/.nuget/packages\
    dotnet build  -c Release --no-restore ./src/Api/src/Api.csproj

WORKDIR /src/Api/src

# Publish project to output folder
# and no build, as we did it already
RUN --mount=type=cache,id=booking_nuget,target=/root/.nuget/packages\
    dotnet publish -c Release --no-build -o out

FROM mcr.microsoft.com/dotnet/aspnet:9.0

# Setup working directory for the project
WORKDIR /
COPY --from=builder /src/Api/src/out  .

ENV ASPNETCORE_URLS="https://*:443, http://*:80"
ENV ASPNETCORE_ENVIRONMENT=docker

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "Api.dll"]

