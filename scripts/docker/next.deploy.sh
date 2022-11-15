docker build -t prodtestapi:vnext ./ProdtestApi
docker tag prodtestapi:vnext brdv/prodtest:vnext
docker push brdv/prodtest:vnext