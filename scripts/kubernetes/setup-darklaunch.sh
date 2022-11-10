# first, traefik has to be authorized:
kubectl apply -f ./kube-config/traefik-setup/00-role.yml \
              -f ./kube-config/traefik-setup/00-account.yml \
              -f ./kube-config/traefik-setup/01-role-binding.yml \
              -f ./kube-config/traefik-setup/02-traefik.yml \
              -f ./kube-config/traefik-setup/02-traefik-services.yml

# secondly, apply the prodtest configurations
kubectl apply -f ./kube-config/prodtest-ingress.yml \
              -f ./kube-config/prodtest.latest.yaml

# check if everything is up and running
kubectl get all

echo "\nNOTE: If you have encountered an error, please make sure minikube is running.\n"
