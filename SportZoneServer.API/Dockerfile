FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY *.sln .
COPY SportZoneServer.API/*.csproj ./SportZoneServer.API/
COPY SportZoneServer.Data/*.csproj ./SportZoneServer.Data/
COPY SportZoneServer.Domain/*.csproj ./SportZoneServer.Domain/
COPY SportZoneServer.Core/*.csproj ./SportZoneServer.Core/
COPY SportZoneServer.Common/*.csproj ./SportZoneServer.Common/

RUN dotnet restore

COPY . .
RUN dotnet publish SportZoneServer.API/SportZoneServer.API.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

EXPOSE 10000

ENTRYPOINT ["dotnet", "SportZoneServer.API.dll"]