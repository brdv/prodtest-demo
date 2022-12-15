kubectl delete deployment order-service-latest order-service-next traefik-dl kitchen-service-latest kitchen-service-next
kubectl delete service order-service-latest order-service-next traefik-dl kitchen-service-latest kitchen-service-next 
kubectl delete ingressRoute traefik-dl-dashboard prodtest-crd
kubectl delete traefikservice.traefik.containo.us/dl-mirror 
kubectl delete statefulset.apps/dl-rabbitmq-server
kubectl delete rabbitmqcluster.rabbitmq.com/dl-rabbitmq

helm uninstall traefik-dl

echo "\n"

kubectl get all