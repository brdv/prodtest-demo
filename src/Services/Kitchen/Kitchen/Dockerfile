FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine as build
WORKDIR /app
COPY ./ .
RUN echo "Building kitchenservice"
RUN ls -la
RUN dotnet restore ./Services/Kitchen/Kitchen/Kitchen.csproj
RUN dotnet publish ./Services/Kitchen/Kitchen/Kitchen.csproj -o /app/published-app

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine as runtime
WORKDIR /app
COPY --from=build /app/published-app /app
ARG VERSION
ENV PRODTEST_VERSION=${VERSION}
ENTRYPOINT [ "dotnet", "/app/Kitchen.dll" ]
