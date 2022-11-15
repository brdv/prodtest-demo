docker build -t prodtestapi:latest ./ProdtestApi
docker tag prodtestapi:latest brdv/prodtest:latest
docker push brdv/prodtest:latest