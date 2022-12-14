# first, traefik has to be setup:
helm repo add traefik https://traefik.github.io/charts
helm repo update
helm install traefik-dl traefik/traefik --version 20.1.0

echo "\nAdded and applied traefik helm: https://artifacthub.io/packages/helm/traefik/traefik\n"


# secondly, we setup rabbitmq
echo "\nAdding kubernetes cluster before adding services\n"

kubectl apply -f ./manifests/rabbitmq-cluster.yaml -f ./manifests/rabbit-secrets.yaml

echo "\nInfra setup, you can now run setup-darklaunch.sh\n"