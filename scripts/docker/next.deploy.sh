docker build -t RegisterService:vnext ./RegisterService
docker tag RegisterService:vnext brdv/RegisterService:vnext
docker push brdv/RegisterService:vnext