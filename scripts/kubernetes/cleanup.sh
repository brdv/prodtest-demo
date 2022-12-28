kubectl delete deployment order-service-latest order-service-next traefik-dl kitchen-service-latest kitchen-service-next
kubectl delete service order-service-latest order-service-next traefik-dl prodtest-db prodtest-db-instances

kubectl delete ingressRoute traefik-dl-dashboard prodtest-crd
kubectl delete traefikservice.traefik.containo.us/dl-mirror 
kubectl delete serviceaccount prodtest-db-sa dl-rabbitmq-server traefik-dl
kubectl delete clusterroles traefik-dl-default
kubectl delete clusterrolebinding traefik-dl-default

kubectl delete statefulset.apps/dl-rabbitmq-server
kubectl delete rabbitmqcluster.rabbitmq.com/dl-rabbitmq
kubectl delete deployment.apps/rabbitmq-cluster-operator -n rabbitmq-system
kubectl delete roles dl-rabbitmq-peer-discovery
kubectl delete serviceaccount dl-rabbitmq-server
kubectl delete clusterroles rabbitmq-cluster-operator-role
kubectl delete clusterrolebinding rabbitmq-cluster-operator-rolebinding
kubectl delete rolebinding dl-rabbitmq-server
kubectl delete pvc persistence-dl-rabbitmq-server-0
kubectl delete namespaces rabbitmq-system

kubectl delete deployment.apps/mysql-operator -n mysql-operator
kubectl delete statefulset prodtest-db

kubectl delete innodbclusters.mysql.oracle.com prodtest-db
kubectl delete serviceaccount prodtest-db-sa
kubectl delete clusterroles mysql-operator mysql-sidecar
kubectl delete clusterrolebinding mysql-operator-rolebinding
kubectl delete rolebinding prodtest-db-sidecar-rb
kubectl delete pvc datadir-prodtest-db-0
kubectl delete namespaces mysql-operator

helm uninstall traefik-dl
helm uninstall mysql-operator -n mysql-operator

helm delete prodtest-db

echo "\n"

kubectl get all