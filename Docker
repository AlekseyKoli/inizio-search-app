FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY inizio.sln ./
COPY inizio/inizio.csproj inizio/
COPY inizio.Tests/inizio.Tests.csproj inizio.Tests/

RUN dotnet restore

COPY . .

RUN dotnet publish inizio/inizio.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080