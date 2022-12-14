
printf '=%.0s' {1..80}

echo "\nThe logs for Vcurrent:\n"

kubectl logs deployments/kitchen-service-latest

echo "\n"

printf '=%.0s' {1..80}

echo "\nThe logs for Vnext:\n"

kubectl logs deployments/kitchen-service-next

echo "\n"

printf '=%.0s' {1..80}
