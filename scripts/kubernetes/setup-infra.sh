# first, traefik has to be setup:
helm repo add traefik https://traefik.github.io/charts
helm repo update
helm install traefik-dl traefik/traefik --version 20.1.0

echo "\nAdded and applied traefik helm: https://artifacthub.io/packages/helm/traefik/traefik\n"


kubectl apply -f "https://github.com/rabbitmq/cluster-operator/releases/latest/download/cluster-operator.yml"
kubectl apply -f ./manifests/rabbitmq-cluster.yaml -f ./manifests/rabbit-secrets.yaml

echo "\nWaiting for resources being ready.\n"

sleep 30

echo "\nInfra setup, you can now run setup-darklaunch.sh\n"