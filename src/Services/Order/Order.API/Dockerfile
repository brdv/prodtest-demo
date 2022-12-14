FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine as build
WORKDIR /app
COPY . .
RUN dotnet restore
RUN dotnet publish -o /app/published-app

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine as runtime
WORKDIR /app
ARG DL_TAG_VERSION
ARG DOTNET_ENVIRONMENT
ARG RMQ_HOST
ENV DL_TAG_VERSION=${DL_TAG_VERSION}
ENV DOTNET_ENVIRONMENT=${DOTNET_ENVIRONMENT}
ENV RMQ_HOST=${RMQ_HOST}
COPY --from=build /app/published-app /app
ENTRYPOINT [ "dotnet", "/app/Order.API.dll" ]
