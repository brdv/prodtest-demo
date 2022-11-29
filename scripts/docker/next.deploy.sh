docker build -t register-service:vnext ./src/RegisterService
docker tag register-service:vnext brdv/register-service:vnext
docker push brdv/register-service:vnext