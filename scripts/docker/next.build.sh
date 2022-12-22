cd ./src
docker build -t order-api:vnext -f ./Services/Order/Order.API/Order.API.Dockerfile .
docker build -t kitchen-service:vnext -f ./Services/Kitchen/Kitchen/Dockerfile . --build-arg DOTNET_ENVIRONMENT="next"