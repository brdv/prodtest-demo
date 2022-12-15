FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine as build
WORKDIR /app
COPY ./ .
RUN ls -la
RUN dotnet restore ./Services/Kitchen/Kitchen/Kitchen.csproj
RUN dotnet publish ./Services/Kitchen/Kitchen/Kitchen.csproj -o /app/published-app

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine as runtime
WORKDIR /app
COPY --from=build /app/published-app /app
ARG DL_TAG_VERSION
RUN echo ${DL_TAG_VERSION}
ARG DOTNET_ENVIRONMENT
ARG RMQ_HOST
ENV DL_TAG_VERSION=${DL_TAG_VERSION}
ENV DOTNET_ENVIRONMENT=${DOTNET_ENVIRONMENT}
ENV RMQ_HOST=${RMQ_HOST}
ENTRYPOINT [ "dotnet", "/app/Kitchen.dll" ]
