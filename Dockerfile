# Build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app
COPY . .
RUN dotnet publish -c Release KwakwaPl.sln -o out

# Image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .

# Startup
CMD ASPNETCORE_URLS=http://*:$PORT dotnet KwakwaPl.dll