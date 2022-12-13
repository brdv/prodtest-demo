FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine as build
WORKDIR /app
COPY . .
RUN dotnet restore ./Services/Order/Order.API/Order.API.csproj
RUN dotnet publish ./Services/Order/Order.API/Order.API.csproj -o /app/published-app

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine as runtime
WORKDIR /app
ARG RMQ_HOST
ENV RMQ_HOST=${RMQ_HOST}
COPY --from=build /app/published-app /app
ENTRYPOINT [ "dotnet", "/app/Order.API.dll" ]
