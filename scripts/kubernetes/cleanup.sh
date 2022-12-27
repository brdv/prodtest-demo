kubectl delete deployment order-service-latest order-service-next traefik-dl kitchen-service-latest kitchen-service-next
kubectl delete service order-service-latest order-service-next traefik-dl prodtest-db prodtest-db-instances
kubectl delete ingressRoute traefik-dl-dashboard prodtest-crd
kubectl delete traefikservice.traefik.containo.us/dl-mirror 
kubectl delete statefulset.apps/dl-rabbitmq-server
kubectl delete rabbitmqcluster.rabbitmq.com/dl-rabbitmq
kubectl delete innodbclusters.mysql.oracle.com prodtest-db
kubectl delete deployment.apps/rabbitmq-cluster-operator -n rabbitmq-system
kubectl delete deployment.apps/mysql-operator -n mysql-operator


helm uninstall traefik-dl
helm uninstall mysql-operator -n mysql-operator
kubectl delete namespaces rabbitmq-system mysql-operator


echo "\n"

kubectl get all