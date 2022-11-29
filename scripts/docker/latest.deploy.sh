docker build -t register-service:latest ./src/RegisterService
docker tag register-service:latest brdv/register-service:latest
docker push brdv/register-service:latest