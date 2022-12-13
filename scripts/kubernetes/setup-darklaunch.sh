# first, traefik has to be authorized:
helm repo add traefik https://traefik.github.io/charts
helm repo update
helm install traefik-dl traefik/traefik --version 20.1.0

echo "\nAdded and applied traefik helm: https://artifacthub.io/packages/helm/traefik/traefik\n"

# secondly, apply the prodtest configurations
kubectl apply -f ./manifests/ingresscrd-prodtest.yaml \
              -f ./manifests/register-service.latest.yaml \
              -f ./manifests/register-service.next.yaml \
              -f ./manifests/kitchen-service.latest.yaml \
              -f ./manifests/traefik-service-mirror.yaml \
              -f ./manifests/rabbitmq-cluster.yaml



# check if everything is up and running
kubectl get all

# echo the url of the api
echo "\nCheck out the api at http://localhost/api/health"