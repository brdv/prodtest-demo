cd ./src
docker build -t order-api:latest -f ./Services/Order/Order.API/Order.API.Dockerfile .
docker build -t kitchen-service:latest -f ./Services/Kitchen/Kitchen/Dockerfile . --build-arg DOTNET_ENVIRONMENT="PRODUCTION" --build-arg RMQ_HOST="dl-rabbitmq" --build-arg DL_TAG_VERSION="Vcurrent"