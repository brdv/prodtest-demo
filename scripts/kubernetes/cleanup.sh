kubectl delete service prodtest-latest prodtest-next traefik-dl 
kubectl delete deployment prodtest-latest prodtest-next traefik-dl 
kubectl delete ingressRoute traefik-dl-dashboard prodtestlatest-crd

helm uninstall traefik-dl

echo "\n"

kubectl get all