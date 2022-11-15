kubectl delete --all service
kubectl delete --all deployments
kubectl delete --all ingress
helm uninstall traefik-dl

echo "\n"

kubectl get all