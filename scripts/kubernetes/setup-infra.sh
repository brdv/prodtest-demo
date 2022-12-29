# first, traefik has to be setup:
helm repo add traefik https://traefik.github.io/charts
helm repo add mysql-operator https://mysql.github.io/mysql-operator/
helm repo update
helm install traefik-dl traefik/traefik --version 20.1.0

echo "\nAdded and applied traefik helm: https://artifacthub.io/packages/helm/traefik/traefik"

echo "\nNow installing mysql into the cluster"

helm install mysql-operator mysql-operator/mysql-operator --namespace mysql-operator --create-namespace

helm install prodtest-db mysql-operator/mysql-innodbcluster \
    --set credentials.root.user='prodtest' \
    --set credentials.root.password='prodtest-dl' \
    --set credentials.root.host='%' \
    --set serverInstances=1 \
    --set routerInstances=1 \
    --set tls.useSelfSigned=true 

echo "\nAdded mysql"

echo "\nNow installing the RabbitMQ cluster"

kubectl apply -f "https://github.com/rabbitmq/cluster-operator/releases/latest/download/cluster-operator.yml"
kubectl apply -f ./manifests/rabbitmq-cluster.yaml -f ./manifests/rabbit-secrets.yaml

echo "\nWaiting for resources being ready.\n"

sleep 30

echo "\nInfra setup, you can now run setup-darklaunch.sh\n"