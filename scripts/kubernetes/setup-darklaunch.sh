# first, traefik has to be authorized:
helm repo add traefik https://traefik.github.io/charts
helm repo update
helm install traefik-dl traefik/traefik --version 20.1.0

echo "\nAdded and applied traefik helm: https://artifacthub.io/packages/helm/traefik/traefik\n"

# secondly, apply the prodtest configurations
kubectl apply -f ./kubernetes/ingresscrd-prodtest.yaml \
              -f ./kubernetes/prodtest.latest.yaml \
              -f ./kubernetes/prodtest.next.yaml \
              -f ./kubernetes/traefik-service-mirror.yml

# check if everything is up and running
kubectl get all

# echo the url of the api
echo "\nCheck out the api at http://localhost/api/health"