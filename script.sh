DOCKER_USER="tuusuario"
IMAGE_NAME="candyapi"
VERSION=${1:-latest}

echo "Building image..."
docker build -t $DOCKER_USER/$IMAGE_NAME:$VERSION .
docker build -t $DOCKER_USER/$IMAGE_NAME:latest .

echo "Pushing to Docker Hub..."
docker push $DOCKER_USER/$IMAGE_NAME:$VERSION
docker push $DOCKER_USER/$IMAGE_NAME:latest

echo "Done! Image pushed: $DOCKER_USER/$IMAGE_NAME:$VERSION"