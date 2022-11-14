kubectl delete --all service
kubectl delete --all deployments
kubectl delete --all ingress
helm uninstall my-traefik

echo "\n"

kubectl get all