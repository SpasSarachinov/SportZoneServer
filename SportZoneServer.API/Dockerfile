# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy solution and restore as distinct layers
COPY *.sln .
COPY SportZoneServer.API/*.csproj ./SportZoneServer.API/
COPY SportZoneServer.Data/*.csproj ./SportZoneServer.Data/
COPY SportZoneServer.Domain/*.csproj ./SportZoneServer.Domain/
COPY SportZoneServer.Core/*.csproj ./SportZoneServer.Core/
COPY SportZoneServer.Common/*.csproj ./SportZoneServer.Common/

RUN dotnet restore

# Copy everything else and build
COPY . .
RUN dotnet publish SportZoneServer.API/SportZoneServer.API.csproj -c Release -o out

# Stage 2: Run
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "SportZoneServer.API.dll"]