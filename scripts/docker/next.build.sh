cd ./src
docker build -t order-api:vnext -f ./Services/Order/Order.API/Order.API.Dockerfile . --build-arg DOTNET_ENVIRONMENT="PRODUCTION" --build-arg RMQ_HOST="dl-rabbitmq"
docker build -t kitchen-service:vnext -f ./Services/Kitchen/Kitchen/Dockerfile . --build-arg DOTNET_ENVIRONMENT="PRODUCTION" --build-arg RMQ_HOST="dl-rabbitmq" --build-arg DL_TAG_VERSION="Vnext"