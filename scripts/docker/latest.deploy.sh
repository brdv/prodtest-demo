docker build -t RegisterService:latest ./RegisterService
docker tag RegisterService:latest brdv/RegisterService:latest
docker push brdv/RegisterService:latest