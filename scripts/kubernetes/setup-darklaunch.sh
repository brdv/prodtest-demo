echo "\nNow adding all services.\n"

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