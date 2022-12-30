echo "\nNow adding all services.\n"

kubectl exec --stdin --tty prodtest-db-0 -- /bin/mysqlsh --sql -u prodtest -pprodtest-dl -e "create database if not exists prodtest"
kubectl exec --stdin --tty prodtest-db-0 -- /bin/mysqlsh --sql -u prodtest -pprodtest-dl -e "create table if not exists prodtest.HandledOrders(    Id                char(36) not null        primary key,    OrderId           char(36) not null,    EstimatedPrepTime int      not null,    ActualPrepTime    int      not null);"
kubectl exec --stdin --tty prodtest-db-0 -- /bin/mysqlsh --sql -u prodtest -pprodtest-dl -e "create table if not exists prodtest.HandledOrdersShadow(    Id                char(36) not null        primary key,    OrderId           char(36) not null,    EstimatedPrepTime int      not null,    ActualPrepTime    int      not null);"

# secondly, apply the prodtest configurations
kubectl apply -f ./manifests/ingresscrd-prodtest.yaml \
              -f ./manifests/order-service.latest.yaml \
              -f ./manifests/order-service.next.yaml \
              -f ./manifests/kitchen-service.latest.yaml \
              -f ./manifests/kitchen-service.next.yaml \
              -f ./manifests/traefik-service-mirror.yaml

# check if everything is up and running
kubectl get all

# echo the url of the api
echo "\nCheck out the api at http://localhost/api/health"