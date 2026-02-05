

DOCKER_USER="tuusuario"
IMAGE_NAME="candyapi"
VERSION=${1:-latest}

echo "Logging in to Docker Hub..."
docker login

echo "Building for Linux amd64..."
docker buildx build \
  --platform linux/amd64 \
  -t $DOCKER_USER/$IMAGE_NAME:$VERSION \
  -t $DOCKER_USER/$IMAGE_NAME:latest \
  --push .
  
 

echo "Done! Image: $DOCKER_USER/$IMAGE_NAME:$VERSION"