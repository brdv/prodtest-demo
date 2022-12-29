cd ./src

if [ -z "$1" ];
then
    echo "ERROR: You have to specify a version to build"
    echo "Use this script as follows:"
    echo "\tsh ./scripts/docker/build-prodtest-images.sh <version>"
    exit 0
fi

docker build -t order-api:$1 -f ./Services/Order/Order.API/Order.API.Dockerfile . --build-arg VERSION=$1
docker build -t kitchen-service:$1 -f ./Services/Kitchen/Kitchen/Dockerfile . --build-arg VERSION=$1