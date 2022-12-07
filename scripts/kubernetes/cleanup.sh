kubectl delete service register-service-latest register-service-next traefik-dl 
kubectl delete deployment register-service-latest register-service-next traefik-dl 
kubectl delete ingressRoute traefik-dl-dashboard prodtest-crd
kubectl delete traefikservice.traefik.containo.us/dl-mirror 

helm uninstall traefik-dl

echo "\n"

kubectl get all