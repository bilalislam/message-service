FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine as build

COPY . .

WORKDIR /src/MessageService.Api
RUN dotnet restore
RUN dotnet publish -o /app/published-app

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine as runtime
WORKDIR /app
COPY --from=build /app/published-app /app
ENTRYPOINT [ "dotnet", "/app/MessageService.Api.dll" ]